using System.Threading.Tasks;
using Xunit;
using VerifyCS = CSharpSourceGeneratorVerifier<H7P.AutoEnumDescriptor.SourceGenerator.FastString.SourceGenerator.FastToStringGenerator>;

namespace H7P.Enum.BoosterPack.IntegrationTests.FastString.Scenarios
{
    public class NotGeneratingTests
    {
        private const string _testContext = "FastString";

        [Fact]
        public async Task When_enum_has_unsupported_access_modifiers_then_extension_method_is_not_generated()
        {
            var codeToTest = TestSourceCreator.GetSourceContent(_testContext, "InvalidAccessModifierEnumSource.cs");

            await new VerifyCS.Test
            {
                TestState =
                {
                    Sources = { codeToTest }
                },
            }
            .RunAsync()
            .ConfigureAwait(false);
        }
    }
}
