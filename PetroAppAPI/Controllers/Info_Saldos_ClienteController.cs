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
    public class Info_Saldos_ClienteController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public Info_Saldos_ClienteController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // TRAE los Docs con Saldo del Cliente (para grafico)
        [HttpGet("{Cliente}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Info_Saldos_Cliente>>> GetInfo(string Cliente)
        {
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@cliente",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = Cliente
                }
            };
            string StoredProc = "exec sp_m_Info_Saldos_Cliente @cliente";
            return await _context.info_Saldos_Clientes.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
