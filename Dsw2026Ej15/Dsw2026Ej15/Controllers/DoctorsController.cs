using Dsw2026Ej15.Data.Dtos;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api/doctors")]
public class DoctorsController : Controller
{
    private readonly IPersistencia _persistencia;

    public DoctorsController(IPersistencia persistencia)
    {
        _persistencia = persistencia;
    }

    // POST: insertar un nuevo médico
    [HttpPost]
    public async Task<IActionResult> InsertNewDoctor([FromBody] DoctorDto dto)
    { 
        var sp = await _persistencia.GetSpecialityAsync(dto.SpecialityId);
        if(sp is null)
        {
            return BadRequest("Error: No existe la especialidad");
        }
        var d = new Doctor(Guid.NewGuid(), dto.Name, dto.LicenseNumber, sp);
        return await _persistencia.AgregarMedicoAsync(d) ? Created() : BadRequest("Error: No se pudo agregar el Medico");
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
        return medico is null ? NotFound($"No se encontro o no esta activo, el medico de id: {id}") : Ok(medico);
    }
    // DELETE: baja logica del medico
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctorFromId(Guid id)
    {
        return await _persistencia.DeleteDoctorAsync(id) ? NoContent() : NotFound("No esta activo o no se encontro el Medico");
    }

}
