using Viajes.DTOs;

namespace Viajes.Services
{
    public interface ICiudadService
    {
        public List<CiudadDTO> ObtenerCiudades();
        public CiudadDTO ObtenerCiudad(int IdCiudad);
        public void AgregarCiudad(CiudadDTO ciudad);
        public void EditarCiudad(CiudadDTO ciudad);
        public void EliminarCiudad(int IdCiudad);
        public void EliminarCiudadViaje(int IdCiudad);
        public bool VerificarCiudad(int IdCiudad);
        public bool VerificarCiudadViaje(int IdCiudad);
    }
}
