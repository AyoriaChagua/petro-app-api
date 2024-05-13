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
    public class Docs_con_Saldo_CreditoController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public Docs_con_Saldo_CreditoController(ApiDbContext contexto)
        {
            _context = contexto;
        }


        // TRAE los Docs con Saldo del Cliente (para grafico)
        [HttpGet("{Cliente}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Docs_con_Saldo_Credito>>> GetDocs_con_Saldo_Credito(string Cliente)
        {
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@cliente",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = Cliente
                }
            };
            string StoredProc = "exec sp_m_Docs_con_Saldo_Credito @cliente";
            return await _context.docs_con_Saldo_Creditos.FromSqlRaw(StoredProc, param).ToListAsync();
        }

        
        // customer total debt    05/04/24
        [HttpGet("Total/{sCliente}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<G_Customer_Total_Debt>>> GetTotal(string sCliente)
        {
            try
            {
                //DateTime targetDate = new DateTime(2024, 04, 06);                && d.Fecha == targetDate
                var Obj = await (from d in _context.Documento
                                 join p in _context.Planta on new { d.Cia, d.Id_planta } equals new { p.Cia, Id_planta = p.Id_Planta }
                                 where d.Cia == "06" && d.id_estado_doc == "01" && (d.Saldo_doc > 0 || d.Saldo_per > 0) 
                                     && !new[] { "n/c","per","pep" }.Contains(d.Id_tipo_doc) && d.Id_cliente == sCliente 
                                 orderby d.Fecha_vencimiento
                                 select new G_Customer_Total_Debt
                                 {
                                     Planta = p.Descripcion,
                                     Document = d.Nro_documento,
                                     Fecha = d.Fecha,
                                     Fecha_vencimiento = d.Fecha_vencimiento,
                                     Saldo = d.Saldo_doc + (d.Saldo_per ?? 0),
                                     Cond_pago = d.Flag_condicion_pago_credito=="1" ? "CREDITO" : "CONTADO",
                                     Cond_pago_dias= d.Condicion_pago_dias_neto,
                                     Currency= d.Id_moneda_doc=="01" ? "SOLES":"DOLARES",
                                     Total_fac= d.Total,
                                     Total_per= d.Monto_per ?? 0,
                                     Total = d.Total + (d.Monto_per ?? 0),
                                     Status= DateTime.Now.Date < d.Fecha_vencimiento.Date ? "POR VENCER" :
                                        DateTime.Now.Date == d.Fecha_vencimiento.Date ? "VENCE HOY" : 
                                        "VENCIDO",
                                     Days_difference= DateTime.Now.Date < d.Fecha_vencimiento.Date ? (d.Fecha_vencimiento.Date - DateTime.Now.Date).Days :
                                        DateTime.Now.Date > d.Fecha_vencimiento.Date ? ( DateTime.Now.Date - d.Fecha_vencimiento.Date).Days :
                                        (int?)null  
                                 }).ToListAsync();
                return Ok(Obj);
            }
            catch (Exception ex)
            {
                // Log the exception for further investigation
                //Console.WriteLine($"An error occurred: {ex.Message}");

                // Return a more descriptive error message
                return StatusCode(500, $"Un error occurrio mientras se procesaba tu request: {ex.Message}");
            }
        }
    }
}
