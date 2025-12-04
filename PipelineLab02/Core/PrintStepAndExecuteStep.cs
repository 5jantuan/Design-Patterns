using System;
using System.Text;
using Pipeline.Interfaces;

namespace Pipeline.Core;

public sealed class PrintStepAndExecuteStep<TContext> : IPipelineStep<TContext>
{
    private readonly IPipelineStep<TContext> _step;

    public PrintStepAndExecuteStep(IPipelineStep<TContext> step)
    {
        _step = step;
    }

    public void Execute(TContext context)
    {
        var sb = new StringBuilder();
        _step.Introspect(sb);
        Console.WriteLine(sb.ToString());
        _step.Execute(context);
    }

    public void Introspect(StringBuilder sb, int indent = 0)
    {
        sb.AppendLine($"{new string(' ', indent)}PrintStep wrapper:");
        _step.Introspect(sb, indent + 2);
    }
}
