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
    public class AsesorController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public AsesorController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // RECUPERA LOS PUNTOS DE VENTA
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Asesor>>> Get()
        {
            var obj = await _context.Asesor.Where(c => c.Cia == "06" && c.Id_estado == "01" ).ToListAsync();

            return Ok(obj);
        }
    }
}
