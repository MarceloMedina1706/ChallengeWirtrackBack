using Viajes.DTOs;
using Viajes.Entities;
using Viajes.Exceptions;

namespace Viajes.Services
{
    public class VehiculoService : IVehiculoService
    {
        private readonly ViajesContext _context;
        public VehiculoService(ViajesContext context)
        {
            _context = context;
        }

        public List<VehiculoDTO> ObtenerVehiculos()
        {

            var vehiculos = (from v in _context.Vehiculos
                             join t in _context.TipoVehiculos
                             on v.IdTipoVehiculo equals t.IdTipoVehiculo
                             select new VehiculoDTO
                             {
                                 IdVehiculo = v.IdVehiculo,
                                 IdTipoVehiculo = v.IdTipoVehiculo,
                                 TipoVehiculo = t.Descripcion,
                                 Modelo = v.Modelo,
                                 Marca = v.Marca,
                                 Patente = v.Patente,
                             }).ToList();

            
            return vehiculos;
        }

        public List<VehiculoDTO> ObtenerVehiculosNoAsginados()
        {

            var vehiculos = (from v in _context.Vehiculos
                             join t in _context.TipoVehiculos
                             on v.IdTipoVehiculo equals t.IdTipoVehiculo
                             where !_context.Viajes.Any(viaje => viaje.IdVehiculo == v.IdVehiculo)
                             select new VehiculoDTO
                             {
                                 IdVehiculo = v.IdVehiculo,
                                 IdTipoVehiculo = v.IdTipoVehiculo,
                                 TipoVehiculo = t.Descripcion,
                                 Modelo = v.Modelo,
                                 Marca = v.Marca,
                                 Patente = v.Patente,
                             }).ToList();


            return vehiculos;
        }
        public VehiculoDTO ObtenerVehiculo(int IdVehiculo)
        {

            var vhcl = (from v in _context.Vehiculos
                             join t in _context.TipoVehiculos
                             on v.IdTipoVehiculo equals t.IdTipoVehiculo
                        where v.IdVehiculo == IdVehiculo
                             select new VehiculoDTO
                             {
                                 IdVehiculo = v.IdVehiculo,
                                 IdTipoVehiculo = v.IdTipoVehiculo,
                                 TipoVehiculo = t.Descripcion,
                                 Modelo = v.Modelo,
                                 Marca = v.Marca,
                                 Patente = v.Patente,
                             }).FirstOrDefault();

            if (vhcl == null) throw new EntityNotFoundException("No se encontró ningún vehículo en la BBDD.");

            return vhcl;
        }

        public VehiculoDTO AgregarVehiculo(VehiculoDTO vehiculo)
        {
            var vehiculoNuevo = new Vehiculo
            {
                IdTipoVehiculo = vehiculo.IdTipoVehiculo,
                Modelo = vehiculo.Modelo,
                Marca = vehiculo.Marca,
                Patente = vehiculo.Patente,
            };

            _context.Add(vehiculoNuevo);
            _context.SaveChanges();

            return ObtenerVehiculo(vehiculoNuevo.IdVehiculo);
        }

        public void EditarVehiculo(VehiculoDTO vehiculo)
        {
            var vhcl = _context.Vehiculos.Where(v => v.IdVehiculo == vehiculo.IdVehiculo).FirstOrDefault();

            if (vhcl == null) throw new EntityNotFoundException("No se encontró ningún vehículo en la BBDD.");

            vhcl.IdTipoVehiculo = vehiculo.IdTipoVehiculo;
            vhcl.Modelo = vehiculo.Modelo;
            vhcl.Marca = vehiculo.Marca;
            vhcl.Patente = vehiculo.Patente;

            _context.SaveChanges();
        }

        public void EliminarVehiculo(int IdVehiculo)
        {
            var vhcl = _context.Vehiculos.Where(v => v.IdVehiculo == IdVehiculo).FirstOrDefault();

            if (vhcl == null) throw new EntityNotFoundException("No se encontró ningún vehículo en la BBDD.");

            _context.Remove(vhcl);

            _context.SaveChanges();
        }

        public void EliminarVehiculoViaje(int IdVehiculo)
        {
            var vv = _context.Viajes.Where(x => x.IdVehiculo == IdVehiculo).ToList();

            vv.ForEach(x =>
            {
                _context.Remove(x);

            });

            _context.SaveChanges();

            EliminarVehiculo(IdVehiculo);
        }

        public bool VerificarPatente(string Patente)
        {
            var any = _context.Vehiculos.Any(v => v.Patente == Patente);
            return any;
        }
        public bool VerificarVehiculoViaje(int IdVehiculo)
        {
            var any = _context.Viajes.Any(v => v.IdVehiculo == IdVehiculo);
            return any;
        }

        
    }
}
