using H7P.AutoEnumDescriptor.SourceGenerator.FastString.SourceGenerator;
using System.Threading.Tasks;
using Xunit;
using VerifyCS = CSharpSourceGeneratorVerifier<H7P.AutoEnumDescriptor.SourceGenerator.FastString.SourceGenerator.FastToStringGenerator>;

namespace H7P.Enum.BoosterPack.IntegrationTests.FastString.Scenarios
{
    public class CanGenerateDescriptionsTests
    {
        private const string _testContext = "FastString";

        [Theory]
        [InlineData("InternalEnumSource.cs", "StateFastToStringGenerator.cs", "ExpectedGeneratedInternalEnum.cs")]
        [InlineData("PublicEnumSource.cs", "ColorFastToStringGenerator.cs", "ExpectedGeneratedPublicEnum.cs")]
        public async Task When_enums_are_specified_then_extension_method_is_generated(string sourceFileToTest, string expectedGeneratedName, string expectedSourceFromFile)
        {
            var codeToTest = TestSourceCreator.GetSourceContent(_testContext, sourceFileToTest);

            await new VerifyCS.Test
            {
                TestState =
                {
                    Sources = { codeToTest },
                    GeneratedSources =
                    {
                        TestSourceCreator.CreateExpectedSourceFromFile<FastToStringGenerator>(expectedGeneratedName, _testContext, expectedSourceFromFile)
                    }
                },
            }.RunAsync()
            .ConfigureAwait(false);
        }
    }

}
