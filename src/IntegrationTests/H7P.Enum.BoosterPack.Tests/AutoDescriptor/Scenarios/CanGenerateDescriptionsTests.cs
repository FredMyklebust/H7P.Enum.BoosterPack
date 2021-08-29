using H7P.Enum.BoosterPack.AutoDescriptor.SourceGenerator;
using System.Threading.Tasks;
using Xunit;
using VerifyCS = CSharpSourceGeneratorVerifier<H7P.Enum.BoosterPack.AutoDescriptor.SourceGenerator.DescribableGenerator>;

namespace H7P.Enum.BoosterPack.IntegrationTests.AutoDescriptor.Scenarios
{
    public class CanGenerateDescriptionsTests
    {
        private const string _testContext = "AutoDescriptor";

        [Theory]
        [InlineData("InternalValidEnumSource.cs", "TilstandDescribableGenerator.cs", "ExpectedGeneratedInternalEnum.cs")]
        [InlineData("PublicValidEnumSource.cs", "ColorDescribableGenerator.cs", "ExpectedGeneratedPublicEnum.cs")]
        public async Task When_enum_is_describable_with_descriptions_then_extension_method_is_generated(string sourceFileToTest, string expectedGeneratedName, string expectedSourceFromFile)
        {
            var codeToTest = TestSourceCreator.GetSourceContent(_testContext, sourceFileToTest);

            await new VerifyCS.Test
            {
                TestState =
                {
                    Sources = { codeToTest },
                    GeneratedSources =
                    {
                        TestSourceCreator.CreateExpectedSourceFromText<DescribableGenerator>("DescribableAttribute.cs", GeneratorData.DescribableAttributeText),
                        TestSourceCreator.CreateExpectedSourceFromFile<DescribableGenerator>(expectedGeneratedName, _testContext, expectedSourceFromFile)
                    }
                },
            }
            .RunAsync()
            .ConfigureAwait(false);
        }
    }

}
