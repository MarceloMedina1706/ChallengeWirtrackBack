using Viajes.DTOs;
using Viajes.Entities;
using Viajes.Exceptions;

namespace Viajes.Services
{
    public class CiudadService : ICiudadService
    {
        private readonly ViajesContext _context;
        public CiudadService(ViajesContext context)
        {
            _context = context;
        }
        public void AgregarCiudad(CiudadDTO ciudad)
        {
            var ciudadNueva = new Ciudad
            {
                IdCiudad = ciudad.IdCiudad,
                NombreCiudad = ciudad.NombreCiudad,
                Pais = ciudad.Pais
            };

            _context.Add(ciudadNueva);
            _context.SaveChanges();
        }

        public void EditarCiudad(CiudadDTO ciudad)
        {
            var c = _context.Ciudads.Where(cc => cc.IdCiudad == ciudad.IdCiudad).FirstOrDefault();

            if(c == null) throw new EntityNotFoundException("No se encontró ninguna ciudad en la BBDD.");

            c.NombreCiudad = ciudad.NombreCiudad;
            c.Pais = ciudad.Pais;
            _context.SaveChanges();
        }

        public void EliminarCiudad(int IdCiudad)
        {
            var c = _context.Ciudads.Where(cc => cc.IdCiudad == IdCiudad).FirstOrDefault();

            if (c == null) throw new EntityNotFoundException("No se encontró ninguna ciudad en la BBDD.");

            _context.Remove(c);
            _context.SaveChanges();
        }

        public void EliminarCiudadViaje(int IdCiudad)
        {
            var vv = _context.Viajes.Where(v => v.IdCiudadDesde == IdCiudad || v.IdCiudadHasta == IdCiudad).ToList();

            vv.ForEach(x =>
            {
                _context.Remove(x);

            });

            _context.SaveChanges();

            EliminarCiudad(IdCiudad);
        }

        public CiudadDTO ObtenerCiudad(int IdCiudad)
        {
            var c = _context.Ciudads.Where(cc => cc.IdCiudad == IdCiudad).Select(cc => new CiudadDTO
            {
                IdCiudad = cc.IdCiudad,
                NombreCiudad = cc.NombreCiudad,
                Pais = cc.Pais
            }).FirstOrDefault();

            if (c == null) throw new EntityNotFoundException("No se encontró ninguna ciudad en la BBDD.");

            return c;
        }

        public List<CiudadDTO> ObtenerCiudades()
        {
            var c = _context.Ciudads.Select(cc => new CiudadDTO
            {
                IdCiudad = cc.IdCiudad,
                NombreCiudad = cc.NombreCiudad,
                Pais = cc.Pais
            }).ToList();


            return c;
        }

        public bool VerificarCiudad(int IdCiudad)
        {
            var c = _context.Ciudads.Any(c => c.IdCiudad == IdCiudad);

            return c;
        }

        public bool VerificarCiudadViaje(int IdCiudad)
        {
            var c = _context.Viajes.Any(v => v.IdCiudadDesde == IdCiudad || v.IdCiudadHasta == IdCiudad );

            return c;
        }
    }
}
