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
    public class MargenController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public MargenController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // RECUPERA LOS MARGENES 
        [HttpGet("{sPlanta}/{sArticulo}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Margen>>> GetMargen(string sPlanta, string sArticulo)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Planta",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 6,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = sPlanta
                        },
                        new SqlParameter() {
                            ParameterName = "@Articulo",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 20,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = sArticulo
                        }};
            string StoredProc = "exec sp_m_margen @Planta, @Articulo";
            return await _context.margen.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
