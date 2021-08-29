using System.Text;

namespace H7P.Enum.BoosterPack.SourceBuilder
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendSpace(this StringBuilder stringBuilder)
        {
            stringBuilder.Append(' ');
            return stringBuilder;
        }
    }
}
