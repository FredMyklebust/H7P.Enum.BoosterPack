using H7P.AutoDescriptor;
using System.ComponentModel;
using Xunit;

namespace H7P.Enum.BoosterPack.UnitTests.Scenarios.ToDescriptionTests
{

    public class WhenEnumIsInternal
    {
        [Fact]
        public void All_enum_values_with_descriptions_have_ToDescription()
        {
            Assert.Equal("Tiny", Size.Small.GetDescription());
            Assert.Equal("Average", Size.Medium.GetDescription());
            Assert.Equal("Big", Size.Large.GetDescription());
        }
    }

    [Describable]
    internal enum Size
    {
        [Description("Tiny")]
        Small,

        [Description("Average")]
        Medium,

        [Description("Big")]
        Large
    }


}
