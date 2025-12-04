using System.Text;
using Pipeline.Contexts;
using Pipeline.Functions;
using Pipeline.Interfaces;
using Pipeline.Models;


namespace Pipeline.Steps;
// Меняет лечащего врача пациента
sealed class ChangeDoctorPipelineStep : IPipelineStep<PatientContext>
{
    public required Doctor? Doctor;

    public void Execute(PatientContext context)
    {
        PipelineFunctions.ChangeDoctor_Adapter(context, Doctor);
    }

    public void Introspect(StringBuilder sb, int indent = 0)
    {
        sb.AppendLine($"{new string(' ', indent)}Change doctor({Doctor?.Name})");
    }
}
