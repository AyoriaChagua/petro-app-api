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
    public class VencimientoCubicacionController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public VencimientoCubicacionController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // TRAE las Ventas al Cliente (para grafico)
        [HttpGet("{sPlanta}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<VencimientoCubicacion>>> Get(string sPlanta)
        {
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@Planta",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 3,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sPlanta
                }};
            string StoredProc = "exec sp_m_Vencimiento_Cubicacion @Planta";
            return await _context.vencimientoCubicacions.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
