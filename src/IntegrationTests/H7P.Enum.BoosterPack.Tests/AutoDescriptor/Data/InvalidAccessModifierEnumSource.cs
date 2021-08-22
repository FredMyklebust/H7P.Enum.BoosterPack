using System.ComponentModel;
using H7P.AutoDescriptor;

namespace H7P.AutoDescriptor.ConsoleApp
{
    public class OuterCkass1
    {
        [Describable]
        private enum Access
        {
            [Description("Privat")]
            Private,

            [Description("Offentlig")]
            Public
        }
    }

    public class OuterClass1
    {
        [Describable]
        protected enum Access2
        {
            [Description("Privat")]
            Private,

            [Description("Offentlig")]
            Public,

            [Description("Beskyttet")]
            Protected
        }
    }

    public class OuterCkass3
    {
        [Describable]
        protected internal enum Accessor
        {
            [Description("Dør")]
            Door,

            [Description("Port")]
            Gate
        }
    }

    public class OuterCkass4
    {
        [Describable]
        private protected enum AccessModifier72
        {
            [Description("Ny morro")]
            NewGoodness,

            [Description("Gammel utslitt")]
            OldWorn
        }
    }
}