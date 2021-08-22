using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace H7P.Enum.BoosterPack.AutoDescriptor.SourceGenerator
{
    internal class DescribableEnumSyntaxFinder : ISyntaxReceiver
    {
        public List<(string Namespace, EnumDeclarationSyntax EnumDeclaration)> DescribableEnums { get; } = new();

        //identify and store enums marked for extraction for later processing
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is EnumDeclarationSyntax enumNode
                && enumNode.AttributeLists.Any(x => x.Attributes.Any(y => y.Name.ToString().Equals(Indentifiers.DescribableAttributeShortName)
                                                                     || y.Name.ToString().Equals(Indentifiers.DescribableAttributeComplete))))
            {
                if (enumNode.Ancestors()
                            .FirstOrDefault(a => a.IsKind(SyntaxKind.NamespaceDeclaration)) is not NamespaceDeclarationSyntax enumNamespace)
                {
                    return;
                }

                var namespaceName = enumNamespace.Name.ToString();
                DescribableEnums.Add((namespaceName, enumNode));
            }
        }
    }
}
