using H7P.AutoEnumDescriptor.SourceGenerator.Models;
using H7P.Enum.BoosterPack.SourceBuilder;
using System.Linq;

namespace H7P.AutoEnumDescriptor.SourceGenerator.FastString.SourceGenerator
{
    public class SourceBuilder
    {
        public string GenerateSource(FastStringEnum enumItem)
        {
            var caseStatements = enumItem
                                    .Values
                                    .Select(v => new ReturnCaseStatement($"{enumItem.Name}.{v}", $"\"{v}\""))
                                    .ToList();

            return ExtensionBuilder
                        .NewExtension(enumItem.Name)
                        .AddUsing("System")
                        .AddUsing(enumItem.Namespace)
                        .InNamepace(enumItem.Namespace)
                        .AddExtensionClass(enumItem.Modifier, $"{enumItem.Name}ToFastStringExtensions")
                        .AddExtensionMethod("ToFastString", "string", "enumValue")
                        .BeginSwitchOn("enumValue")
                        .AddReturnCases(caseStatements)
                        .DefaultCaseThrowsArgumentException()
                        .EndExtensionMethod()
                        .EndNamespace()
                        .Build();

        }
    }
}
