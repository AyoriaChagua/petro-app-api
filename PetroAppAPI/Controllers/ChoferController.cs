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
    public class ChoferController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public ChoferController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        [HttpGet("search/{sText}")]
        public async Task<ActionResult<IEnumerable<Chofer>>> GetSearch(string sText)
        {
            var obj = await _context.Set<Chofer>()
                .Where(c => c.Cia == "06" && c.Id_estado == "01" && (c.Id_Chofer.Contains(sText) || c.Descripcion.Contains(sText)))
                .Take(10)
                .ToListAsync();
            return Ok(obj);
        }
    }
}
