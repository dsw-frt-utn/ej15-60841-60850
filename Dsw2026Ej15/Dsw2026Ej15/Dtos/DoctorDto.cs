using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dsw2026Ej15.Data.Dtos;

public record DoctorDto
{
    public record Request([Required] string Name, [Required] string LicenseNumber, [Required] Guid SpecialityId);
    public record Response([Required] Guid Id, [Required] string Name, [Required] string LicenseNumber);
}
