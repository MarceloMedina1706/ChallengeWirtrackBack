namespace Viajes.DTOs
{
    public class VehiculoDTO
    {
        public int IdVehiculo { get; set; }

        public short IdTipoVehiculo { get; set; }

        public string TipoVehiculo { get; set; } = null!;

        public string Modelo { get; set; } = null!;

        public string Marca { get; set; } = null!;

        public string Patente { get; set; } = null!;
    }
}
