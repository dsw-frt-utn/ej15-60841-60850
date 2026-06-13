using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Dsw2026Ej15.Domain.Interfaces;

public interface IPersistence
{
    Task InitializeDoctors();
    Task InitializeSpecialities();
}
