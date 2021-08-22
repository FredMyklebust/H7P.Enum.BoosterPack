using Microsoft.CodeAnalysis.Text;
using System;
using System.IO;
using System.Text;

namespace H7P.Enum.BoosterPack.IntegrationTests
{
    public static class TestSourceCreator
    {
        public static (Type, string, SourceText) CreateExpectedSourceFromFile<T>(string generatedName, string testContext, string expectedSourceFile)
        {
            var sourceText = GetSourceContent(testContext, expectedSourceFile);
            return CreateExpectedSourceFromText<T>(generatedName, sourceText);
        }

        public static (Type, string, SourceText) CreateExpectedSourceFromText<T>(string generatedName, string sourceText)
        {
            return (typeof(T), generatedName, SourceText.From(sourceText, Encoding.UTF8, SourceHashAlgorithm.Sha256));
        }

        public static string GetSourceContent(string testContext, string fileName)
        {
            return File.ReadAllText($"{testContext}\\Data\\{fileName}");
        }
    }

}
