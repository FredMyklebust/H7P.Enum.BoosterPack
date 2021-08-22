using H7P.AutoDescriptor;
using System;
using System.ComponentModel;
using Xunit;

namespace H7P.Enum.BoosterPack.UnitTests.Scenarios.ToDescriptionTests
{
    public class WhenEnumIsMissingDescription
    {

        [Fact]
        public void Throws_argumentException_when_no_description_attribute()
        {
            Assert.Throws<ArgumentException>(() => Unmentionable.HeWhoShouldNotBeNamed.GetDescription());
        }

        [Fact]
        public void Throws_argumentException_when_invalid_value_supplied()
        {
            Assert.Throws<ArgumentException>(() => ((Unmentionable)2).GetDescription());
        }

    }

    [Describable]
    public enum Unmentionable
    {
        [Description("Value has not been set")]
        NotSet = 0,

        HeWhoShouldNotBeNamed
    }
}
