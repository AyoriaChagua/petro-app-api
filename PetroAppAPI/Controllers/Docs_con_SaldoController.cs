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
    public class Docs_con_SaldoController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public Docs_con_SaldoController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // TRAE los Docs con Saldo del Cliente (para grafico)
        [HttpGet("{Cliente}/{sTipo}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Docs_con_Saldo>>> GetDocs_con_Saldo(string Cliente, string sTipo)
        {
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@cliente",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = Cliente
                },
                new SqlParameter() {
                    ParameterName = "@tipo",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 10,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sTipo
                }
            };
            string StoredProc = "exec sp_m_Docs_con_Saldo @cliente, @tipo";
            return await _context.docs_Con_Saldos.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
