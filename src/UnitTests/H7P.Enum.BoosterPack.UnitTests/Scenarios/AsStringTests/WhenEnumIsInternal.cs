using Xunit;

namespace H7P.Enum.BoosterPack.UnitTests.Scenarios.ToFastStringTests
{
    public class WhenEnumIsInternal
    {
        [Fact]
        public void All_enum_values_have_ToFastString()
        {
            Assert.Equal(nameof(TrafficLightState.Red), TrafficLightState.Red.AsString());
            Assert.Equal(nameof(TrafficLightState.Green), TrafficLightState.Green.AsString());
            Assert.Equal(nameof(TrafficLightState.Yellow), TrafficLightState.Yellow.AsString());
        }
    }

    internal enum TrafficLightState
    {
        Red,
        Green,
        Yellow
    }
}
