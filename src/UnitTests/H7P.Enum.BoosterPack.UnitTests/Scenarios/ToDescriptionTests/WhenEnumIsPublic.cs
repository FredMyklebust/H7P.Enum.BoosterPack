using H7P.AutoDescriptor;
using System.ComponentModel;
using Xunit;

namespace H7P.Enum.BoosterPack.UnitTests.Scenarios.ToDescriptionTests
{

    public class WhenEnumIsPublic
    {
        [Fact]
        public void All_enum_values_with_descriptions_have_ToDescription()
        {
            Assert.Equal("Ajar", DoorState.Open.GetDescription());
            Assert.Equal("Shut", DoorState.Closed.GetDescription());
            Assert.Equal("Jammed", DoorState.Stuck.GetDescription());
        }
    }

    [Describable]
    public enum DoorState
    {
        [Description("Ajar")]
        Open,

        [Description("Shut")]
        Closed,

        [Description("Jammed")]
        Stuck
    }


}
