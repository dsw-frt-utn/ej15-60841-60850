using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
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
        await persistencia.InitializeSpecialities();
        await persistencia.InitializeDoctors();
        return persistencia;
    }
    public async Task InitializeDoctors()
    {
        var doctorsData = await LoadData<Doctor>("doctors");
        if (doctorsData != null)
        {
            foreach (var data in doctorsData)
            {
                var speciality = Specialities.Find(s => s.Id == data.Id);
                Doctor s = new Doctor(data.Id, data.Name, data.LicenseNumber, data.IsActive,data.Speciality);
                Doctors.Add(s);
            }
        }

    }
    public async Task InitializeSpecialities()
    {
        var specialitiesData = await LoadData<Speciality>("specialities");
        if(specialitiesData != null)
        {
            foreach (var data in specialitiesData)
            {
                Speciality s = new Speciality(data.Id,data.Name, data.Description);
                Specialities.Add(s);
            }
        }
    }
    private async Task<List<T>?> LoadData<T>(string file)
    {
        string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", $"{file}.json");
        var jsonContent = await File.ReadAllTextAsync(jsonPath);
        return JsonSerializer.Deserialize<List<T>>(jsonContent);
    }
}
