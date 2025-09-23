using System;

//экземпляр класса Database
var db = new Database();

var patient1 = new Patient("John Doe", 30, 70.5f, GenderEnum.Male);
db.Add(patient1);
var patient2 = new Patient("Jane Smith", 25, 60.1f, GenderEnum.Female);
db.Add(patient2);
var patient3 = new Patient("John Doe", 30, 72.0f, GenderEnum.Male);
db.Add(patient3);
var patient4 = new Patient("Alice Johnson", 40, 80.3f, GenderEnum.Female);
db.Add(patient4);

var mask = new PatientFieldMask
{
    Name = true,
    Age = true
};

var ageMask = new PatientFieldMask
{
    Age = true
};

var biteMask = PatientFields.Name | PatientFields.Age;

var mergedPatient = Patient.MergePatients(patient1, patient2, biteMask);
Console.WriteLine("Merged Patient:");
Patient.PrintByBoolMask(mergedPatient);


// Patient.EqualByMask(patient1, patient3, mask);
// bool areEqual = Patient.EqualByMask(patient1, patient2, mask);
// Console.WriteLine($"Are patient1 and patient2 equal by mask? {areEqual}\n");

// Patient.CopyFiedsByMask(patient2, patient1, mask);
// Console.WriteLine("After copying fields by mask from patient2 to patient1:");
// Patient.PrintByBoolMask(patient1);

// var patients = new List<Patient> { patient1, patient2, patient3, patient4 };

// Patient.Shit(
//     patient1,
//     patient4,
//     mask,
//     ageMask,
//     patients);

// Patient.PrintByBoolMask(patient3, mask);



public enum GenderEnum
{
    Male,
    Female
}

//domain model( task 1)
public sealed class Patient
{
    private static int _counterId = 1;

    public int Id;
    public string Name;
    public int Age;
    public float Weight;
    public GenderEnum Gender;

    //id is auto-generated
    public Patient(string name, int age, float weight, GenderEnum gender)
    {
        Id = _counterId++;
        Name = name;
        Age = age;
        Weight = weight;
        Gender = gender;
    }

    // Конструктор копирования
    public Patient(Patient other)
    {
        Id = _counterId++;      // новый уникальный Id
        Name = other.Name;
        Age = other.Age;
        Weight = other.Weight;
        Gender = other.Gender;
    }

    public static void PrintByBoolMask(
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

    public static bool EqualByMask(
        Patient p1,
        Patient p2,
        PatientFieldMask mask)
    {
        if (mask.Id && p1.Id != p2.Id)
        {
            return false;
        }

        if (mask.Name && p1.Name != p2.Name)
        {
            return false;
        }

        if (mask.Age && p1.Age != p2.Age)
        {
            return false;
        }

        if (mask.Weight && Math.Abs(p1.Weight - p2.Weight) > 0.01f)
        {
            return false;
        }

        if (mask.Gender && p1.Gender != p2.Gender)
        {
            return false;
        }

        return true;
    }

    public static void CopyFiedsByMask(
        Patient source,
        Patient target,
        PatientFieldMask mask)
    {
        if (mask.Id)
        {
            target.Id = source.Id;
        }

        if (mask.Name)
        {
            target.Name = source.Name;
        }

        if (mask.Age)
        {
            target.Age = source.Age;
        }

        if (mask.Weight)
        {
            target.Weight = source.Weight;
        }

        if (mask.Gender)
        {
            target.Gender = source.Gender;
        }
    }

    public static void Shit(
        Patient comparePatient,
        Patient copyPatient,
        PatientFieldMask compareMask,
        PatientFieldMask copyMask,
        List<Patient> patients)
    {
        Patient comparePatientTemp = new Patient(comparePatient);

        for (int i = 0; i < patients.Count; i++)
        {
            if (EqualByMask(patients[i], comparePatientTemp, compareMask))
            {
                CopyFiedsByMask(copyPatient, patients[i], copyMask);
            }
        }
    }

    public static Patient MergePatients(Patient a, Patient b, PatientFields mask)
    {
        return new Patient(
            name:   (mask & PatientFields.Name)   != 0 ? b.Name   : a.Name,
            age:    (mask & PatientFields.Age)    != 0 ? b.Age    : a.Age,
            weight: (mask & PatientFields.Weight) != 0 ? b.Weight : a.Weight,
            gender: (mask & PatientFields.Gender) != 0 ? b.Gender : a.Gender
        );
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

public enum PatientFields
{
    None = 0,
    Name = 1 << 0,
    Age = 1 << 1,
    Weight = 1 << 2,
    Gender = 1 << 3,
    All = Name | Age | Weight | Gender
}

//db simulation( task 2)
public sealed class Database
{

    public readonly List<Patient> _patients = new();

    // public IEnumerable<Patient> Patients => _patients;

    public Patient Add(Patient patient)
    {
        _patients.Add(patient);
        return patient;
    }

    // method to find patient by name and print info with field mask(task 4)
    public void FindByName(string name, PatientFieldMask? mask = null)
    {
        foreach (var patient in _patients)
        {
            if (patient.Name == name)
            {
                Patient.PrintByBoolMask(patient, mask);
                Console.WriteLine();
            }
        }
    }
    
}