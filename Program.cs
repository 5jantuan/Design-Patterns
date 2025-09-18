using System;

//экземпляр класса Database
var db = new Database();

var patient1 = db.Create("John Doe", 30, 70.5f, [Gender.Male]);
var patient2 = db.Create("Jane Smith", 25, 60.1f, [Gender.Female]);
var patient3 = db.Create("Alex Johnson", 40, 80.4f, [Gender.Male]);
var patient4 = db.Create("Emily Davis", 35, 55.1f, [Gender.Female]);
var patient5 = db.Create("Chris Brown", 28, 75.9f, [Gender.Male]);

var mask = new PatientFieldMask
{
    Id = true,
    Name = true,
    Age = true
};

db.FindByName("Jane Smith", mask);
Database.Print(patient1, mask);


public enum Gender
{
    Male,
    Female
}

//domain model( task 1)
public sealed class Patient
{
    private static int _nextId = 1;

    public int Id;
    public required string Name;
    public required int Age;
    public required float Weight;
    public required Gender[] Gender;

    //id is auto-generated
    public Patient()
    {
        Id = _nextId++;
    }
}

//Field mask(task 3)
public sealed class PatientFieldMask
{
    public bool Id;
    public bool Name;
    public bool Age;
    public bool Weight;
    public bool Gender;
}

//db simulation( task 2)
public sealed class Database
{

    public readonly List<Patient> _patients = new();

    // public IEnumerable<Patient> Patients => _patients;

    public Patient Create(string name, int age, float weight, Gender[] gender)
    {
        var patient = new Patient
        {
            Name = name,
            Age = age,
            Weight = weight,
            Gender = gender,
        };

        _patients.Add(patient);
        return patient;
    }

    //static method to print patient info with field mask(task 5)
    public static void Print(
        Patient patient,
        PatientFieldMask? mask = null)
    {
        if (mask is not { } m)
        {
            m = new()
            {
                Id = true,
                Name = true,
                Age = true,
                Weight = true,
                Gender = true,
            };
        }

        if (m.Id)
        {
            Console.WriteLine($"Id: {patient.Id}");
        }

        if (m.Name)
        {
            Console.WriteLine($"Name: {patient.Name}");
        }
        if (m.Age)
        {
            Console.WriteLine($"Age: {patient.Age}");
        }
        if (m.Weight)
        {
            Console.WriteLine($"Weight: {patient.Weight}");
        }
        if (m.Gender)
        {
            Console.WriteLine($"Gender: {string.Join(", ", patient.Gender)}");
        }
    }

    public void PrintAll(PatientFieldMask? mask = null)
    {
        foreach (var patient in _patients)
        {
            Print(patient, mask);
            Console.WriteLine();
        }
    }

    // method to find patient by name and print info with field mask(task 4)
    public void FindByName(string name, PatientFieldMask? mask = null)
    {
        foreach (var patient in _patients)
        {
            if (patient.Name == name)
            {
                Print(patient, mask);
                Console.WriteLine();
            }
        }
    }

    public void MergeDublicates(PatientFieldMask? mask = null)
    {
        var uniquePatients = new Dictionary<string, Patient>();


    }

}