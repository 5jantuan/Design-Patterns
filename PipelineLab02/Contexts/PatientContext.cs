using System.Collections.Generic;
using Pipeline.Interfaces;
using Pipeline.Models;

namespace Pipeline.Contexts;

public sealed class PatientContext : ICanStop
{
    public List<Doctor> PreviousDoctors = new();
    public required Patient Patient;
    public bool IsDone { get; set; }
}
