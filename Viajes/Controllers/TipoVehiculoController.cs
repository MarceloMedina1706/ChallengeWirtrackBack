using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Viajes.DTOs;
using Viajes.Entities;
using Viajes.Services;

namespace Viajes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoVehiculoController : ControllerBase
    {
        private readonly ITipoVehiculoService _tipoVehiculoService;
        private readonly IConfiguration _configuration;
        public TipoVehiculoController(ITipoVehiculoService tipoVehiculoService, IConfiguration configuration)
        {
            _tipoVehiculoService = tipoVehiculoService;
            _configuration = configuration;
        }


        [HttpGet("ListaTiposVehiculo")]
        public async Task<ActionResult> ListaTiposVehiculo()
        {
            try
            {
                var tipos = _tipoVehiculoService.ObtenerTiposVehiculo();

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Data = tipos
                }) ;
            }
            catch (Exception e){
                Console.WriteLine(e);
                return Ok(new ResponseDTO
                {
                    Codigo = 0,
                    Mensaje = _configuration.GetValue<string>("ServerErrorMsg")
                });
            }
        }

        [HttpPost("AgregarTipoVehiculo")]
        public async Task<ActionResult> AgregarTipoVehiculo(TipoVehiculoDTO TipoVehiculo)
        {
            try
            {
                var added = _tipoVehiculoService.AgregarTipoVehiculo(TipoVehiculo);

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "El tipo de vehiculo se cargó satisfactoriamente.",
                    Data = added
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

        [HttpPut("EditarTipoVehiculo")]
        public async Task<ActionResult> EditarTipoVehiculo(TipoVehiculoDTO TipoVehiculo)
        {
            try
            {
                 _tipoVehiculoService.EditarTipoVehiculo(TipoVehiculo);

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "El tipo de vehiculo se editó satisfactoriamente.",
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

        [HttpDelete("EliminarTipoVehiculo/{idTipo}")]
        public async Task<ActionResult> EliminarTipoVehiculo(int idTipo)
        {
            try
            {
                var notAllow = _tipoVehiculoService.VerificarTipoVehiculo(idTipo);

                if (notAllow)
                {
                    return Ok(new ResponseDTO
                    {
                        Codigo = 2,
                        Mensaje = "El tipo de vehiculo que interta eliminar tiene vehiculos asignados.",
                    });
                }

                _tipoVehiculoService.EliminarTipoVehiculo(idTipo);

                return Ok(new ResponseDTO
                {
                    Codigo = 1,
                    Mensaje = "El tipo de vehiculo se eliminó satisfactoriamente.",
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
