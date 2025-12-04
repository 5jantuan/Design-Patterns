using System.Text;
using Pipeline.Contexts;
using Pipeline.Functions;
using Pipeline.Interfaces;

namespace Pipeline.Steps;

public sealed class PrintDoctorsPipelineStep : IPipelineStep<PatientContext>
{
    public void Execute(PatientContext context)
    {
        PipelineFunctions.PrintDoctors_Adapter(context);
    }

    public void Introspect(StringBuilder sb, int indent = 0)
    {
        sb.AppendLine($"{new string(' ', indent)}Print doctors");
    }
}
