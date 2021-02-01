using Jh.SourceGenerator.Common.CodeBuilders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace Jh.SourceGenerator
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
                    var tableDto = receiver.GetTableDto(table);
                    var code = new IRepositoryCodeBuilder(tableDto,filePath:"");
                    // inject the created source into the users compilation
                    context.AddSource(tableDto.Name, SourceText.From(code.ToString(), Encoding.UTF8));
                }
            }
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new IRepositorySyntaxReceiver());
        }
    }
}
