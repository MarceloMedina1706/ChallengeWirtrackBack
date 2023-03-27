namespace Viajes.DTOs
{
    public class ViajeDTO
    {

        public int IdViaje { get; set; }

        // DATOS PARA AGREGAR
        public int IdVehiculo { get; set; }
        public int IdCiudadDesde { get; set; }
        public int IdCiudadHasta { get; set; }
        public DateTime FechaSalida { get; set; }


        //DATOS PARA LA MUESTRA
        public VehiculoDTO? Vehiculo { get; set; }
        public string? CiudadHasta { get; set; }
        public string? CiudadDesde { get; set; }


        //PRONOSTICO
        public PronosticoDTO? Pronostico { get; set; }

    }
}
