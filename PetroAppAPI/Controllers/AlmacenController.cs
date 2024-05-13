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
    public class AlmacenController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public AlmacenController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // RECUPERA LOS ALMACENES DE UNA PLANTA
        [HttpGet("{sPlanta}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Almacen>>> GetAlmacen(string sPlanta)
        {
            var param = new SqlParameter[] {
                            new SqlParameter() {
                                ParameterName = "@Planta",
                                SqlDbType =  System.Data.SqlDbType.VarChar,
                                Size = 6,
                                Direction = System.Data.ParameterDirection.Input,
                                Value = sPlanta
                            }};
            string StoredProc = "exec sp_m_Almacen @Planta";
            return await _context.almacen.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
