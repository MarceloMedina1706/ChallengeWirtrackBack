using System;
using System.Collections.Generic;

namespace Viajes.Entities;

public partial class Vehiculo
{
    public int IdVehiculo { get; set; }

    public short IdTipoVehiculo { get; set; }

    public string Modelo { get; set; } = null!;

    public string Marca { get; set; } = null!;

    public string Patente { get; set; } = null!;

    public virtual TipoVehiculo IdTipoVehiculoNavigation { get; set; } = null!;

    public virtual ICollection<Viaje> Viajes { get; } = new List<Viaje>();
}
