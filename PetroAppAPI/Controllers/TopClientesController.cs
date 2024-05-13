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
    public class TopClientesController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public TopClientesController(ApiDbContext contexto)
        {
            _context = contexto;
        }


        // TRAE las Ventas al Cliente (para grafico)
        [HttpGet("{sFechaIni}/{sFechaFin}/{sClasificaTipoNego}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TopClientes>>> Get(string sFechaIni, string sFechaFin, string sClasificaTipoNego)
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
                    ParameterName = "@ClasificaTipoNego",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 2,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sClasificaTipoNego
                }};
            string StoredProc = "exec sp_m_Top_Clientes @FechaIni, @FechaFin, @ClasificaTipoNego";
            return await _context.topClientess.FromSqlRaw(StoredProc, param).ToListAsync();
        }


        // TRAE ranking Ventas al Cliente (para grafico) + Filtros
        [HttpGet("{sFechaIni}/{sFechaFin}/{sClasificaTipoNego}/{sPlanta}/{sFlgCondiPagoCredit}/{sAsesor}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TopClientes>>> GetVtas(string sFechaIni, string sFechaFin, string sClasificaTipoNego, string sPlanta, string sFlgCondiPagoCredit, string sAsesor)
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
                    ParameterName = "@ClasificaTipoNego",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 2,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sClasificaTipoNego
                },
                new SqlParameter() {
                    ParameterName = "@Planta",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 3,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sPlanta
                },
                new SqlParameter() {
                    ParameterName = "@FlgCondiPagoCredit",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 1,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sFlgCondiPagoCredit
                },
                new SqlParameter() {
                    ParameterName = "@Asesor",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 5,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sAsesor
                }};
            string StoredProc = "exec sp_m_Top_Clientes_ @FechaIni, @FechaFin, @ClasificaTipoNego, @Planta, @FlgCondiPagoCredit, @Asesor";
            return await _context.topClientess.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
