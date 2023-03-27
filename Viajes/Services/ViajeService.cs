using Newtonsoft.Json;
using Viajes.DTOs;
using Viajes.Entities;
using Viajes.Exceptions;
using Viajes.Models;

namespace Viajes.Services
{
    public class ViajeService : IViajeService
    {
        private readonly ViajesContext _context;
        private static IConfiguration? _configuration;
        public ViajeService(ViajesContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public ViajeDTO AsignarViaje(ViajeDTO viaje)
        {
            var newViaje = new Viaje
            {
                IdVehiculo = viaje.IdVehiculo,
                IdCiudadDesde = viaje.IdCiudadDesde,
                IdCiudadHasta = viaje.IdCiudadHasta,
                Fecha = viaje.FechaSalida
            };

            _context.Add(newViaje);
            _context.SaveChanges();


            var viajeAdded = obtenerViaje(newViaje.IdViaje);
            return viajeAdded;
        }

        public List<ViajeDTO> obtenerViajes()
        {
            var viajes = (from via in _context.Viajes
                          join vehi in _context.Vehiculos
                          on via.IdVehiculo equals vehi.IdVehiculo
                          join tVehi in _context.TipoVehiculos
                          on vehi.IdTipoVehiculo equals tVehi.IdTipoVehiculo
                          join ciudad1 in _context.Ciudads
                          on via.IdCiudadDesde equals ciudad1.IdCiudad
                          join ciudad2 in _context.Ciudads
                          on via.IdCiudadHasta equals ciudad2.IdCiudad
                          select new ViajeDTO
                          {
                              IdViaje = via.IdViaje,
                              IdVehiculo = via.IdVehiculo,
                              IdCiudadDesde = via.IdCiudadDesde,
                              IdCiudadHasta = via.IdCiudadHasta,
                              FechaSalida = via.Fecha,
                              CiudadDesde = ciudad1.NombreCiudad,
                              CiudadHasta = ciudad2.NombreCiudad,
                              Vehiculo = new VehiculoDTO
                              {
                                  IdTipoVehiculo = tVehi.IdTipoVehiculo,
                                  TipoVehiculo = tVehi.Descripcion,
                                  Marca = vehi.Marca,
                                  Modelo = vehi.Modelo,
                                  Patente = vehi.Patente
                              }

                          }).ToList();

            return viajes;

        }

        public ViajeDTO obtenerViaje(int IdViaje)
        {
            var viaje = (from via in _context.Viajes
                          join vehi in _context.Vehiculos
                          on via.IdVehiculo equals vehi.IdVehiculo
                          join tVehi in _context.TipoVehiculos
                          on vehi.IdTipoVehiculo equals tVehi.IdTipoVehiculo
                          join ciudad1 in _context.Ciudads
                          on via.IdCiudadDesde equals ciudad1.IdCiudad
                          join ciudad2 in _context.Ciudads
                          on via.IdCiudadHasta equals ciudad2.IdCiudad
                          where via.IdViaje == IdViaje
                          select new ViajeDTO
                          {
                              IdViaje = via.IdViaje,
                              IdVehiculo = via.IdVehiculo,
                              IdCiudadDesde = via.IdCiudadDesde,
                              IdCiudadHasta = via.IdCiudadHasta,
                              FechaSalida = via.Fecha,
                              CiudadDesde = ciudad1.NombreCiudad,
                              CiudadHasta = ciudad2.NombreCiudad,
                              Vehiculo = new VehiculoDTO
                              {
                                  IdTipoVehiculo = tVehi.IdTipoVehiculo,
                                  TipoVehiculo = tVehi.Descripcion,
                                  Marca = vehi.Marca,
                                  Modelo = vehi.Modelo,
                                  Patente = vehi.Patente
                              }
                              
                          }).FirstOrDefault();

            if (viaje == null) throw new EntityNotFoundException("No se encontró ningún viaje en la BBDD");

            return viaje;
        }

        public void ReprogramarViaje(ReprogramarDTO rep)
        {
            var viaje = _context.Viajes.Where(v => v.IdViaje == rep.IdViaje).FirstOrDefault();

            if (viaje == null) throw new EntityNotFoundException("No se encontró ningún viaje en la BBDD");

            viaje.Fecha = rep.Fecha;
            _context.SaveChanges();
        }

        public void EliminarViaje(int idViaje)
        {
            var viaje = _context.Viajes.Where(v => v.IdViaje == idViaje).FirstOrDefault();

            if (viaje == null) throw new EntityNotFoundException("No se encontró ningún viaje en la BBDD");

            _context.Remove(viaje);
            _context.SaveChanges();
        }

        public static async Task<PronosticoDTO> VerificarPronostico(int ciudad, DateTime fecha)
        {
            try
            {
                using (var http = new HttpClient())
                {
                    var urlWeather = _configuration.GetValue<string>("UrlWeather").Replace("idCiudad", $"{ciudad}");
                    var urlWeatherIcon = _configuration.GetValue<string>("UrlWeatherIcon");
                    //var url = $"https://api.openweathermap.org/data/2.5/forecast?id={ciudad}&lang=es&appid=acfd67d0ed552aa84ea3a2c8f791cb5f";


                    var response = await http.GetStringAsync(urlWeather);

                    var result = JsonConvert.DeserializeObject<ResponseWeather>(response);


                    var pronostico = new PronosticoDTO();
                    result?.List.ForEach(data =>
                    {
                        data.Weather.ForEach(w =>
                        {
                            var date1 = data.dt_txt.ToString().Split(" ")[0];
                            var date2 = fecha.ToString().Split(" ")[0];
                            Console.WriteLine($"DATE --> {date1}");
                            if(date1 == date2)
                            {
                                if (w.Id >= 500 && w.Id <= 600)
                                {
                                    pronostico.Id = w.Id;
                                    pronostico.Description = w.Description;
                                    pronostico.Icon = urlWeatherIcon.Replace("idIcon", $"{w.Icon}");
                                }

                            }
                        });

                    });

                    return pronostico;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new Exception("Ha ocurrido un error al consultar el pronostico.");
            }
        }
    }
}
