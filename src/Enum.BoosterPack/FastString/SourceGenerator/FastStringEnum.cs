using System.Collections.Generic;

namespace H7P.AutoEnumDescriptor.SourceGenerator.Models
{
    public class FastStringEnum
    {
        public FastStringEnum(string @namespace, string modifier, string name, List<string> values)
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
