using Xunit;

namespace H7P.Enum.BoosterPack.UnitTests.Scenarios.ToFastStringTests
{
    public class WhenEnumIsPublic
    {
        [Fact]
        public void All_enum_values_have_ToFastString()
        {
            Assert.Equal(nameof(RefactorState.Red), RefactorState.Red.ToFastString());
            Assert.Equal(nameof(RefactorState.Green), RefactorState.Green.ToFastString());
            Assert.Equal(nameof(RefactorState.Refactor), RefactorState.Refactor.ToFastString());
        }
    }

    public enum RefactorState
    {
        Red,
        Green,
        Refactor
    }
}
