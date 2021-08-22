using System.ComponentModel;
using H7P.AutoDescriptor;

namespace H7P.AutoDescriptor.ConsoleApp
{
    [Describable]
    internal enum Color
    {
        [Description("Rød")]
        Red,

        [Description("Grønn")]
        Green
    }
}