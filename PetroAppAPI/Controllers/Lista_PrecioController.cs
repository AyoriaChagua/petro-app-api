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
    public class Lista_PrecioController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public Lista_PrecioController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // RECUPERA LAS LISTAS DE PRECIO
        [HttpGet("{sPlanta}/{sAlmacen}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Lista_Precio>>> GetDescuento(string sPlanta, string sAlmacen)
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
                    ParameterName = "@Almacen",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 4,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = sAlmacen
                }
            };
            string StoredProc = "exec sp_m_Lista_Precios @Planta, @Almacen";
            return await _context.lista_Precios.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
