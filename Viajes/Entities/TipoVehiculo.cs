using System;
using System.Collections.Generic;

namespace Viajes.Entities;

public partial class TipoVehiculo
{
    public short IdTipoVehiculo { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Vehiculo> Vehiculos { get; } = new List<Vehiculo>();
}
