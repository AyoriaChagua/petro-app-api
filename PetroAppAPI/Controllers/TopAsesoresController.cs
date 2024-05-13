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
    public class TopAsesoresController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public TopAsesoresController(ApiDbContext contexto)
        {
            _context = contexto;
        }


        // TRAE ranking de asesores - iPeriodo 1 ó 0 - sTipoCrecimiento PORCENTAJE ó VOLUMEN
        [HttpGet("{iPeriodo}/{sTipoCrecimiento}/{sClasificaTipoNego}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TopAsesor>>> Get(int iPeriodo, string sTipoCrecimiento, string sClasificaTipoNego)
        {
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@Periodo",
                    SqlDbType =  System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = iPeriodo
                },
                new SqlParameter() {
                    ParameterName = "@TipoCrecimiento",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 10,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sTipoCrecimiento
                },
                new SqlParameter() {
                    ParameterName = "@ClasificaTipoNego",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 2,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sClasificaTipoNego
                }
            };
            string StoredProc = "exec sp_m_Top_Asesores @Periodo, @TipoCrecimiento, @ClasificaTipoNego";
            return await _context.topAsesors.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
