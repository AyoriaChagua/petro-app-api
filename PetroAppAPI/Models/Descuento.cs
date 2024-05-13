using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Descuento
    {
        [Key]
        public int? Id_descuento { get; set; }
        public DateTime Fecha_ini { get; set; }
        public DateTime Fecha_fin { get; set; }
        public string Id_cliente { get; set; }
        public string Desc_cliente { get; set; }
        public string Id_planta { get; set; }
        public string Desc_planta { get; set; }
        public string Id_almacen { get; set; }
        public string Desc_almacen { get; set; }
        public string Id_articulo_subclase { get; set; }
        public string Desc_articulo_subclase { get; set; }
        public string Id_condicion_pago { get; set; }
        public string Desc_condicion_pago { get; set; }
        public double? Factor_sin_igv { get; set; }
        public double Factor_con_igv { get; set; }
        public string Id_articulo_grupo { get; set; }
        public string Desc_articulo { get; set; }

        public string Id_moneda { get; set; }
        public string Id_punto_venta { get; set; }
        public string Id_articulo_clase { get; set; }
        public string Id_estado { get; set; }
        public string usuario_sistema { get; set; }
        //*********************************************
        [NotMapped]
        public int Id_solicitud_dscto { get; set; }
        [NotMapped]
        public int Id_solicitud_dscto_det { get; set; }
        [NotMapped]
        public string Comentario_Resp { get; set; }
        
    }
}
