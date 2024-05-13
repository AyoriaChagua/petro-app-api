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
    public class VentasAlClienteController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public VentasAlClienteController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // TRAE las Ventas al Cliente (para grafico)
        [HttpGet("{Cliente}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<VentasAlCliente>>> GetVentasAlCliente(string Cliente)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@cliente",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 20,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = Cliente
                        }};
            string StoredProc = "exec sp_m_Ventas_al_Cliente_U_M @cliente";
            return await _context.ventasAlClientes.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
