using H7P.Enum.BoosterPack.AsString.SourceGenerator;
using System.Threading.Tasks;
using Xunit;
using VerifyCS = CSharpSourceGeneratorVerifier<H7P.Enum.BoosterPack.AsString.SourceGenerator.AsStringGenerator>;

namespace H7P.Enum.BoosterPack.IntegrationTests.FastString.Scenarios
{
    public class CanGetAsStringTests
    {
        private const string _testContext = "AsString";

        [Theory]
        [InlineData("InternalEnumSource.cs", "StateAsStringGenerator.cs", "ExpectedGeneratedInternalEnum.cs")]
        [InlineData("PublicEnumSource.cs", "ColorAsStringGenerator.cs", "ExpectedGeneratedPublicEnum.cs")]
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
                        TestSourceCreator.CreateExpectedSourceFromFile<AsStringGenerator>(expectedGeneratedName, _testContext, expectedSourceFromFile)
                    }
                },
            }.RunAsync()
            .ConfigureAwait(false);
        }
    }

}
