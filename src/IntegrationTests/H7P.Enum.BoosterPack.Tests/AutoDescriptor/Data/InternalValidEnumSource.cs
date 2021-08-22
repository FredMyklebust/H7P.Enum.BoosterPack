using System.ComponentModel;
using H7P.AutoDescriptor;

namespace H7P.AutoDescriptor.ConsoleApp
{
    [Describable]
    internal enum Tilstand
    {
        [Description("Gyldig")]
        Valid,

        [Description("Ugyldig")]
        Invalid
    }
}