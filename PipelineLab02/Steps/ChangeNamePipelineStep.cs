using System.Text;
using Pipeline.Contexts;
using Pipeline.Functions;
using Pipeline.Interfaces;

namespace Pipeline.Steps;

public sealed class ChangeNamePipelineStep : IPipelineStep<PatientContext>
{
    public required string Name;

    public void Execute(PatientContext context)
    {
        PipelineFunctions.ChangeName_Adapter(context, Name);
    }

    public void Introspect(StringBuilder sb, int indent = 0)
    {
        sb.AppendLine($"Change name({Name})");
    }
}
