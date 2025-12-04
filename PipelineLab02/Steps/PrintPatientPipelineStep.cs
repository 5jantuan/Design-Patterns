using System.Text;
using Pipeline.Contexts;
using Pipeline.Functions;
using Pipeline.Interfaces;

namespace Pipeline.Steps;

public sealed class PrintPatientPipelineStep : IPipelineStep<PatientContext>
{
    public void Execute(PatientContext context)
    {
        PipelineFunctions.PrintPatient_Adapter(context);
    }

    public void Introspect(StringBuilder sb, int indent = 0)
    {
        sb.AppendLine($"{new string(' ', indent)}Print Patient info");
    }
}
