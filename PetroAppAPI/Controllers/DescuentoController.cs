using ApiTestIIS.Contexts;
using ApiTestIIS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DescuentoController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public DescuentoController(ApiDbContext contexto)
        {
            _context = contexto;
        }

        // RECUPERA LOS DESCUENTOS
        [HttpGet("{sCliente}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Descuento>>> GetDescuento(string sCliente)
        {
            var param = new SqlParameter[] {
                            new SqlParameter() {
                                ParameterName = "@Cliente",
                                SqlDbType =  System.Data.SqlDbType.VarChar,
                                Size = 10,
                                Direction = System.Data.ParameterDirection.Input,
                                Value = sCliente
                            }};
            string StoredProc = "exec sp_m_descuentos @Cliente";
            return await _context.descuento.FromSqlRaw(StoredProc, param).ToListAsync();
        }


        // GRABA DESCUENTOS
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostDescuento(List<Descuento> Descuento)
        {
            DataTable miDataTabla = new DataTable();
            miDataTabla.Columns.Add("Id_descuento", typeof(int));
            miDataTabla.Columns.Add("Fecha_ini", typeof(DateTime));
            miDataTabla.Columns.Add("Fecha_fin", typeof(DateTime));
            miDataTabla.Columns.Add("Id_moneda", typeof(string));
            miDataTabla.Columns.Add("Factor_con_igv", typeof(decimal));
            miDataTabla.Columns.Add("Id_condicion_pago", typeof(string));
            miDataTabla.Columns.Add("Id_cliente", typeof(string));
            miDataTabla.Columns.Add("Id_punto_venta", typeof(string));
            miDataTabla.Columns.Add("Id_planta", typeof(string));
            miDataTabla.Columns.Add("Id_almacen", typeof(string));
            miDataTabla.Columns.Add("Id_articulo_grupo", typeof(string));
            miDataTabla.Columns.Add("Id_articulo_clase", typeof(string));
            miDataTabla.Columns.Add("Id_articulo_subclase", typeof(string));
            miDataTabla.Columns.Add("Id_estado", typeof(string));
            miDataTabla.Columns.Add("usuario", typeof(string));

            foreach (var misCamposTabla in Descuento)
            {
                miDataTabla.Rows.Add(new Object[] {
                misCamposTabla.Id_descuento, misCamposTabla.Fecha_ini, misCamposTabla.Fecha_fin, misCamposTabla.Id_moneda, misCamposTabla.Factor_con_igv,
                misCamposTabla.Id_condicion_pago, misCamposTabla.Id_cliente, misCamposTabla.Id_punto_venta, misCamposTabla.Id_planta, misCamposTabla.Id_almacen,
                misCamposTabla.Id_articulo_grupo, misCamposTabla.Id_articulo_clase, misCamposTabla.Id_articulo_subclase, misCamposTabla.Id_estado, misCamposTabla.usuario_sistema });
            }

            var param2 = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@ty_m_Descuento_save",
                            SqlDbType =  System.Data.SqlDbType.Structured,
                            Value = miDataTabla,
                            TypeName = "dbo.ty_m_Descuento_save"
                        },
                        new SqlParameter() {
                            ParameterName = "@bOkOut",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 1,
                            Direction = System.Data.ParameterDirection.Output,
                        } };

            int affectedRows2 = await _context.Database.ExecuteSqlRawAsync("[dbo].[sp_m_Descuento_save] @ty_m_Descuento_save, @bOkOut out", param2);
            string sResul = Convert.ToString(param2[1].Value);
            if (sResul == "1")
            {
                return Ok();
            }
            else
            {
                return BadRequest("Occurrio un error");
            }
        }
    }
}
