using Xunit;

namespace H7P.Enum.BoosterPack.UnitTests.Scenarios.ToFastStringTests
{
    public class WhenEnumIsPublic
    {

        [Fact]
        public void All_enum_values_have_ToFastString()
        {
            Assert.Equal(RefactorState.Red.ToString(), RefactorState.Red.ToFastString());
            Assert.Equal(RefactorState.Green.ToString(), RefactorState.Green.ToFastString());
            Assert.Equal(RefactorState.Refactor.ToString(), RefactorState.Refactor.ToFastString());
        }
    }

    public enum RefactorState
    {
        Red,
        Green,
        Refactor
    }
}
