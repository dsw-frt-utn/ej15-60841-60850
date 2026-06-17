using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Dsw2026Ej15.Data.Dtos;

public class DoctorDto
{
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string LicenseNumber { get; set; } = string.Empty;
    [Required]
    public Guid SpecialityId { get; set; }

}
