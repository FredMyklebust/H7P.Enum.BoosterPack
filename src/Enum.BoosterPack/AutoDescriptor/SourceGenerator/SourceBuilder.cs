using H7P.AutoEnumDescriptor.SourceGenerator.AutoDescriptor.SourceGenerator;
using H7P.AutoEnumDescriptor.SourceGenerator.Models;
using System.Text;

namespace H7P.Enum.BoosterPack.AutoDescriptor.SourceGenerator
{
    public class SourceBuilder
    {
        private readonly StringBuilder _sourceBuilder = new();

        private EnumDetails _enumItem = null;
        private int _indent = 0;

        public string GenerateSource(EnumDetails enumItem)
        {
            _enumItem = enumItem;
            _indent = 0;
            _sourceBuilder.Clear();

            AppendUsings();
            AppendNamespace();
            AppendClassDeclaration();
            AppendMethodGetDescriptionSummary();
            AppendMethodGetDescriptionSignature();
            AppendMethodGetDescriptionBody();
            AppendEndGetDescriptionMethod();
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
            AppendWithIndent(_enumItem.Modifier).Append(" static class Ɠ").Append(_enumItem.Name).AppendLine("Extensions");
            AppendLineWithIndent("{");
            IncrementIndent();
        }

        private void AppendMethodGetDescriptionSummary()
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

        private void AppendMethodGetDescriptionSignature()
        {
            AppendWithIndent("public static string GetDescription(this ").Append(_enumItem.Name).AppendLine(" enumValue)");
            AppendLineWithIndent("{");
            IncrementIndent();
        }

        private void AppendMethodGetDescriptionBody()
        {
            AppendLineWithIndent("return enumValue switch");
            AppendLineWithIndent("{");
            IncrementIndent();
            foreach (var enumValue in _enumItem.KeyValues)
            {
                AppendWithIndent(_enumItem.Name).Append('.').Append(enumValue.Key).Append(" => \"").Append(enumValue.Value).AppendLine("\",");
            }
            AppendLineWithIndent("_ => throw new ArgumentException($\"{enumValue} does not have an description attribute\")");
            DecrementIndent();
            AppendLineWithIndent("};");
        }

        private void AppendEndGetDescriptionMethod()
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
