using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Viajes.DTOs;
using Viajes.Exceptions;
using Viajes.Models;
using Viajes.Services;

namespace Viajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IVehiculoService _vehiculoService;
        private readonly IConfiguration _configuration;
        public VehiculoController(IVehiculoService vehiculoService, IConfiguration configuration)
        {
            _vehiculoService = vehiculoService;
            _configuration = configuration;
        }

        [HttpGet("ListaVehiculos")]
        public async Task<ActionResult> ListaVehiculos()
        {
            try
            {
                var vehiculos = _vehiculoService.ObtenerVehiculos();

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Data = vehiculos
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

        [HttpGet("ListaVehiculosNoAsignado")]
        public async Task<ActionResult> ListaVehiculosNoAsignado()
        {
            try
            {
                var vehiculos = _vehiculoService.ObtenerVehiculosNoAsginados();

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Data = vehiculos
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

        [HttpGet("ObtenerVehiculo/{IdVehiculo}")]
        public async Task<ActionResult> ObtenerVehiculo(int IdVehiculo)
        {
            try
            {
                var vehiculo = _vehiculoService.ObtenerVehiculo(IdVehiculo);

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Data = vehiculo
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
                    Mensaje =  _configuration.GetValue<string>("ServerErrorMsg")
                });

            }
        }


        [HttpPost("AgregarVehiculo")]
        public async Task<ActionResult> AgregarVehiculo(VehiculoDTO vehiculo)
        {
            try
            {

                var anyPatente = _vehiculoService.VerificarPatente(vehiculo.Patente);

                if (anyPatente)
                {
                    return Ok(new ResponseDTO
                    {
                        Codigo = 2,
                        Mensaje = $"Ya existe un vehiculo con la patente <b>{vehiculo.Patente}</b>.",
                    });
                }

                var obj = _vehiculoService.AgregarVehiculo(vehiculo);
                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "El vehiculo se cargó satisfactoriamente.",
                    Data = obj
                });
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje =  _configuration.GetValue<string>("ServerErrorMsg")
                });
            }
        }

        [HttpPut("EditarVehiculo")]
        public async Task<ActionResult> EditarVehiculo(VehiculoDTO vehiculo)
        {
            try
            {
                _vehiculoService.EditarVehiculo(vehiculo);
                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "El vehiculo se modificó satisfactoriamente."
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
                    Mensaje =  _configuration.GetValue<string>("ServerErrorMsg")
                });
            }
        }

        [HttpDelete("EliminarVehiculo/{IdVehiculo}")]
        public async Task<ActionResult> EliminarVehiculo(int IdVehiculo)
        {
            try
            {
                _vehiculoService.EliminarVehiculo(IdVehiculo);
                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "El vehiculo se eliminó satisfactoriamente."
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
                    Mensaje =  _configuration.GetValue<string>("ServerErrorMsg")
                });
            }
        }

        [HttpDelete("EliminarVehiculoViaje/{IdVehiculo}")]
        public async Task<ActionResult> EliminarVehiculoViaje(int IdVehiculo)
        {
            try
            {
                _vehiculoService.EliminarVehiculoViaje(IdVehiculo);
                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "El vehiculo se eliminó satisfactoriamente."
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
                    Mensaje =  _configuration.GetValue<string>("ServerErrorMsg")
                });
            }
        }



        [HttpGet("VerificarVehiculoViaje/{IdVehiculo}")]
        public async Task<ActionResult> VerificarVehiculoViaje(int IdVehiculo)
        {
            try
            {
                var result = _vehiculoService.VerificarVehiculoViaje(IdVehiculo);


                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "",
                    Data = result
                });
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje =  _configuration.GetValue<string>("ServerErrorMsg")
                });
            }
        }


    }
}
