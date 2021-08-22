using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace H7P.AutoEnumDescriptor.SourceGenerator.FastString.SourceGenerator
{
    internal class EnumSyntaxFinder : ISyntaxReceiver
    {
        public List<(string Namespace, EnumDeclarationSyntax EnumDeclaration)> Enums { get; } = new();

        //identify and store all enums
        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is EnumDeclarationSyntax enumNode)
            {
                if (enumNode.Ancestors()
                            .FirstOrDefault(a => a.IsKind(SyntaxKind.NamespaceDeclaration)) is not NamespaceDeclarationSyntax enumNamespace)
                {
                    return;
                }

                var namespaceName = enumNamespace.Name.ToString();
                Enums.Add((namespaceName, enumNode));
            }
        }
    }
}
