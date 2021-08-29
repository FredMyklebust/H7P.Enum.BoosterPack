using H7P.AutoEnumDescriptor.SourceGenerator.AutoDescriptor.SourceGenerator;
using H7P.Enum.BoosterPack.SourceBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H7P.Enum.BoosterPack.AutoDescriptor.SourceGenerator
{
    public class SourceBuilder
    {
        public string GenerateSource(EnumDetails enumItem)
        {
            var caseStatements = enumItem
                                     .KeyValues
                                     .Select(v => new ReturnCaseStatement($"{enumItem.Name}.{v.Key}", $"\"{v.Value}\""))
                                     .ToList();

            var exceptions = new List<ExceptionSummary>
            {
                new ExceptionSummary(nameof(ArgumentException), "Throws if the description for the value is missing, or an invalid value is supplied.")
            };

            var s = ExtensionBuilder
                        .NewExtension(enumItem.Name)
                        .AddUsing("System")
                        .AddUsing(enumItem.Namespace)
                        .InNamepace(enumItem.Namespace)
                        .AddExtensionClass(enumItem.Modifier, $"{enumItem.Name}GetDescriptionExtensions")
                        .AddExtensionSummaryFormat("Gets the description from the <see cref=\"System.ComponentModel.DescriptionAttribute\"/> for the supplied <see cref=\"{0}\"/> value.", enumItem.Name)
                        .AddTypedParamTag("enumValue", "The enum to get descriptions from.")
                        .AddExceptionTags(exceptions)
                        .AddReturnsTag("A <see cref=\"string\"/> with the description.")
                        .AddExtensionMethod("GetDescription", "string", "enumValue")
                        .BeginSwitchOn("enumValue")
                        .AddReturnCases(caseStatements)
                        .DefaultCaseThrowsArgumentException()
                        .EndExtensionMethod()
                        .EndNamespace()
                        .Build();

            return s;
        }
    }


}
