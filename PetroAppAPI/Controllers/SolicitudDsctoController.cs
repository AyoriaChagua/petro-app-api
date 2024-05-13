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
    public class SolicitudDsctoController : ControllerBase
    {
        private readonly ApiDbContext _context;
        public SolicitudDsctoController(ApiDbContext contexto)
        {
            _context = contexto;
        }


        // RECUPERA LAS SOLICITUDES DE DESCUENTO - FOR REQUESTER
        [HttpGet("{sEstado}/{sFecha}/{sUser}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Solicitud_dscto_sel>>> GetSolicitud_Dscto_sel(string sEstado, string sFecha, string sUser)
        {
            var param = new SqlParameter[] {
                            new SqlParameter() {
                                ParameterName = "@Estado",
                                SqlDbType =  System.Data.SqlDbType.VarChar,
                                Size = 2,
                                Direction = System.Data.ParameterDirection.Input,
                                Value = sEstado
                            },
                            new SqlParameter() {
                                ParameterName = "@Fecha",
                                SqlDbType =  System.Data.SqlDbType.VarChar,
                                Size = 10,
                                Direction = System.Data.ParameterDirection.Input,
                                Value = sFecha
                            },
                            new SqlParameter() {
                                ParameterName = "@User",
                                SqlDbType =  System.Data.SqlDbType.VarChar,
                                Size = 20,
                                Direction = System.Data.ParameterDirection.Input,
                                Value = sUser
                            }};
            string StoredProc = "exec sp_m_solicitud_dscto_sel @Estado, @Fecha, @User";
            return await _context.solicitud_Dscto_Sel.FromSqlRaw(StoredProc, param).ToListAsync();
        }


        // IT RETRIEVE DISCONT REQUESTS FOR APPROVERS
        [HttpGet("{sEstado}/{sFechaIni}/{sFechaFin}/{sSolicitante}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Solicitud_dscto_sel>>> GetRequest4Approver(string sEstado, string sFechaIni, string sFechaFin, string sSolicitante)
        {
            var param = new SqlParameter[] {
                            new SqlParameter() {
                                ParameterName = "@Estado",
                                SqlDbType =  System.Data.SqlDbType.VarChar,
                                Size = 2,
                                Direction = System.Data.ParameterDirection.Input,
                                Value = sEstado
                            },
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
                                ParameterName = "@User",
                                SqlDbType =  System.Data.SqlDbType.VarChar,
                                Size = 20,
                                Direction = System.Data.ParameterDirection.Input,
                                Value = sSolicitante
                            }};
            string StoredProc = "exec sp_m_solicitud_dscto_sel_approver @Estado, @FechaIni, @FechaFin, @User";
            return await _context.solicitud_Dscto_Sel.FromSqlRaw(StoredProc, param).ToListAsync();
        }


        // RECUPERA LAS SOLICITUDE DE DESCUENTO - DETALLE
        [HttpGet("{nIdSolicitud}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Solicitud_dscto_det_sel>>> GetSolicitud_Dscto_Det_sel(int nIdSolicitud)
        {
            var param = new SqlParameter[] {
                            new SqlParameter() {
                                ParameterName = "@IdSolicitud",
                                SqlDbType =  System.Data.SqlDbType.Int,
                                //Size = 2,
                                Direction = System.Data.ParameterDirection.Input,
                                Value = nIdSolicitud
                            }};
            string StoredProc = "exec sp_m_solicitud_dscto_det_sel @IdSolicitud";
            return await _context.solicitud_dscto_det_sel.FromSqlRaw(StoredProc, param).ToListAsync();
        }


        // INSERTA LAS SOLICITUD DE DESCUENTO
        [HttpPost]
        [Authorize] 
        public async Task<ActionResult> SolicitudDsctoPost(List<SolicitudDscto> SolicitudDscto) 
        {
            string sId_Cliente = null, sComentario = null, sUsuarioSistema = null, sEstado = null, sImage = null, sCopy_to = null;
            bool b1raFila = false;
            
            DataTable miDataTabla = new DataTable();
            miDataTabla.Columns.Add("Id_solicitud_dscto_det", typeof(int));
            miDataTabla.Columns.Add("Id_articulo_subclase", typeof(string));
            miDataTabla.Columns.Add("Id_articulo_grupo", typeof(string));
            miDataTabla.Columns.Add("Id_condicion_pago", typeof(string));
            miDataTabla.Columns.Add("Id_planta", typeof(string));
            miDataTabla.Columns.Add("Factor_sin_igv", typeof(decimal));
            miDataTabla.Columns.Add("Factor_con_igv", typeof(decimal));
            miDataTabla.Columns.Add("Id_moneda", typeof(string));
            miDataTabla.Columns.Add("Id_punto_venta", typeof(string));
            miDataTabla.Columns.Add("Id_almacen", typeof(string));
            miDataTabla.Columns.Add("Cia", typeof(string));

            foreach (var miSolDscto in SolicitudDscto)
            {                
                if (b1raFila == false)
                {
                    sId_Cliente = miSolDscto.Id_cliente;
                    sComentario = miSolDscto.Comentario;
                    sUsuarioSistema = miSolDscto.Usuario_sistema;
                    sEstado = miSolDscto.Id_estado;
                    sImage = miSolDscto.Image;
                    sCopy_to = miSolDscto.Copy_to;
                    b1raFila = true;
                }
                else
                {   
                    //nId_solicitud_dscto_det++;
                    miDataTabla.Rows.Add(new Object[] {
                    miSolDscto.Id_solicitud_dscto_det, miSolDscto.Id_articulo_subclase, miSolDscto.Id_articulo_grupo, miSolDscto.Id_condicion_pago, miSolDscto.Id_planta, 
                    miSolDscto.Factor_sin_igv, miSolDscto.Factor_con_igv, miSolDscto.Id_moneda, miSolDscto.Id_punto_venta, miSolDscto.Id_almacen, null});
                }
            }
            //var xxx = miDataTabla.Rows[0]["Factor_sin_igv"];

            var param2 = new SqlParameter[] {
                        new SqlParameter() {
                            ParameterName = "@Id_cliente",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 20,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = sId_Cliente
                        },
                        new SqlParameter() {
                            ParameterName = "@Comentario",
                            SqlDbType =  System.Data.SqlDbType.NVarChar,
                            Size = 500,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = sComentario
                        },
                        new SqlParameter() {
                            ParameterName = "@UsuarioSistema",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 20,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = sUsuarioSistema
                        },
                        new SqlParameter() {
                            ParameterName = "@Estado",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 2,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = sEstado
                        },
                        new SqlParameter() {
                            ParameterName = "@Image",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 50,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = sImage
                        },
                        new SqlParameter() {
                            ParameterName = "@Copy_to",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 200,
                            Direction = System.Data.ParameterDirection.Input,
                            Value = sCopy_to
                        },
                        new SqlParameter() {
                            ParameterName = "@ty_m_Descuento",
                            SqlDbType =  System.Data.SqlDbType.Structured,
                            Value = miDataTabla,
                            TypeName = "dbo.ty_m_SoliciDscto"
                        },
                        new SqlParameter() {
                            ParameterName = "@bOkOut",
                            SqlDbType =  System.Data.SqlDbType.VarChar,
                            Size = 1,
                            Direction = System.Data.ParameterDirection.Output,
                        } };

            int affectedRows2 = await _context.Database.ExecuteSqlRawAsync("[dbo].[sp_m_soliciDscto] @Id_cliente, @Comentario, @UsuarioSistema, @Estado, @Image, @Copy_to, @ty_m_Descuento, @bOkOut out", param2);
            string sResul = Convert.ToString(param2[7].Value);
            if (sResul == "1") { 
                return Ok();
            }
            else
            {
                return BadRequest("Occurrio un error"); 
            }
        }
    }
}
