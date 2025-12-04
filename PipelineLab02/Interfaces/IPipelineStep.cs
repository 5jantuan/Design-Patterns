using System.Text;

namespace Pipeline.Interfaces;

public interface IPipelineStep<TContext>
{
    void Execute(TContext context);
    void Introspect(StringBuilder sb, int indent = 0);
}
