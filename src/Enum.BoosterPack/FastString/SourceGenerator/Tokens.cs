using Microsoft.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("H7P.Enum.BoosterPack.IntegrationTests")]

namespace H7P.AutoEnumDescriptor.SourceGenerator.FastString.SourceGenerator
{
    internal static class DiagnosticTokens
    {
        internal const string InvalidNodeId = "H7DG0005";
        internal const DiagnosticSeverity InvalidNodeSeverity = DiagnosticSeverity.Warning;
        internal const string InvalidNodeTitle = "Invalid node passed to Generator";
        internal const string InvalidNodeMessage = "An unknown type was passed to the generator can not handle, source won't be generated";
        internal const string InvalidNodeCategory = "Unknown";
    }
}
