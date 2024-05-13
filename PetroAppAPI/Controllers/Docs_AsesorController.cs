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
    public class Docs_AsesorController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public Docs_AsesorController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // TRAE los Docs del Asesor o Administrador
        [HttpGet("{sAsesor}/{sClasif_Tipo_Negocio}/{iMes}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Docs_Asesor>>> GetDocs_con_Saldo(string sAsesor, string sClasif_Tipo_Negocio, int iMes)
        {
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@Asesor",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 5,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sAsesor
                },
                new SqlParameter() {
                    ParameterName = "@Clasif_Tipo_Negocio",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 2,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sClasif_Tipo_Negocio
                },
                new SqlParameter() {
                    ParameterName = "@Mes",
                    SqlDbType =  System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = iMes
                }
            };
            string StoredProc = "exec sp_m_Docs_Asesor @Asesor, @Clasif_Tipo_Negocio, @Mes";
            return await _context.docs_asesors.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
