using System;
using System.Collections.Generic;

namespace Viajes.Entities;

public partial class Ciudad
{
    public int IdCiudad { get; set; }

    public string NombreCiudad { get; set; } = null!;

    public string Pais { get; set; } = null!;

    public virtual ICollection<Viaje> ViajeIdCiudadDesdeNavigations { get; } = new List<Viaje>();

    public virtual ICollection<Viaje> ViajeIdCiudadHastaNavigations { get; } = new List<Viaje>();
}
