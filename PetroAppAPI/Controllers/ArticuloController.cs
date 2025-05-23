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
    public class ArticuloController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public ArticuloController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // RECUPERA LOS ARTICULOS AUTORIZADOS PARA EL CLIENTE
        [HttpGet("{sCliente}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Articulo>>> GetArticulo(string sCliente)
        {
            var param = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Cliente",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 10,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = sCliente
                        }};
            string StoredProc = "exec sp_m_articulo_2 @Cliente";
            return await _context.articulo.FromSqlRaw(StoredProc, param).ToListAsync();
        }

        [HttpGet("{idPlanta}/{idListaPrecio}")]
       // [Authorize]
        public async Task<ActionResult<IEnumerable<ArticuloPrecio>>> GetArticuloPrecio(string idPlanta, string idListaPrecio)
        {
            var param = new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@id_planta",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 6,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = idPlanta
                },
                new SqlParameter() {
                    ParameterName = "@id_lista_precio",
                    SqlDbType =  System.Data.SqlDbType.VarChar,
                    Size = 5,
                    Direction = System.Data.ParameterDirection.Input,
                    Value = idListaPrecio
                }
            };
            string StoredProc = "exec ObtenerPrecioArticulosAlmacen @id_planta, @id_lista_precio";
            return await _context.articuloPrecio.FromSqlRaw(StoredProc, param).ToListAsync();
        }
    }
}
