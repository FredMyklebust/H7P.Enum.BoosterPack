using H7P.AutoEnumDescriptor.SourceGenerator.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H7P.AutoEnumDescriptor.SourceGenerator.FastString.SourceGenerator
{
    [Generator]
    public class FastToStringGenerator : ISourceGenerator
    {
        private static readonly DiagnosticDescriptor InvalidNodeWarning = new(id: DiagnosticTokens.InvalidNodeId,
                                                                                title: DiagnosticTokens.InvalidNodeTitle,
                                                                                messageFormat: DiagnosticTokens.InvalidNodeMessage,
                                                                                category: DiagnosticTokens.InvalidNodeCategory,
                                                                                defaultSeverity: DiagnosticTokens.InvalidNodeSeverity,
                                                                                isEnabledByDefault: true);

        private readonly SourceBuilder _enumDescriptorBuilder = new();

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new EnumSyntaxFinder());
        }

        public void Execute(GeneratorExecutionContext context)
        {

            if (context.SyntaxReceiver is not EnumSyntaxFinder syntaxReceiver)
            {
                context.ReportDiagnostic(Diagnostic.Create(InvalidNodeWarning, Location.None));
                return;
            }

            if (syntaxReceiver.Enums.Count == 0)
            {
                return;
            }

            var enumDetails = ExtractEnumDetails(syntaxReceiver.Enums);
            if (enumDetails.Count == 0)
            {
                return;
            }

            foreach (var source in GenerateSource(enumDetails))
            {
                context.AddSource($"{source.Key}{nameof(FastToStringGenerator)}", SourceText.From(source.Value, Encoding.UTF8, SourceHashAlgorithm.Sha256));
            }
        }

        private List<FastStringEnum> ExtractEnumDetails(List<(string Namespace, EnumDeclarationSyntax EnumDeclaration)> EnumsDetails)
        {
            List<FastStringEnum> enums = new();

            foreach (var (Namespace, EnumDeclaration) in EnumsDetails)
            {
                var enumDeclaration = EnumDeclaration;
                if (HasIgnoredAccessModifiers(enumDeclaration))
                {
                    continue;
                }

                var accessModifier = enumDeclaration.Modifiers.SingleOrDefault(m => m.IsKind(SyntaxKind.PublicKeyword)
                                                                                || m.IsKind(SyntaxKind.InternalKeyword));

                var enumValues = enumDeclaration
                        .DescendantNodes()
                        .OfType<EnumMemberDeclarationSyntax>()
                        .ToList();

                if (enumValues.Count == 0)
                {
                    continue;
                }

                var enumName = enumDeclaration.Identifier.Text;

                var enumKeys = enumValues.Select(e => e.Identifier.Text).ToList();

                var modifier = accessModifier.Text;
                FastStringEnum enumDetails = new(Namespace, modifier, enumName, enumKeys);
                enums.Add(enumDetails);
            }
            return enums;
        }

        // We only consider "internal" or "public" enums. "internal protected", "protected" and "private" are ignored.
        private static bool HasIgnoredAccessModifiers(EnumDeclarationSyntax enumDeclaration)
        {
            return enumDeclaration.Modifiers.Count > 1
                                    || enumDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.ProtectedKeyword)
                                                                            || m.IsKind(SyntaxKind.PrivateKeyword));
        }

        private Dictionary<string, string> GenerateSource(List<FastStringEnum> enums)
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
}
