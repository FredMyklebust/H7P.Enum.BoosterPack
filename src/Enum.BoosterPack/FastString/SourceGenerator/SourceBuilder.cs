using H7P.AutoEnumDescriptor.SourceGenerator.Models;
using System.Text;

namespace H7P.AutoEnumDescriptor.SourceGenerator.FastString.SourceGenerator
{
    public class SourceBuilder
    {
        private readonly StringBuilder _sourceBuilder = new();

        private FastStringEnum _enumItem = null;
        private int _indent = 0;

        public string GenerateSource(FastStringEnum enumItem)
        {
            _enumItem = enumItem;
            _indent = 0;
            _sourceBuilder.Clear();

            AppendUsings();
            AppendNamespace();
            AppendClassDeclaration();
            //AppendMethodGetFastStringSummary();
            AppendMethodGetFastStringSignature();
            AppendMethodGetFastBody();
            AppendEndGetFastMethod();
            AppendEndClass();
            AppendEndNamespace();

            return _sourceBuilder.ToString();
        }

        private void AppendUsings()
        {
            AppendLine("using System;");
            Append("using ").Append(_enumItem.Namespace).AppendLine(";");
            AppendLine();
        }

        private void AppendNamespace()
        {
            Append("namespace ").AppendLine(_enumItem.Namespace);
            AppendLine("{");
            IncrementIndent();
        }

        private void AppendClassDeclaration()
        {
            //prefix class-name with unicode to prevent name-collisions - Ɠ - Ɠenerated 😊
            AppendWithIndent(_enumItem.Modifier).Append(" static class Ɠ").Append(_enumItem.Name).AppendLine("FastStringExtensions");
            AppendLineWithIndent("{");
            IncrementIndent();
        }

        private void AppendMethodGetFastStringSummary()
        {
            AppendLineWithIndent("/// <summary>");
            AppendWithIndent("/// Gets the description from the <see cref=\"System.ComponentModel.DescriptionAttribute\"/> for the supplied <see cref=\"").Append(_enumItem.Name);
            AppendLine("\"/> value.");
            AppendLineWithIndent("/// </summary>");
            AppendLineWithIndent("/// <exception cref=\"ArgumentException\"> Throws if the description for the value is missing, or an invalid value is supplied.</exception> ");
            AppendLineWithIndent("/// <param name=\"enumValue\"></param>");
            AppendLineWithIndent("/// <returns>");
            AppendLineWithIndent("/// A <see cref=\"string\"/> with the description.");
            AppendLineWithIndent("/// </returns>");
        }

        private void AppendMethodGetFastStringSignature()
        {
            AppendWithIndent("public static string ToFastString(this ").Append(_enumItem.Name).AppendLine(" enumValue)");
            AppendLineWithIndent("{");
            IncrementIndent();
        }

        private void AppendMethodGetFastBody()
        {
            AppendLineWithIndent("switch(enumValue)");
            AppendLineWithIndent("{");
            IncrementIndent();
            foreach (var enumValue in _enumItem.Values)
            {
                AppendWithIndent("case ").Append(_enumItem.Name).Append('.').Append(enumValue).Append(": return \"").Append(enumValue).AppendLine("\";");
            }
            AppendLineWithIndent("default: throw new ArgumentException($\"{enumValue} invalid enum-specified\");");
            DecrementIndent();
            AppendLineWithIndent("};");
        }

        private void AppendEndGetFastMethod()
        {
            DecrementIndent();
            AppendLineWithIndent("}");
        }

        private void AppendEndClass()
        {
            DecrementIndent();
            AppendLineWithIndent("}");
        }

        private void AppendEndNamespace()
        {
            AppendLine("}");
        }


        private void IncrementIndent()
        {
            _indent++;
        }

        private void DecrementIndent()
        {
            _indent--;
        }

        private StringBuilder AppendWithIndent(string s)
        {
            if (_indent > 0)
            {
                _sourceBuilder.Append('\t', _indent);
            }
            _sourceBuilder.Append(s);
            return _sourceBuilder;
        }

        private StringBuilder AppendLineWithIndent(string s)
        {
            return AppendWithIndent(s).AppendLine();
        }

        private StringBuilder Append(string s = null)
        {
            return _sourceBuilder.Append(s);
        }

        private StringBuilder AppendLine(string s = null)
        {
            return _sourceBuilder.AppendLine(s);
        }
    }
}
