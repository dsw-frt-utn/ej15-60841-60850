using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Data.Dta;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.CompilerServices;


namespace Dsw2026Ej15.Data;

public class PersistenciaInMemory : IPersistencia
{
    private readonly List<Doctor> Doctors = [];
    private readonly List<Speciality> Specialities = [];

    public PersistenciaInMemory()
    {
        Initialize();
    }

    //public static async Task<PersistenciaInMemory> InitializeDataAsync()
    //{
    //    var persistencia = new PersistenciaInMemory();
    //    await persistencia.InitializeDoctors();
    //    return persistencia;
    //}
    private void Initialize()
    {
        InitializeSpecialities();
        InitializeDoctors();
    }
    private void InitializeDoctors()
    {
        var s1 = Specialities.Find(s => s.Name == "Neurología");
        var s2 = Specialities.Find(s => s.Name == "Neurología");
        var s3 = Specialities.Find(s => s.Name == "Cardiología");

        if(s1 is null || s2 is null || s3 is null)
        {
           throw new Exception();
        }

        var d1 = new Doctor(Guid.NewGuid(), "Joaquin Valdecantos", "MN-45213", s1, true);
        var d2 = new Doctor(Guid.NewGuid(), "Juan Tenreyro", "MM-45213", s2, false);
        var d3 = new Doctor(Guid.NewGuid(), "Tadeo Torres", "MR-45213", s3, true);
        
        Doctors.Add(d1);
        Doctors.Add(d2);
        Doctors.Add(d3);
    }
    private void InitializeSpecialities()
    {
        var specialitiesData = LoadSpecialities<SpecialityDto>("specialities");
        if(specialitiesData != null)
        {
            foreach (var data in specialitiesData)
            {
                Speciality s = new Speciality(data.id,data.name, data.description);
                Specialities.Add(s);
            }
        }
    }
    private List<T>? LoadSpecialities<T>(string file)
    {
        string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", $"{file}.json");
        string jsonContent = File.ReadAllText(jsonPath);
        return JsonSerializer.Deserialize<List<T>>(jsonContent);
    }

    public async Task<bool> AgregarMedicoAsync(Doctor doctor)
    {
        try
        {
            Doctors.Add(doctor);
            return true;
        }
        catch
        {
            return false;
        }
    }
    public async Task<Doctor?> GetDoctorAsync(Guid id)
    {
        return Doctors.Find(d => d.Id == id);
    }

    public async Task<List<Doctor>> GetDoctorsListAsync()
    {
        return Doctors;
    }
    public async Task<List<Speciality>> GetSpecialitiesListAsync()
    {
        return Specialities;
    }
    public async Task<Speciality?> GetSpecialityAsync(Guid id)
    {
        return Specialities.Find(s => s.Id == id);
    }
    public async Task<bool> DeleteDoctorAsync(Guid id)
    { 
        var medico = Doctors.Find(d => d.Id == id);
        if(medico is null)
        {
            return false;
        }
        if(medico.IsActive == false)
        {
            return false;
        }
        else
        {
            medico.IsActive = false;
            return true;
        }
    }
}
