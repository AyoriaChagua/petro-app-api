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

        [HttpGet("{sCliente}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Punto_Venta>>> GetByClient(string sCliente)
        {
            var obj = await (from c in _context.Punto_Venta
                            where c.Cia == "06" && c.Id_estado == "01" && c.Id_cliente == sCliente
                            select new Punto_Venta { 
                                Id_punto_venta=c.Id_punto_venta, 
                                Descripcion= c.Descripcion.Substring( c.Descripcion.IndexOf("S.A.C") == -1 ? c.Descripcion.Length : c.Descripcion.IndexOf("S.A.C")+5 ).Trim() +" - "+c.Descripcion, 
                                Cia=c.Cia, 
                                Id_cliente=c.Id_cliente, 
                                Id_estado=c.Id_cliente
                            })
                        .ToListAsync();
            return Ok(obj);
        }

        [HttpGet("search/{searchString}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Punto_Venta>>> GetSearch(string searchString)
        {
            var obj = await (from c in _context.Punto_Venta
                             where c.Cia == "06" && c.Id_estado == "01"
                             select new
                             {
                                 PuntoVenta = c,
                                 DescripcionModificada = c.Descripcion.Substring(c.Descripcion.IndexOf("S.A.C") == -1 ? c.Descripcion.Length : c.Descripcion.IndexOf("S.A.C") + 5).Trim() + " - " + c.Descripcion
                             })
                             .ToListAsync();

            var result = obj
                .Where(c => c.PuntoVenta.Id_cliente == searchString || c.PuntoVenta.Direccion.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(c => new Punto_Venta
                {
                    Id_punto_venta = c.PuntoVenta.Id_punto_venta,
                    Descripcion = c.DescripcionModificada,
                    Cia = c.PuntoVenta.Cia,
                    Id_cliente = c.PuntoVenta.Id_cliente,
                    Id_estado = c.PuntoVenta.Id_cliente,
                    Direccion = c.PuntoVenta.Direccion
                })
                .Take(15)
                .ToList();

            return Ok(result);
        }


    }
}
