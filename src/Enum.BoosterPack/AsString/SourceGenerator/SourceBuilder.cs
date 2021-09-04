using H7P.Enum.BoosterPack.SourceBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H7P.Enum.BoosterPack.AsString.SourceGenerator
{
    public class SourceBuilder
    {
        public string GenerateSource(AsStringEnum enumItem)
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
                        .AddExtensionClass(enumItem.Modifier, $"{enumItem.Name}AsStringExtensions")
                        .AddExtensionSummaryFormat("Gets the string representation for the supplied <see cref=\"{0}\"/> value.", enumItem.Name)
                        .AddTypedParamTag(paramName, "The enum to get the string representation from.")
                        .AddExceptionTags(exceptions)
                        .AddReturnsTag("A <see cref=\"string\"/>.")
                        .AddExtensionMethod("AsString", "string", paramName)
                        .BeginSwitchOn(paramName)
                        .AddReturnCases(caseStatements)
                        .DefaultCaseThrowsArgumentException()
                        .EndExtensionMethod()
                        .EndNamespace()
                        .Build();
        }
    }
}
