using Viajes.DTOs;

namespace Viajes.Services
{
    public interface IViajeService
    {
        public ViajeDTO AsignarViaje(ViajeDTO viaje);
        public List<ViajeDTO> obtenerViajes();
        public ViajeDTO obtenerViaje(int IdViaje);
        public void ReprogramarViaje(ReprogramarDTO rep);
        public void EliminarViaje(int idViaje);
        
    }
}
