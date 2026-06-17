using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistencia
{
    Task<bool> AgregarMedicoAsync(Doctor doctor);
    Task<Doctor?> GetDoctorAsync(Guid id);
    Task<List<Doctor>> GetDoctorsListAsync();
    Task<List<Speciality>> GetSpecialitiesListAsync();
    Task<Speciality?> GetSpecialityAsync(Guid id);
    Task<bool> DeleteDoctorAsync(Guid id);
}
