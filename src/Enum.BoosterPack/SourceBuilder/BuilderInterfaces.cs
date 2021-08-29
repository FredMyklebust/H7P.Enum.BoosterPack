using System.Collections.Generic;

namespace H7P.Enum.BoosterPack.SourceBuilder
{
    public interface INewExtension
    {
        IUsings NewExtension(string extendsType);
    }

    public interface IUsings
    {
        IUsings AddUsing(string namespaceName);
        IUsings AddUsings(string[] namespaces);
        INamespace InNamepace(string namespaceName);
    }

    public interface INamespace
    {
        IExtensionMethodSummary AddExtensionClass(string accessModifier, string className);
    }

    public interface IExtensionMethodSummary
    {
        IExtensionTypeParam AddExtensionSummaryFormat(string format, string arg);

        IImplementation AddExtensionMethod(string methodName, string returnType, string typeParameterName, Parameter parameter = null);

    }

    public interface IExtensionTypeParam
    {
        IAdditonalParam AddTypedParamTag(string paramName, string description);
    }

    public interface IAdditonalParam
    {
        IExceptionsSummary AddParamTags(List<Parameter> @params);

        IReturnsDescription AddExceptionTags(List<ExceptionSummary> exceptions);

        IExtensionClass AddReturnsTag(string description);
    }

    public interface IExceptionsSummary
    {
        IReturnsDescription AddExceptionTags(List<ExceptionSummary> exceptions);
    }

    public interface IReturnsDescription
    {
        IExtensionClass AddReturnsTag(string description);
    }

    public interface IExtensionClass
    {
        IImplementation AddExtensionMethod(string methodName, string returnType, string typeParameterName, Parameter parameter = null);
    }

    public interface IImplementation
    {
        ICase BeginSwitchOn(string switchName);
    }

    public interface ICase
    {
        ICase AddReturnCases(List<ReturnCaseStatement> cases);
        IDefaultCase DefaultCaseThrowsArgumentException();
    }

    public interface IDefaultCase
    {
        IEndExtension EndExtensionMethod();
    }

    public interface IEndExtension
    {
        IEndNamespace EndNamespace();
    }

    public interface IEndNamespace
    {
        string Build();
    }

    public class Parameter
    {
        public Parameter(string paramTypeName, string paramName, string summary)
        {
            TypeName = paramTypeName;
            Name = paramName;
            Summary = summary;
        }

        public string TypeName { get; }
        public string Name { get; }
        public string Summary { get; }
    }

    public class ReturnCaseStatement
    {
        public ReturnCaseStatement(string caseMatch, string returnValue)
        {
            CaseMatch = caseMatch;
            ReturnValue = returnValue;
        }

        public string CaseMatch { get; }
        public string ReturnValue { get; }
    }

    public class ExceptionSummary
    {
        public ExceptionSummary(string exception, string summary)
        {
            Exception = exception;
            Summary = summary;
        }

        public string Exception { get; }
        public string Summary { get; }
    }
}
