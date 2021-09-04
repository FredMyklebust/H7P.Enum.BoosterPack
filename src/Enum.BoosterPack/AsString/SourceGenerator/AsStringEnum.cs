using System.Collections.Generic;

namespace H7P.Enum.BoosterPack.AsString.SourceGenerator
{
    public class AsStringEnum
    {
        public AsStringEnum(string @namespace, string modifier, string name, List<string> values)
        {
            Namespace = @namespace;
            Name = name;
            Modifier = modifier;
            Values = values;
        }

        public string Namespace { get; }

        public string Modifier { get; }
        public string Name { get; }
        public List<string> Values { get; } = new();
    }
}
