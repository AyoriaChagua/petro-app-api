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
    public class IngresosResumenController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public IngresosResumenController(ApiDbContext contexto)
        {
            _context = contexto;
        }


        // TRAE las Ventas Resumen (para grafico)
        [HttpGet("{sPeriodo}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Ventas_Resumen>>> GetResumen(string sPeriodo)
        {
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@Periodo",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 2,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sPeriodo
                }};
            string StoredProc = "exec sp_m_Ingresos_Resumen @Periodo";
            return await _context.ventas_Resumens.FromSqlRaw(StoredProc, param).ToListAsync();
        }


        // TRAE el detalle de los Ingresos de las Ventas hacia la Lista 
        [HttpGet("{sFechaIni}/{sFechaFin}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Ingresos_Det_XVentas>>> Get(string sFechaIni, string sFechaFin)
        {
            var param = new SqlParameter[] {
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
                } /* ,
                new SqlParameter() {
                    ParameterName = "@sTipoReport",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 11,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sTipoReport
                }*/
            };
            string StoredProc = "exec sp_m_Ingresos_det_Resumen @FechaIni, @FechaFin";
            return await _context.ingresos_Det_XVentass.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
