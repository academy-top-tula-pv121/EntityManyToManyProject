using EntityManyToManyProject;
using Microsoft.EntityFrameworkCore;

using (AmbulanceContext ambulance = new AmbulanceContext())
{
    ambulance.Database.EnsureDeleted();
    ambulance.Database.EnsureCreated();

    List<Doctor> doctors = new();
    doctors.Add(new() { Name = "Aybolit" });
    doctors.Add(new() { Name = "Livsi" });
    doctors.Add(new() { Name = "Watson" });

    List<Patient> patients = new();
    patients.Add(new() { Name = "Tom" });
    patients.Add(new() { Name = "Bob" });
    patients.Add(new() { Name = "Joe" });
    patients.Add(new() { Name = "Sam" });
    patients.Add(new() { Name = "Jim" });
    patients.Add(new() { Name = "Tim" });

    ambulance.Doctors.AddRange(doctors);
    ambulance.Patients.AddRange(patients);

    doctors[0].Patients.Add(patients[4]);
    doctors[0].Patients.Add(patients[1]);

    doctors[1].Patients.Add(patients[0]);
    doctors[1].Patients.Add(patients[2]);
    doctors[0].Patients.Add(patients[5]);

    doctors[2].Patients.Add(patients[1]);
    doctors[2].Patients.Add(patients[3]);
    doctors[2].Patients.Add(patients[5]);
    doctors[2].Patients.Add(patients[4]);
    doctors[2].Patients.Add(patients[2]);

    ambulance.SaveChanges();

}

using (AmbulanceContext ambulance = new AmbulanceContext())
{
    var doctors = ambulance.Doctors
                            .Include(d => d.Patients)
                            .ToList();
    foreach(var doctor in doctors)
    {
        Console.WriteLine(doctor.Name);
        foreach(var patient in doctor.Patients)
            Console.WriteLine($"\t{patient.Name}");
    }

    Console.WriteLine("-------------------------");

    var patients = ambulance.Patients
                            .Include(p => p.Doctors)
                            .ToList();
    foreach (var patient in patients)
    {
        Console.WriteLine(patient.Name);
        foreach (var doctor in patient.Doctors)
            Console.WriteLine($"\t{doctor.Name}");
    }
}

Console.WriteLine("-------------------------");
Console.WriteLine("-------------------------");

using (AmbulanceContext ambulance = new AmbulanceContext())
{
    Doctor? doctor = ambulance.Doctors.FirstOrDefault(d => d.Name == "Livsi");
    ambulance.Doctors.Remove(doctor);

    Patient? patient = new() { Name = "Leo" };
    var doctors = ambulance.Doctors.ToList();
    patient.Doctors.Add(doctors[0]);
    patient.Doctors.Add(doctors[2]);
    ambulance.Patients.Add(patient);

    ambulance.SaveChanges();
}


using (AmbulanceContext ambulance = new AmbulanceContext())
{
    var doctors = ambulance.Doctors
                            .Include(d => d.Patients)
                            .ToList();
    foreach (var doctor in doctors)
    {
        Console.WriteLine(doctor.Name);
        foreach (var patient in doctor.Patients)
            Console.WriteLine($"\t{patient.Name}");
    }

    Console.WriteLine("-------------------------");

    var patients = ambulance.Patients
                            .Include(p => p.Doctors)
                            .ToList();
    foreach (var patient in patients)
    {
        Console.WriteLine(patient.Name);
        foreach (var doctor in patient.Doctors)
            Console.WriteLine($"\t{doctor.Name}");
    }

    //Console.WriteLine("-------------------------");

    //patients = ambulance.Patients
    //                    .Include(p => p.Doctors)
    //                        .ThenInclude(d => d != null)
    //                    .ToList();
    //foreach (var patient in patients)
    //{
    //    Console.WriteLine(patient.Name);
    //    foreach (var doctor in patient.Doctors)
    //        Console.WriteLine($"\t{doctor.Name}");
    //}
}