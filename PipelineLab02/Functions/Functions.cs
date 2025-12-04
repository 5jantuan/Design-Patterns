// Functions/PipelineFunctions.cs
using System;
using System.Collections.Generic;
using Pipeline.Contexts;
using Pipeline.Core;
using Pipeline.Models;
using Pipeline.Steps;

namespace Pipeline.Functions;

public static class PipelineFunctions
{
    public static Pipeline<PatientContext> CreatePipeline()
    {
        var pipeline = new Pipeline<PatientContext>();

        var doctor = new Doctor { Name = "Pacifica Nortwest" };

        pipeline.Steps.Add(new PrintPatientPipelineStep());
        pipeline.Steps.Add(new ChangeDoctorPipelineStep { Doctor = doctor });
        pipeline.Steps.Add(new ChangeNamePipelineStep { Name = "Doe John" });
        pipeline.Steps.Add(new PrintPatientPipelineStep());
        pipeline.Steps.Add(new PrintDoctorsPipelineStep());

        return pipeline;
    }

    public static void ChangeName_Adapter(PatientContext context, string name)
        => ChangeName(context.Patient, name);

    public static void ChangeName(Patient patient, string name)
    {
        Console.WriteLine("Changing Name");
        patient.Name = name;
    }

    public static void ChangeDoctor_Adapter(PatientContext context, Doctor? newDoctor)
        => ChangeDoctor(context.Patient, context.PreviousDoctors, newDoctor);

    public static void ChangeDoctor(Patient patient, List<Doctor> previousDoctors, Doctor? newDoctor)
    {
        Console.WriteLine("Changing Doctor");
        if (patient.Doctor != null)
            previousDoctors.Add(patient.Doctor);

        patient.Doctor = newDoctor;
    }

    public static void PrintDoctors_Adapter(PatientContext context)
    {
        Console.WriteLine("Previous Doctors:");
        foreach (var o in context.PreviousDoctors)
            Console.WriteLine(o.Name);
    }

    public static void PrintPatient_Adapter(PatientContext context)
    {
        Console.WriteLine("Patient Information:");
        Console.WriteLine($"Name: {context.Patient.Name}");
        Console.WriteLine($"Doctor: {context.Patient.Doctor?.Name ?? "<no doctor>"}");
    }
}
