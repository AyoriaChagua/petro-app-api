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
    public class EstadoVentasClienteController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public EstadoVentasClienteController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // TRAE el Estado de Ventas del Cliente
        [HttpGet("{Cliente}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EstadoVentasCliente>>> GetPuntoVenta(string Cliente)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@cliente",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 20,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Cliente
                        }};
            string StoredProc = "exec sp_m_Estado_cliente @cliente";
            return await _context.estadoVentasClientes.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
