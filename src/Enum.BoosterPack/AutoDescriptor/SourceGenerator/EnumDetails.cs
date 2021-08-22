using System.Collections.Generic;

namespace H7P.AutoEnumDescriptor.SourceGenerator.AutoDescriptor.SourceGenerator
{
    public class EnumDetails
    {
        public EnumDetails(string @namespace, string modifier, string name, Dictionary<string, string> keyValues)
        {
            Namespace = @namespace;
            Name = name;
            Modifier = modifier;
            KeyValues = keyValues;
        }

        public string Namespace { get; }

        public string Modifier { get; }
        public string Name { get; }
        public Dictionary<string, string> KeyValues { get; } = new();
    }
}
