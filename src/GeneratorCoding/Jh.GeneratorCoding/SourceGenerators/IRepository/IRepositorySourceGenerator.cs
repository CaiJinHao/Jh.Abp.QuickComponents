using Jh.GeneratorCoding.CodeBuilders;
using Jh.SourceGenerator.Common.GeneratorAttributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Text;

namespace Jh.GeneratorCoding.SourceGenerators
{
    [Generator]
    public class IRepositorySourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            System.Diagnostics.Debugger.Launch();
            if (context.SyntaxReceiver is IRepositorySyntaxReceiver receiver)
            {
                foreach (var table in receiver.CandidateClassCollection)
                {
                    var code = new IRepositoryCodeBuilder(receiver.GetTableDto(table));
                    // inject the created source into the users compilation
                    context.AddSource("codename", SourceText.From(code.ToString(), Encoding.UTF8));
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new IRepositorySyntaxReceiver());
        }
    }
}
