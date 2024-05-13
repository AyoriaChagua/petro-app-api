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
    public class ArticuloSubclaseController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public ArticuloSubclaseController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        [HttpGet]
        [Authorize] 
        public async Task<ActionResult<IEnumerable<ArticuloSubclase>>> Get()
        {
            string Query = "select ID_ARTICULO_SUBCLASE from ARTICULO_SUBCLASE where CIA='06' and ID_ESTADO='01' and ID_ARTICULO_CLASE='com'";
            return await _context.ArticuloSubclase.FromSqlRaw(Query).ToListAsync();
        }
    }
}
