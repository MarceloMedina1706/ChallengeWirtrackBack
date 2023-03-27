using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Viajes.DTOs;
using Viajes.Exceptions;
using Viajes.Services;

namespace Viajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadController : ControllerBase
    {
        private readonly ICiudadService _ciudadService;
        private readonly IConfiguration _configuration;
        public CiudadController(ICiudadService ciudadService, IConfiguration configuration)
        {
            _ciudadService = ciudadService;
            _configuration = configuration;
        }

        [HttpGet("ListaCiudades")]
        public async Task<ActionResult> ListaCiudades()
        {
            try
            {
                var ciudades = _ciudadService.ObtenerCiudades();

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Data = ciudades
                });

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje = _configuration.GetValue<string>("ServerErrorMsg")
                });

            }
        }

        [HttpGet("ObtenerCiudad/{IdCiudad}")]
        public async Task<ActionResult> ObtenerCiudad(int IdCiudad)
        {
            try
            {
                var ciudad = _ciudadService.ObtenerCiudad(IdCiudad);

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Data = ciudad
                });

            }
            catch (EntityNotFoundException e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje = e.Message
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje = _configuration.GetValue<string>("ServerErrorMsg")
                });

            }
        }

        [HttpPost("AgregarCiudad")]
        public async Task<ActionResult> AgregarCiudad(CiudadDTO ciudad)
        {
            try
            {

                var anyCiudad = _ciudadService.VerificarCiudad(ciudad.IdCiudad);

                if (anyCiudad)
                {
                    return Ok(new ResponseDTO
                    {
                        Codigo = 2,
                        Mensaje = $"Ya existe una ciudad con la Id <b>{ciudad.IdCiudad}</b>.",
                    });
                }


                _ciudadService.AgregarCiudad(ciudad);
                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "La ciudad se cargó satisfactoriamente."
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje = _configuration.GetValue<string>("ServerErrorMsg")
                });
            }
        }

        [HttpPut("EditarCiudad")]
        public async Task<ActionResult> EditarCiudad(CiudadDTO ciudad)
        {
            try
            {
                _ciudadService.EditarCiudad(ciudad);
                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "La ciudad se modificó satisfactoriamente."
                });
            }
            catch (EntityNotFoundException e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje = e.Message
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje = _configuration.GetValue<string>("ServerErrorMsg")
                });
            }
        }

        [HttpDelete("EliminarCiudad/{IdCiudad}")]
        public async Task<ActionResult> EliminarCiudad(int IdCiudad)
        {
            try
            {
                _ciudadService.EliminarCiudad(IdCiudad);
                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "La ciudad se eliminó satisfactoriamente."
                });
            }
            catch (EntityNotFoundException e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje = e.Message
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje = _configuration.GetValue<string>("ServerErrorMsg")
                });
            }
        }

        [HttpDelete("EliminarCiudadViaje/{IdCiudad}")]
        public async Task<ActionResult> EliminarCiudadViaje(int IdCiudad)
        {
            try
            {
                _ciudadService.EliminarCiudadViaje(IdCiudad);
                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "La ciudad se eliminó satisfactoriamente."
                });
            }
            catch (EntityNotFoundException e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje = e.Message
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje = _configuration.GetValue<string>("ServerErrorMsg")
                });
            }
        }



        [HttpGet("VerificarCiudadViaje/{IdCiudad}")]
        public async Task<ActionResult> VerificarVehiculoViaje(int IdCiudad)
        {
            try
            {
                var result = _ciudadService.VerificarCiudadViaje(IdCiudad);


                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "",
                    Data = result
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje = _configuration.GetValue<string>("ServerErrorMsg")
                });
            }
        }
    }
}
