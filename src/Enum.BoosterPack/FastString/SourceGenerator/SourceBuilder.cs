using H7P.AutoEnumDescriptor.SourceGenerator.Models;
using H7P.Enum.BoosterPack.SourceBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H7P.AutoEnumDescriptor.SourceGenerator.FastString.SourceGenerator
{
    public class SourceBuilder
    {
        public string GenerateSource(FastStringEnum enumItem)
        {
            const string paramName = "enumValue";

            var caseStatements = enumItem
                                    .Values
                                    .Select(v => new ReturnCaseStatement($"{enumItem.Name}.{v}", $"\"{v}\""))
                                    .ToList();

            var exceptions = new List<ExceptionSummary>
            {
                new ExceptionSummary(nameof(ArgumentException), "Throws if an invalid enum is supplied.")
            };

            return ExtensionBuilder
                        .NewExtension(enumItem.Name)
                        .AddUsing("System")
                        .AddUsing(enumItem.Namespace)
                        .InNamepace(enumItem.Namespace)
                        .AddExtensionClass(enumItem.Modifier, $"{enumItem.Name}ToFastStringExtensions")
                        .AddExtensionSummaryFormat("Gets the string representation for the supplied <see cref=\"{0}\"/> value.", enumItem.Name)
                        .AddTypedParamTag(paramName, "The enum to get the string representation from.")
                        .AddExceptionTags(exceptions)
                        .AddReturnsTag("A <see cref=\"string\"/>.")
                        .AddExtensionMethod("ToFastString", "string", paramName)
                        .BeginSwitchOn(paramName)
                        .AddReturnCases(caseStatements)
                        .DefaultCaseThrowsArgumentException()
                        .EndExtensionMethod()
                        .EndNamespace()
                        .Build();

        }
    }
}
