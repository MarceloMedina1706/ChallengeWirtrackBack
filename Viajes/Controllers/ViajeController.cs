using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Viajes.DTOs;
using Viajes.Entities;
using Viajes.Services;

namespace Viajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViajeController : ControllerBase
    {

        private readonly IViajeService _viajeService;
        private readonly IConfiguration _configuration;
        public ViajeController(IViajeService viajeService, IConfiguration configuration)
        {
            _viajeService = viajeService;
            _configuration = configuration;
        }

        [HttpGet("ListaViajes")]
        public async Task<ActionResult> ListaViajes()
        {
            try
            {
                var viajes = _viajeService.obtenerViajes();

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Data = viajes
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje =  _configuration.GetValue<string>("ServerErrorMsg")
                });

            }
        }

        [HttpGet("ObtenerViaje/{IdViaje}")]
        public async Task<ActionResult> ListaViajes(int IdViaje)
        {
            try
            {
                var viaje = _viajeService.obtenerViaje(IdViaje);

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Data = viaje
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje =  _configuration.GetValue<string>("ServerErrorMsg")
                });

            }
        }

        [HttpGet("VerificarPronostico/{ciudad}")]
        public async Task<ActionResult<ResponseDTO>> VerificarPronostico(int ciudad)
        {
            
            try
            {
                var pronostico = await ViajeService.VerificarPronostico(ciudad, new DateTime());
                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Data = pronostico
                });
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje =  _configuration.GetValue<string>("ServerErrorMsg"),
                });
            }
        }

        [HttpPost("AsignarViaje")]
        public async Task<ActionResult> AsignarViaje(ViajeDTO viaje)
        {
            try
            {
                var viajeAgregar = _viajeService.AsignarViaje(viaje);
                var pronostico = await ViajeService.VerificarPronostico(viaje.IdCiudadDesde, viaje.FechaSalida);
                viajeAgregar.Pronostico = pronostico;

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "El viaje se asignó correctamente.",
                    Data = viajeAgregar
                });

            }catch(Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje =  _configuration.GetValue<string>("ServerErrorMsg"),
                });
            }
        }

        [HttpPut("ReprogramarViaje")]
        public async Task<ActionResult> ReprogramarViaje(ReprogramarDTO rep)
        {
            try
            {

                _viajeService.ReprogramarViaje(rep);
                var viaje = _viajeService.obtenerViaje(rep.IdViaje);
                var pronostico = await ViajeService.VerificarPronostico(viaje.IdCiudadDesde, rep.Fecha);
                viaje.Pronostico = pronostico;

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "El viaje se reprogramó correctamente.",
                    Data = viaje
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje =  _configuration.GetValue<string>("ServerErrorMsg"),
                });
            }
        }

        [HttpDelete("EliminarViaje/{idViaje}")]
        public async Task<ActionResult> EliminarViaje(int idViaje)
        {
            try
            {

                _viajeService.EliminarViaje(idViaje);

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "El viaje se eliminó correctamente."
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje =  _configuration.GetValue<string>("ServerErrorMsg"),
                });
            }
        }


    }
}
