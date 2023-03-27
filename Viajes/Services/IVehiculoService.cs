using Viajes.DTOs;

namespace Viajes.Services
{
    public interface IVehiculoService
    {
        public List<VehiculoDTO> ObtenerVehiculos();
        public List<VehiculoDTO> ObtenerVehiculosNoAsginados();
        public VehiculoDTO ObtenerVehiculo(int IdVehiculo);
        public VehiculoDTO AgregarVehiculo(VehiculoDTO vehiculo);
        public void EditarVehiculo(VehiculoDTO vehiculo);
        public void EliminarVehiculo(int IdVehiculo);
        public void EliminarVehiculoViaje(int IdVehiculo);
        public bool VerificarPatente(string Patente);
        public bool VerificarVehiculoViaje(int IdVehiculo);
    }
}
