using Xunit;

namespace H7P.Enum.BoosterPack.UnitTests.Scenarios.ToFastStringTests
{
    public class WhenEnumIsInternal
    {

        [Fact]
        public void All_enum_values_have_ToFastString()
        {
            Assert.Equal(TrafficLightState.Red.ToString(), TrafficLightState.Red.ToFastString());
            Assert.Equal(TrafficLightState.Green.ToString(), TrafficLightState.Green.ToFastString());
            Assert.Equal(TrafficLightState.Yellow.ToString(), TrafficLightState.Yellow.ToFastString());
        }
    }

    internal enum TrafficLightState
    {
        Red,
        Green,
        Yellow
    }
}
