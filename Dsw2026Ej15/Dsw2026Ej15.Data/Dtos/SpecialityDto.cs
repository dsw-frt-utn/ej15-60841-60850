using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Data.Dta;

public class SpecialityDto
{
    public Guid id { get; set; }
    public string name { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
}
