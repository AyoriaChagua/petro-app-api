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
    public class Discount_ApprovedController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public Discount_ApprovedController(ApiDbContext contexto)
        {
            _context = contexto;
        }


        // GRABA DESCUENTOS APROBADOS
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostDescuento(List<Descuento> Descuento)
        {
            int iSolicitudDscto = 0;
            string sComentarioResp = null, sFechaIni, sFechaFin;
            bool b1raFila = false;

            DataTable miDataTabla = new DataTable();
            miDataTabla.Columns.Add("Fecha_ini", typeof(string));
            miDataTabla.Columns.Add("Fecha_fin", typeof(string));
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
            miDataTabla.Columns.Add("Id_estado_dscto", typeof(string));
            miDataTabla.Columns.Add("usuario", typeof(string));
            miDataTabla.Columns.Add("Id_solicitud_dscto_det", typeof(int));

            foreach (var misCamposTabla in Descuento)
            {   
                if (b1raFila == false)
                {
                    iSolicitudDscto = misCamposTabla.Id_solicitud_dscto;
                    sComentarioResp = misCamposTabla.Comentario_Resp;
                    b1raFila = true;
                }
                else
                {
                    sFechaIni = misCamposTabla.Fecha_ini.ToString("yyyy-MM-dd");
                    sFechaFin = misCamposTabla.Fecha_fin.ToString("yyyy-MM-dd");
                    miDataTabla.Rows.Add(new Object[] {
                    sFechaIni, sFechaFin, misCamposTabla.Id_moneda, misCamposTabla.Factor_con_igv,
                    misCamposTabla.Id_condicion_pago, misCamposTabla.Id_cliente, misCamposTabla.Id_punto_venta, misCamposTabla.Id_planta, misCamposTabla.Id_almacen,
                    misCamposTabla.Id_articulo_grupo, misCamposTabla.Id_articulo_clase, misCamposTabla.Id_articulo_subclase, 
                    misCamposTabla.Id_estado, misCamposTabla.usuario_sistema, misCamposTabla.Id_solicitud_dscto_det });
                }   
            }

            var param2 = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@SolicitudDscto",
                            SqlDbType =  System.Data.SqlDbType.Int,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = iSolicitudDscto
                        },
                        new SqlParameter() {
                            ParameterName = "@ComentarioResp",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size = 500,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = sComentarioResp
                        },
                        new SqlParameter() {
                            ParameterName = "@ty_m_Descuento_save_approved",
                            SqlDbType =  System.Data.SqlDbType.Structured,
                            Value = miDataTabla,
                            TypeName = "dbo.ty_m_Descuento_save_approved"
                        },
                        new SqlParameter() {
                            ParameterName = "@bOkOut",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 1,
                            Direction = System.Data.ParameterDirection.Output,
                        } };

            int affectedRows2 = await _context.Database.ExecuteSqlRawAsync("[dbo].[sp_m_Descuento_save_approved] @SolicitudDscto, @ComentarioResp, @ty_m_Descuento_save_approved, @bOkOut out", param2);
            string sResul = Convert.ToString(param2[3].Value);
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
