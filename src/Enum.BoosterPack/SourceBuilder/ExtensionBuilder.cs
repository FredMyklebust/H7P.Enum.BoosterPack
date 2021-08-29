using System.Collections.Generic;
using System.Text;

namespace H7P.Enum.BoosterPack.SourceBuilder
{

    public sealed class ExtensionBuilder :
                    IUsings,
                    INamespace,
                    IExtensionClass,
                    IExtensionMethodSummary,
                    IExtensionTypeParam,
                    IAdditonalParam,
                    IExceptionsSummary,
                    IReturnsDescription,
                    IImplementation,
                    ICase,
                    IDefaultCase,
                    IEndExtension,
                    IEndNamespace
    {
        private readonly StringBuilder _extensionBuilder = new();
        private readonly string _typeToExtend;
        private string _switchParam;
        private int _indent = 0;

        private ExtensionBuilder(string extendsType)
        {
            _typeToExtend = extendsType;
        }

        public static IUsings NewExtension(string extendsType)
        {
            return new ExtensionBuilder(extendsType);
        }

        public IUsings AddUsing(string @namespace)
        {
            Append("using ").Append(@namespace).AppendLine(";");

            return this;
        }

        public IUsings AddUsings(string[] namespaces)
        {
            foreach (var @namespace in namespaces)
            {
                AddUsing(@namespace);
            }

            return this;
        }

        public INamespace InNamepace(string namespaceName)
        {
            AppendLine();
            Append("namespace ").AppendLine(namespaceName);
            AppendLine("{");
            IncrementIndent();

            return this;
        }

        public IExtensionMethodSummary AddExtensionClass(string accessModifier, string className)
        {
            //prefix class-name with unicode to prevent name-collisions - Ɠ - Ɠenerated
            Append(accessModifier).Append(" static class Ɠ").AppendLine(className);
            BeginBlock();

            return this;
        }

        public IExtensionTypeParam AddExtensionSummaryFormat(string format, string arg)
        {
            AppendLine("/// <summary>");
            Append("/// ").AppendFormat(format, arg).AppendLine();
            AppendLine("/// </summary>");

            return this;
        }

        public IAdditonalParam AddTypedParamTag(string paramName, string description)
        {
            Append("/// <param name=\"").Append(paramName).Append("\">").Append(description).AppendLine("</param>");

            return this;
        }

        public IExceptionsSummary AddParamTags(List<Parameter> @params)
        {
            foreach (var param in @params)
            {
                Append("/// <param name=\"").Append(param.Name).Append("\">").Append(param.Summary).AppendLine("</param>");
            }

            return this;
        }

        public IReturnsDescription AddExceptionTags(List<ExceptionSummary> exceptions)
        {
            foreach (var exception in exceptions)
            {
                Append("/// <exception cref=\"").Append(exception.Exception).Append("\">").Append(exception.Summary).AppendLine("</exception>");
            }

            return this;
        }

        public IExtensionClass AddReturnsTag(string description)
        {
            AppendLine("/// <returns>");
            Append("///").AppendLine(description);
            AppendLine("/// </returns>");

            return this;
        }

        public IImplementation AddExtensionMethod(string methodName, string returnType, string typeParameterName, Parameter parameter = null)
        {
            Append("public static ").Append(returnType).AppendSpace().Append(methodName).Append("(this ").Append(_typeToExtend).AppendSpace().Append(typeParameterName);
            if (parameter != null)
            {
                Append(_typeToExtend).Append(", ").Append(parameter.TypeName).AppendSpace().Append(parameter.Name);
            }
            AppendLine(")", false);
            BeginBlock();

            return this;
        }

        public ICase BeginSwitchOn(string switchParam)
        {
            _switchParam = switchParam;
            Append("switch(").Append(switchParam).AppendLine(")");
            BeginBlock();

            return this;
        }

        public ICase AddReturnCases(List<ReturnCaseStatement> cases)
        {
            foreach (var @case in cases)
            {
                Append("case ").Append(@case.CaseMatch).Append(": return ").Append(@case.ReturnValue).AppendLine(";");
            }
            return this;
        }

        public IDefaultCase DefaultCaseThrowsArgumentException()
        {
            Append("default: throw new ArgumentException($\"Invalid value {").Append(_switchParam).Append("} specified\", \"").Append(_switchParam).AppendLine("\");");
            EndBlock();

            return this;
        }

        public IEndExtension EndExtensionMethod()
        {
            EndBlock();

            return this;
        }

        public IEndNamespace EndNamespace()
        {
            EndBlock();
            return this;
        }

        public string Build()
        {
            EndBlock();
            return _extensionBuilder.ToString();
        }

        private void BeginBlock()
        {
            Indent();
            _extensionBuilder.AppendLine("{");
            IncrementIndent();
        }

        private void EndBlock()
        {
            DecrementIndent();
            Indent();
            _extensionBuilder.AppendLine("}");
        }

        private void IncrementIndent()
        {
            _indent++;
        }

        private void DecrementIndent()
        {
            _indent--;
        }

        private void Indent()
        {
            if (_indent == 0)
            {
                return;
            }

            _extensionBuilder.Append('\t', _indent);
        }

        public StringBuilder Append(string code)
        {
            Indent();
            _extensionBuilder.Append(code);
            return _extensionBuilder;
        }

        public StringBuilder AppendSpace()
        {
            _extensionBuilder.Append(' ');
            return _extensionBuilder;
        }


        public StringBuilder AppendLine()
        {
            Indent();
            _extensionBuilder.AppendLine();
            return _extensionBuilder;
        }

        public StringBuilder AppendLine(string code, bool indent = true)
        {
            if (indent)
            {
                Indent();
            }

            _extensionBuilder.AppendLine(code);
            return _extensionBuilder;
        }
    }
}