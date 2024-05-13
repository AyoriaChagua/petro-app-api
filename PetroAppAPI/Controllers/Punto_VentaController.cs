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
    public class Punto_VentaController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public Punto_VentaController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // RECUPERA LOS PUNTOS DE VENTA
        [HttpGet("{sCliente}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Punto_Venta>>> Get(string sCliente)
        {
            var obj = await (from c in _context.Punto_Venta
                            where c.Cia == "06" && c.Id_estado == "01" && c.Id_cliente == sCliente
                            select new Punto_Venta { Id_punto_venta=c.Id_punto_venta, 
                                Descripcion= c.Descripcion.Substring( c.Descripcion.IndexOf("S.A.C") == -1 ? c.Descripcion.Length : c.Descripcion.IndexOf("S.A.C")+5 ).Trim() +" - "+c.Descripcion, 
                                Cia=c.Cia, Id_cliente=c.Id_cliente, Id_estado=c.Id_cliente })
                        .ToListAsync();
            //var obj = await _context.Punto_Venta.Where(c => c.Cia == "06" && c.Id_estado == "01" && c.Id_cliente == sCliente).ToListAsync();

            return Ok(obj);
        }
    }
}
