using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Data.Dta;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Dsw2026Ej15.Data;

public class PersistenciaInMemory : IPersistence
{
    private readonly List<Doctor> Doctors = [];
    private readonly List<Speciality> Specialities = [];

    public PersistenciaInMemory(){}

    public static async Task<PersistenciaInMemory> InitializeDataAsync()
    {
        var persistencia = new PersistenciaInMemory();
        await persistencia.InitializeDoctors();
        return persistencia;
    }
    public async Task InitializeDoctors()
    {
        await InitializeSpecialities();
        var s1 = Specialities.Find(s => s.Name == "Pediatría");
        var s2 = Specialities.Find(s => s.Name == "Neurología");
        var s3 = Specialities.Find(s => s.Name == "Cardiología");

        var d1 = new Doctor(Guid.Parse("31b4fc6c - 8594 - 4b51 - 93a0 - 7b2432a51f89"), "Joaquin Valdecantos", "MN-45213", true, s1);
        var d2 = new Doctor(Guid.Parse("31b4fc67 - 8594 - 4b51 - 93a0 - 7b2432a51f89"), "Juan Tenreyro", "MM-45213", false, s2);
        var d3 = new Doctor(Guid.Parse("31b4fc62 - 8594 - 4b51 - 93a0 - 7b2432a51f89"), "Tadeo Torres", "MR-45213", true, s3);
        
        Doctors.Add(d1);
        Doctors.Add(d2);
        Doctors.Add(d3);
    }
    public async Task InitializeSpecialities()
    {
        var specialitiesData = await LoadSpecialities<SpecialityDta>("specialities");
        if(specialitiesData != null)
        {
            foreach (var data in specialitiesData)
            {
                Speciality s = new Speciality(data.Id,data.Name, data.Description);
                Specialities.Add(s);
            }
        }
    }
    private async Task<List<T>?> LoadSpecialities<T>(string file)
    {
        string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", $"{file}.json");
        var jsonContent = await File.ReadAllTextAsync(jsonPath);
        return JsonSerializer.Deserialize<List<T>>(jsonContent);
    }
}
