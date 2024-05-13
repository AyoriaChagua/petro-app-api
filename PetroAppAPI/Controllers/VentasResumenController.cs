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
    public class VentasResumenController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public VentasResumenController(ApiDbContext contexto)
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
                    Size = 4,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sPeriodo
                }};
            string StoredProc = "exec sp_m_Ventas_Resumen @Periodo";
            return await _context.ventas_Resumens.FromSqlRaw(StoredProc, param).ToListAsync();
        }


        // TRAE el detalle de las Ventas hacia la Lista 
        [HttpGet("{sFechaIni}/{sFechaFin}/{sTipoReport}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Ventas_Det_Resumen>>> Get(string sFechaIni, string sFechaFin, string sTipoReport)
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
                },
                new SqlParameter() {
                    ParameterName = "@sTipoReport",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 11,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sTipoReport
                }};
            string StoredProc = "exec sp_m_Ventas_Det_Resumen @FechaIni, @FechaFin, @sTipoReport";
            return await _context.ventas_Det_Resumens.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
