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
    public class CondiPagoController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public CondiPagoController(ApiDbContext contexto)
        {
            _context = contexto;
        }
        
        // RECUPERA LAS CONDICIONES DE PAGO ASIGNADOS AL CLIENTE
        [HttpGet("{sCliente}")]
        [Authorize] 
        public async Task<ActionResult<IEnumerable<CondiPago>>> GetCondiPago(string sCliente)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Cliente",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 10,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = sCliente
                        }};
            string StoredProc = "exec sp_m_CondiPagoXcli @Cliente";
            return await _context.CondiPago.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
