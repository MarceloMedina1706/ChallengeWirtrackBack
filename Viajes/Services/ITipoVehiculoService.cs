using Viajes.DTOs;

namespace Viajes.Services
{
    public interface ITipoVehiculoService
    {
        public List<TipoVehiculoDTO> ObtenerTiposVehiculo();
        public TipoVehiculoDTO AgregarTipoVehiculo(TipoVehiculoDTO tipoVehiculo);
        public void EditarTipoVehiculo(TipoVehiculoDTO tipoVehiculo);
        public void EliminarTipoVehiculo(int idTipoVehiculo);
        public bool VerificarTipoVehiculo(int idTipoVehiculo);
    }
}
