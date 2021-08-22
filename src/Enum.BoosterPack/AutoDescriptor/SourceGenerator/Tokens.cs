using Microsoft.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleToAttribute("H7P.Enum.BoosterPack.IntegrationTests")]

namespace H7P.Enum.BoosterPack.AutoDescriptor.SourceGenerator
{
    internal static class Indentifiers
    {
        internal static string DescribableAttributeShortName = "Describable";
        internal static string DescribableAttributeComplete = "DescribableAttribute";

        internal static string DescriptionAttributeAbbrivated = "[Description(";
        internal static string DescriptionAttribute = "[DescriptionAttribute(";
    }

    internal static class GeneratorData
    {
        internal const string DescribableAttributeText = @"using System;

namespace H7P.AutoDescriptor
{
    [AttributeUsage(AttributeTargets.Enum, Inherited = false, AllowMultiple = false)]
    public sealed class DescribableAttribute : Attribute { }
}
";

    }

    internal static class DiagnosticTokens
    {
        internal const string InvalidNodeId = "H7DG0001";
        internal const DiagnosticSeverity InvalidNodeSeverity = DiagnosticSeverity.Warning;
        internal const string InvalidNodeTitle = "Invalid node passed to Generator";
        internal const string InvalidNodeMessage = "An unknown type was passed to the generator can not handle, source won't be generated";
        internal const string InvalidNodeCategory = "Unknown";

        internal const string NoDescriptionsId = "H7DG0002";
        internal const DiagnosticSeverity NoDescriptionsSeverity = DiagnosticSeverity.Warning;
        internal const string NoDescriptionsTitle = "Incomplete describable enum";
        internal const string NoDescriptionsMessage = "An enum with a DescribableAttribute was found withour any description attributes";
        internal const string NoDescriptionsCategory = "IncompleteSyntax";

        internal const string InvalidAccessModifierId = "H7DG0003";
        internal const DiagnosticSeverity InvalidAccessModifierSeverity = DiagnosticSeverity.Warning;
        internal const string InvalidAccessModifierTitle = "Invalid describable enum";
        internal const string InvalidAccessModifierMessage = "The enum {0} has a DescribableAttribute, but it is not public or internal";
        internal const string InvalidAccessModifierCategory = "InvalidSyntax";
    }
}
