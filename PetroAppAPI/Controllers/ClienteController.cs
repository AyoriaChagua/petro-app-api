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
    public class ClienteController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public ClienteController(ApiDbContext contexto)
        {
            _context = contexto;
        }
        
        // TRAE LOS CLIENTES     A = actuales
        [HttpGet("{idLike}")]
        //[Authorize] 
        public async Task<ActionResult<IEnumerable<Cliente>>> Get(string idLike)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@idLike",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 1,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = idLike
                        }};
            string StoredProc = "exec sp_m_cliente @idLike";
            return await _context.Cliente.FromSqlRaw(StoredProc, param).ToListAsync();
        }

        
        // client with debt: Line and seller
        [HttpGet("Line/{sCliente}")]
        //[Authorize] 
        public async Task<ActionResult<IEnumerable<G_Client_for_Debt>>> Get_Line(string sCliente)
        {
            try
            {
                var Obj = await ( from c in _context.Cliente 
                                  join pv in _context.Punto_Venta_Asesor on new { c.Cia, Id_cliente=c.id_cliente } equals new { pv.Cia, pv.Id_cliente }
                                  join a in _context.Asesor on new {c.Cia, pv.Id_asesor } equals new { a.Cia, Id_asesor=a.Id_Asesor }
                                  where c.Cia== "06" && c.id_cliente== sCliente && c.Id_estado== "01" && pv.Id_estado== "01" && a.Id_estado== "01"
                                  select new G_Client_for_Debt 
                                  {
                                      Limite_credito= c.Limite_credito,
                                      Saldo= c.Saldo,
                                      Asesor= a.Nombre,
                                      Asesor_cell= a.Cellphone,
                                      Asesor_email= a.E_mail
                                  }).Distinct()
                                  .ToListAsync();
                return Ok(Obj);
            }
            catch (Exception ex)
            {
                //    Log the exception for further investigation
                //Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, $"Un error occurrio mientras se procesaba tu request: {ex.Message}");
            } 
        }
        
    }   
}
