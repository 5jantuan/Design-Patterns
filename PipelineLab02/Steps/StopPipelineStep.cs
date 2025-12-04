using System.Text;
using Pipeline.Contexts;
using Pipeline.Functions;
using Pipeline.Interfaces;
using Pipeline.Models;


namespace Pipeline.Steps;
public sealed class StopPipelineStep : IPipelineStep<PatientContext>
{
    public void Execute(PatientContext context)
    {
        Console.WriteLine("StopPipelineStep: setting context.IsDone = true");
        context.IsDone = true;
    }

    public void Introspect(StringBuilder sb, int indent = 0)
    {
        sb.AppendLine($"{new string(' ', indent)}StopPipelineStep (sets IsDone = true)");
    }
}
