using System;
using System.Collections.Generic;

namespace Viajes.Entities;

public partial class Viaje
{
    public int IdViaje { get; set; }

    public int IdVehiculo { get; set; }

    public int IdCiudadDesde { get; set; }

    public int IdCiudadHasta { get; set; }

    public DateTime Fecha { get; set; }

    public virtual Ciudad IdCiudadDesdeNavigation { get; set; } = null!;

    public virtual Ciudad IdCiudadHastaNavigation { get; set; } = null!;

    public virtual Vehiculo IdVehiculoNavigation { get; set; } = null!;
}
