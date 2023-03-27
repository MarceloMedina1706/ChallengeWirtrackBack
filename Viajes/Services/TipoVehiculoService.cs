using Viajes.DTOs;
using Viajes.Entities;
using Viajes.Exceptions;

namespace Viajes.Services
{
    public class TipoVehiculoService : ITipoVehiculoService
    {
        private readonly ViajesContext _context;

        public TipoVehiculoService(ViajesContext context)
        {
            _context = context;
        }

        public List<TipoVehiculoDTO> ObtenerTiposVehiculo()
        {
            var tipos = _context.TipoVehiculos.Select(s =>
            new TipoVehiculoDTO
            {
                IdTipoVehiculo = s.IdTipoVehiculo,
                Descripcion = s.Descripcion
            }).ToList();

            return tipos;
        }
        public TipoVehiculoDTO AgregarTipoVehiculo(TipoVehiculoDTO tipoVehiculo)
        {
            var tipo = new TipoVehiculo
            {
                Descripcion = tipoVehiculo.Descripcion
            };

            _context.Add(tipo);
            _context.SaveChanges();

            return new TipoVehiculoDTO
            {
                IdTipoVehiculo = tipo.IdTipoVehiculo,
                Descripcion = tipo.Descripcion
            };
        }

        public void EditarTipoVehiculo(TipoVehiculoDTO tipoVehiculo)
        {
            var tipo = _context.TipoVehiculos.Where(t => t.IdTipoVehiculo == tipoVehiculo.IdTipoVehiculo).FirstOrDefault();

            if (tipo == null) throw new EntityNotFoundException("No se encontró ningún tipo de vehículo en la BBDD");

            tipo.Descripcion = tipoVehiculo.Descripcion;
            _context.SaveChanges();
        }

        public void EliminarTipoVehiculo(int idTipoVehiculo)
        {
            var tipo = _context.TipoVehiculos.Where(t => t.IdTipoVehiculo == idTipoVehiculo).FirstOrDefault();

            if (tipo == null) throw new EntityNotFoundException("No se encontró ningún tipo de vehículo en la BBDD");
            
            _context.Remove(tipo);
            _context.SaveChanges();
        }

        public bool VerificarTipoVehiculo(int idTipoVehiculo)
        {
            var any = _context.Vehiculos.Any(v => v.IdTipoVehiculo == idTipoVehiculo);
            return any;
        }
    }
}
