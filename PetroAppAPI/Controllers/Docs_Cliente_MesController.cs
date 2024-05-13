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
    public class Docs_Cliente_MesController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public Docs_Cliente_MesController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // TRAE las Ventas al Cliente (para grafico)
        [HttpGet("{Cliente}/{sFechaIni}/{sFechaFin}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Docs_Cliente_Mes>>> Get(string Cliente, string sFechaIni, string sFechaFin)
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
                    ParameterName = "@FechaIni",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 10,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sFechaIni
                },
                new SqlParameter() {
                    ParameterName = "@FechaFin",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 10,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sFechaFin
                }
            };
            string StoredProc = "exec sp_m_Docs_Cliente_Mes @cliente, @FechaIni, @FechaFin";
            return await _context.docs_Cliente_Mess.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
