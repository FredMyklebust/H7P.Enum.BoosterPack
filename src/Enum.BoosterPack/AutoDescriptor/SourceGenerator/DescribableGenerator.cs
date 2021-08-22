using H7P.AutoEnumDescriptor.SourceGenerator.AutoDescriptor.SourceGenerator;
using H7P.AutoEnumDescriptor.SourceGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H7P.Enum.BoosterPack.AutoDescriptor.SourceGenerator
{
    [Generator]
    public class DescribableGenerator : ISourceGenerator
    {
        private static readonly DiagnosticDescriptor InvalidNodeWarning = new(id: DiagnosticTokens.InvalidNodeId,
                                                                                title: DiagnosticTokens.InvalidNodeTitle,
                                                                                messageFormat: DiagnosticTokens.InvalidNodeMessage,
                                                                                category: DiagnosticTokens.InvalidNodeCategory,
                                                                                defaultSeverity: DiagnosticTokens.InvalidNodeSeverity,
                                                                                isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor NoDescriptionsWarning = new(id: DiagnosticTokens.NoDescriptionsId,
                                                                                    title: DiagnosticTokens.NoDescriptionsTitle,
                                                                                    messageFormat: DiagnosticTokens.NoDescriptionsMessage,
                                                                                    category: DiagnosticTokens.NoDescriptionsCategory,
                                                                                    defaultSeverity: DiagnosticTokens.NoDescriptionsSeverity,
                                                                                    isEnabledByDefault: true);

        private static readonly DiagnosticDescriptor InvalidAcessModifierWarning = new(id: DiagnosticTokens.InvalidAccessModifierId,
                                                                                        title: DiagnosticTokens.InvalidAccessModifierTitle,
                                                                                        messageFormat: DiagnosticTokens.InvalidAccessModifierMessage,
                                                                                        category: DiagnosticTokens.InvalidAccessModifierCategory,
                                                                                        defaultSeverity: DiagnosticTokens.InvalidAccessModifierSeverity,
                                                                                        isEnabledByDefault: true);

        private readonly SourceBuilder _enumDescriptorBuilder = new();

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new DescribableEnumSyntaxFinder());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            //We allways emit the "DescribableAttribute" for use by consumers
            EmitDescribableAttribute(context);

            if (context.SyntaxReceiver is not DescribableEnumSyntaxFinder syntaxReceiver)
            {
                context.ReportDiagnostic(Diagnostic.Create(InvalidNodeWarning, Location.None));
                return;
            }

            if (syntaxReceiver.DescribableEnums.Count == 0)
            {
                return;
            }

            var enumDescriptions = ExtractEnumDescriptions(syntaxReceiver.DescribableEnums);
            foreach(var ignoredEnum in enumDescriptions.IgnoredEnums)
            {
                context.ReportDiagnostic(Diagnostic.Create(InvalidAcessModifierWarning, Location.None, ignoredEnum));
            }

            foreach (var noDescriptionEnum in enumDescriptions.NoDescriptionEnums)
            {
                context.ReportDiagnostic(Diagnostic.Create(NoDescriptionsWarning, Location.None, noDescriptionEnum));
            }

            if (enumDescriptions.EnumDetails.Count == 0)
            {
                return;
            }

            foreach (var source in GenerateSource(enumDescriptions.EnumDetails))
            {
                context.AddSource($"{source.Key}{nameof(DescribableGenerator)}", SourceText.From(source.Value, Encoding.UTF8, SourceHashAlgorithm.Sha256));
            }
        }

        private static void EmitDescribableAttribute(GeneratorExecutionContext context)
        {
            context.AddSource(Indentifiers.DescribableAttributeComplete, SourceText.From(GeneratorData.DescribableAttributeText, Encoding.UTF8, SourceHashAlgorithm.Sha256));
        }

        private ExtractedEnumResult ExtractEnumDescriptions(List<(string Namespace, EnumDeclarationSyntax EnumDeclaration)> EnumsToDescribe)
        {
            List<EnumDetails> enums = new();
            List<string> IgnoredEnums = new();
            List<string> noDescriptionEnums = new();

            foreach (var (Namespace, EnumDeclaration) in EnumsToDescribe)
            {
                var enumDeclaration = EnumDeclaration;
                if (HasIgnoredAccessModifiers(enumDeclaration))
                {
                    IgnoredEnums.Add(enumDeclaration.Identifier.Text);
                    continue;
                }

                var accessModifier = enumDeclaration.Modifiers.FirstOrDefault(m => m.IsKind(SyntaxKind.PublicKeyword)
                                                                                || m.IsKind(SyntaxKind.InternalKeyword));

                var describedValues = enumDeclaration
                                        .DescendantNodes()
                                        .OfType<EnumMemberDeclarationSyntax>()
                                        .Where(x => x.AttributeLists.Any(DescriptionAttributesFilter))
                                        .ToList();

                if (describedValues.Count == 0)
                {
                    noDescriptionEnums.Add(enumDeclaration.Identifier.Text);
                    continue;
                }

                Dictionary<string, string> enumDescriptions = new(describedValues.Count);
                foreach (var describedValue in describedValues)
                {
                    var description = GetDescriptionFromAttribute(describedValue);
                    if (description == null)
                    {
                        continue;
                    }

                    var enumName = describedValue.Identifier.Text;
                    enumDescriptions.Add(enumName, description);
                }

                if (enumDescriptions.Count == 0)
                {
                    noDescriptionEnums.Add(enumDeclaration.Identifier.Text);
                    continue;
                }

                var key = enumDeclaration.Identifier.Text;
                var modifier = accessModifier.Text;
                EnumDetails enumDetails = new(Namespace, modifier, key, enumDescriptions);
                enums.Add(enumDetails);
            }
            return new ExtractedEnumResult(enums, IgnoredEnums, noDescriptionEnums);
        }

        // We only consider "internal" or "public" enums. "internal protected", "protected" and "private" are ignored.
        private static bool HasIgnoredAccessModifiers(EnumDeclarationSyntax enumDeclaration)
        {
            return enumDeclaration.Modifiers.Count > 1
                                    || enumDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.ProtectedKeyword)
                                                                            || m.IsKind(SyntaxKind.PrivateKeyword));
        }

        private static string GetDescriptionFromAttribute(EnumMemberDeclarationSyntax describedValue)
        {
            var descAttributeList = describedValue
                                        .AttributeLists
                                        .Single(DescriptionAttributesFilter)
                                        .Attributes
                                        .Single()
                                        .ArgumentList;

            var descriptionArg = descAttributeList!
                                    .Arguments
                                    .SingleOrDefault();

            if (descriptionArg == null)
            {
                return null;
            }

            return descriptionArg
                    .ToString()
                    .Trim(new[] { '"' });
        }

        private static bool DescriptionAttributesFilter(AttributeListSyntax attributeList)
        {
            var attributeName = attributeList.ToString();

            return attributeName.StartsWith(Indentifiers.DescriptionAttributeAbbrivated)
                    || attributeName.StartsWith(Indentifiers.DescriptionAttribute);
        }

        private Dictionary<string, string> GenerateSource(List<EnumDetails> enums)
        {
            Dictionary<string, string> sources = new();

            foreach (var enumItem in enums)
            {
                var source = _enumDescriptorBuilder.GenerateSource(enumItem);
                sources.Add(enumItem.Name, source);
            }
            return sources;
        }
    }

    public class ExtractedEnumResult
    {
        public ExtractedEnumResult(List<EnumDetails> enumDetails, List<string> IgnoredEnums, List<string> noDescriptionEnums)
        {
            EnumDetails = enumDetails;
            this.IgnoredEnums = IgnoredEnums;
            NoDescriptionEnums = noDescriptionEnums;
        }

        public List<EnumDetails> EnumDetails { get; }
        public List<string> IgnoredEnums { get; }
        public List<string> NoDescriptionEnums { get; }
    }
}
