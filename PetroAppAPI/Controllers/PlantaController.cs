using ApiTestIIS.Contexts;
using ApiTestIIS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantaController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public PlantaController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // RECUPERA LAS PLANTAS
        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<Planta>>> GetPlanta()
        {
            var obj = await _context.Planta.Where(c => c.Cia == "06" && c.Id_estado == "01").ToListAsync();

            return Ok(obj);
        }

        /*
        [HttpGet]
        [Authorize] // SOLO USUARIOS AUTENTICADOS
        public async Task<ActionResult<IEnumerable<Planta>>> Get()
        {
            string Query = "select ID_PLANTA, DESCRIPCION from PLANTA where CIA='06' and ID_ESTADO='01'";
            return await _context.Planta.FromSqlRaw(Query).ToListAsync();
        }
        */
    }
}
