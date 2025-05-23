using ApiTestIIS.Contexts;
using ApiTestIIS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public VehiculoController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        [HttpGet("search/{sVehiculo}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Vehiculo>>> Get(string sVehiculo)
        {
            try
            {
                var obj = await _context.Set<Vehiculo>()
                    .Where(c => c.Cia == "06" && c.Id_estado == "01" && (c.Placa_cisterna.Contains(sVehiculo)))
                    .Take(10)
                    .ToListAsync();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Un error ocurrió mientras se procesaba la solicitud de los vehiculos");
            }
        }

        [HttpGet("compartimento/{sCia}/{sPlacaCisterna}/{sPlacaTracto}")]
       // [Authorize]
        public async Task<ActionResult<IEnumerable<Vehiculo_Compartimiento>>> GetCompartimento(string sCia, string sPlacaCisterna, string sPlacaTracto)
        {
            try
            {
                var obj = await _context.Set<Vehiculo_Compartimiento>()
                    .Where(c => c.Cia == sCia && c.Placa_tractor == sPlacaTracto && c.Placa_cisterna == sPlacaCisterna && c.Id_estado == "01")
                    .ToListAsync();
                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Un error ocurrió al obtener los compartimientos de los vehiculos {ex}");
            }
        }
    }   
}
