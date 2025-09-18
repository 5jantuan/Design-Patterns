using System;
using System.Collections;
using System.Dynamic;

//экземпляр класса Database
var db = new Database();

db.initDb();



var maskForMerge = new PatientFieldMask
{
    Name = true,
};

var mask = new PatientFieldMask
{
    Id = true,
    Name = true,
    Age = true,
    Weight = true,
};

//db.FindByName("Jane Smith", mask);

List<Patient> patients = db.GetAll();
var mergeTest = db.MergeDublicates(maskForMerge);

foreach (var patient in mergeTest)
{
    Database.Print(patient, mask);
    Console.WriteLine();
}



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
    // public required Gender[] Gender;
    public required Gender Gender;

    

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

    public Patient Create(string name, int age, float weight, Gender gender)
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

    public List<Patient> GetAll()
    {
        return _patients;
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

    public float GetAverageWeight()
    {
        if (_patients.Count == 0) return 0;

        float totalWeight = 0;
        foreach (var patient in _patients)
        {
            totalWeight += patient.Weight;
        }

        return totalWeight / _patients.Count;
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

    public void initDb()
    {
        var patient1 = Create("John Doe", 30, 70.5f, Gender.Male);
        var patient2 = Create("John Doe", 25, 60.1f, Gender.Female);
        //var patient3 = Create("Alex Johnson", 40, 80.4f, Gender.Male);
       // var patient4 = Create("Emily Davis", 35, 55.1f, Gender.Female);
        //var patient5 = Create("Chris Brown", 28, 75.9f, Gender.Male);


    }

    /*   public List<Patient> MergeDublicates(PatientFieldMask? mask = null)
       {
           List<Patient> uniquePatients = new List<Patient>();

           foreach (var patient in _patients)
           {
               if (uniquePatients.TryGetValue(patient.Name, out var existingPatient))
               {
                   // Merge logic based on the mask
                   if (mask is not null)
                   {
                       if (mask.Age && patient.Age != 0)
                       {
                           existingPatient.Age = patient.Age;
                       }
                       if (mask.Weight && patient.Weight != 0)
                       {
                           existingPatient.Weight = patient.Weight;
                       }
                   }
               }
           }
           return uniquePatients.Values.ToList();
       }*/

    public List<Patient> MergeDublicates(PatientFieldMask? mask = null)
    {

        List<Patient> allPatients = GetAll();

        List<Patient> uniquePatients = new List<Patient>();

        foreach (var patient in allPatients)
        {
            if (!uniquePatients.Any(p => equalByMask(p, patient, mask)))
            {
                uniquePatients.Add(patient);
            }
            else
            { 
                var existingPatient = uniquePatients.First(p => equalByMask(p, patient, mask));
                existingPatient.Weight = GetAverageWeight();
            }
        }
        return uniquePatients;
    }

    public bool equalByMask(Patient p1, Patient p2, PatientFieldMask? mask = null)
    {
        if (mask is null) return false;

        if (mask.Id && p1.Id != p2.Id) return false;
        if (mask.Name && p1.Name != p2.Name) return false;
        if (mask.Age && p1.Age != p2.Age) return false;
        if (mask.Weight && p1.Weight != p2.Weight) return false;

        return true;
    }
}