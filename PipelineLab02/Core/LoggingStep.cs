using System;
using System.Text;
using Pipeline.Interfaces;

namespace Pipeline.Core;

public sealed class LoggingStep<TContext> : IPipelineStep<TContext>
{
    private readonly IPipelineStep<TContext> _step;

    public LoggingStep(IPipelineStep<TContext> step)
    {
        _step = step;
    }

    public void Execute(TContext context)
    {
        Console.WriteLine($"[Before] {_step.GetType().Name}");
        _step.Execute(context);
        Console.WriteLine($"[After] {_step.GetType().Name}");
    }

    public void Introspect(StringBuilder sb, int indent = 0)
    {
        sb.AppendLine($"{new string(' ', indent)}Logging({_step.GetType().Name})");
        _step.Introspect(sb, indent + 2);
    }
}
