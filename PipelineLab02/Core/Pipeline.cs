using System;
using System.Collections.Generic;
using System.Text;
using Pipeline.Interfaces;

namespace Pipeline.Core;

public sealed class Pipeline<TContext>
{
    public List<IPipelineStep<TContext>> Steps = new();

    public void Execute(TContext context)
    {
        foreach (var step in Steps)
        {
            step.Execute(context);
            if (context is ICanStop stop && stop.IsDone)
                break;
        }
    }

    public void Describe(StringBuilderIndent sb)
    {
        sb.AppendLine("Pipeline:");
        sb.IncreaseIndent();
        foreach (var step in Steps)
        {
            step.Introspect(sb);
        }
        sb.DecreaseIndent();
    }

    public static void PrintPipeline(Pipeline<TContext> pipeline)
    {
        var sb = new StringBuilder();
        pipeline.Describe(sb);
        Console.WriteLine(sb.ToString());
    }
}

public abstract class StringBuilderIndent
{
    public StringBuilder? Sb;
    public int indent = 0;

    public int IncreaseIndent() => indent++;

    public int DecreaseIndent() => indent--;

    public virtual void AppendLine(string s)
    {
        if (Sb == null) Sb = new StringBuilder();

        for (int i = 0; i < indent; i++)
            Sb.Append("    ");

        Sb.AppendLine(s);
    }
}