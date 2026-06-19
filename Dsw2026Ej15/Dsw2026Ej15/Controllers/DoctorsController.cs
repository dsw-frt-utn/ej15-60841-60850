using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : ControllerBase
{
    private readonly IPersistencia _persistencia;

    public DoctorsController(IPersistencia persistencia)
    {
        _persistencia = persistencia;
    }

    // POST: insertar un nuevo médico
    [HttpPost]
    public async Task<IActionResult> InsertNewDoctor([FromBody] DoctorDto.Request dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.LicenseNumber))
        {
            throw new ValidationException("Error: no existe la especialidad");
        }
        var sp = await _persistencia.GetSpecialityAsync(dto.SpecialityId);
        if (sp is null)
        {
            throw new ValidationException("Error: no existe la especialidad");
        }
        var d = new Doctor(Guid.NewGuid(), dto.Name, dto.LicenseNumber, sp);
        return await _persistencia.AgregarMedicoAsync(d) ? Created() : throw new ValidationException("Error: No se pudo agregar el Medico");
    }

    // GET: listar los doctores
    [HttpGet]
    public async Task<IActionResult> GetAllDoctors()
    {
        return Ok(await _persistencia.GetDoctorsListAsync());
    }

    // GET: obtener un doctor
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDoctorFromId(Guid id)
    {
        var medico = await _persistencia.GetDoctorAsync(id);
        return medico is null ? throw new ValidationException($"No se encontro o no esta activo, el Medico de id: {id}") : Ok(medico);
    }

    // DELETE: baja logica del medico
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorFromId(Guid id)
    {
        return await _persistencia.DeleteDoctorAsync(id) ? NoContent() : throw new ValidationException("No esta activo o no se encontro el Medico");
    }
}