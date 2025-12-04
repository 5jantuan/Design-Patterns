using System;
using System.Diagnostics;
using System.Text;
using Pipeline.Interfaces;

namespace Pipeline.Core;

public sealed class TimingStep<TContext> : IPipelineStep<TContext>
{
    private readonly IPipelineStep<TContext> _step;

    public TimingStep(IPipelineStep<TContext> step)
    {
        _step = step;
    }

    public void Execute(TContext context)
    {
        var sw = Stopwatch.StartNew();
        _step.Execute(context);
        sw.Stop();
        Console.WriteLine($"[{_step.GetType().Name}] took {sw.ElapsedMilliseconds} ms");
    }

    public void Introspect(StringBuilder sb, int indent = 0)
    {
        sb.AppendLine($"{new string(' ', indent)}Timing({_step.GetType().Name})");
        _step.Introspect(sb, indent + 2);
    }
}
