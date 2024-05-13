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
    public class Simulacion_VentaController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public Simulacion_VentaController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // RECUPERA LAS LISTAS DE PRECIO
        [HttpGet("{sPlanta}/{sArticulo}/{sCliente}/{sListaPrecio}/{sPuntoVenta}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Simulacion_Venta>>> GetDescuento(string sPlanta, string sArticulo, string sCliente, string sListaPrecio, string sPuntoVenta)
        {
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@Planta",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 3,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sPlanta
                },
                new SqlParameter() {
                    ParameterName = "@Articulo",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sArticulo
                },
                new SqlParameter() {
                    ParameterName = "@Cliente",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sCliente
                },
                new SqlParameter() {
                    ParameterName = "@ListaPrecio",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 5,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sListaPrecio
                },
                new SqlParameter() {
                    ParameterName = "@PuntoVenta",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 20,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sPuntoVenta
                }
            };
            string StoredProc = "exec sp_m_Simulacion_Venta @Planta, @Articulo, @Cliente, @ListaPrecio, @PuntoVenta";
            return await _context.simulacion_ventas.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
