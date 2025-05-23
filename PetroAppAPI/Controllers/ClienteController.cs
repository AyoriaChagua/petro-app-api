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
    public class ClienteController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public ClienteController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        [HttpGet("search/{sCliente}")]
        [Authorize] 
        public async Task<ActionResult<IEnumerable<G_Client_for_Debt>>> Get_Search(string sCliente)
        {
            try
            {
                var sql = @"
                        SELECT TOP 10  id_cliente, descripcion, Id_estado, Nro_di, Cia
                        FROM Cliente WITH (NOLOCK)
                        WHERE Cia = '06' 
                          AND Id_estado = '01'
                          AND (id_cliente LIKE @buscar OR descripcion LIKE @buscar OR Nro_di LIKE @buscar)
                    ";
                var parametro = new SqlParameter("@buscar", $"%{sCliente}%");
                var obj = await _context.Cliente
                    .FromSqlRaw(sql, parametro)
                    .ToListAsync();

                /*var Obj = await _context.Set<Cliente>()
                    .Where(c => c.Cia == "06" && c.Id_estado == "01" && (c.id_cliente.Contains(sCliente) || c.descripcion.Contains(sCliente) || c.Nro_di.Contains(sCliente)))
                    .Take(10)
                    .ToListAsync();*/

                return Ok(obj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Un error occurrio mientras se procesaba tu request: {ex.Message}");
            }
        }
    }   
}
