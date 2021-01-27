using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jh.GeneratorCoding.SyntaxReceivers
{
    public class IRepositorySyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> CandidateClassCollection { get; } = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax
                   && classDeclarationSyntax.AttributeLists.Count > 0)
            {
                //找到domain类
                CandidateClassCollection.Add(classDeclarationSyntax);
            }
        }
    }
}
