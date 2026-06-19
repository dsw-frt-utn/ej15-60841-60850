using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dsw2026Ej15.Data.Dtos;

public record DoctorDto
{
    public record Request(string Name, string LicenseNumber, Guid SpecialityId);
}
