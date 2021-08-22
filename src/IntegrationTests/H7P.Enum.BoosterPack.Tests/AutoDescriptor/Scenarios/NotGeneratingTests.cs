using H7P.Enum.BoosterPack.AutoDescriptor.SourceGenerator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using System.Threading.Tasks;
using Xunit;
using DiagnosticTokens = H7P.Enum.BoosterPack.AutoDescriptor.SourceGenerator.DiagnosticTokens;
using VerifyCS = CSharpSourceGeneratorVerifier<H7P.Enum.BoosterPack.AutoDescriptor.SourceGenerator.DescribableGenerator>;

namespace H7P.Enum.BoosterPack.IntegrationTests.AutoDescriptor.Scenarios
{
    public class NotGeneratingTests
    {
        private const string _testContext = "AutoDescriptor";

        [Fact]
        public async Task When_enum_is_describable_with_unsupported_access_modifiers_and_valid_descriptions_then_extension_method_is_not_generated_and_warnings_are_added()
        {
            var codeToTest = TestSourceCreator.GetSourceContent(_testContext, "InvalidAccessModifierEnumSource.cs");

            await new VerifyCS.Test
            {
                TestState =
                {
                    Sources = { codeToTest },
                    GeneratedSources =
                    {
                        TestSourceCreator.CreateExpectedSourceFromText<DescribableGenerator>("DescribableAttribute.cs", GeneratorData.DescribableAttributeText),
                    },
                    ExpectedDiagnostics =
                    {
                        new DiagnosticResult(DiagnosticTokens.InvalidAccessModifierId, DiagnosticSeverity.Warning).WithArguments("Access"),
                        new DiagnosticResult(DiagnosticTokens.InvalidAccessModifierId, DiagnosticSeverity.Warning).WithArguments("Access2"),
                        new DiagnosticResult(DiagnosticTokens.InvalidAccessModifierId, DiagnosticSeverity.Warning).WithArguments("AccessModifier72"),
                        new DiagnosticResult(DiagnosticTokens.InvalidAccessModifierId, DiagnosticSeverity.Warning).WithArguments("Accessor")
                    }
                },
            }.RunAsync();
        }
    }
}
