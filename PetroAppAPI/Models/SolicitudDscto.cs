using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class SolicitudDscto
    {
        [Key]
        public string Id_cliente { get; set; }
        public string Comentario { get; set; }
        public string Usuario_sistema { get; set; }
        public string Id_estado { get; set; }
        public string Image { get; set; }
        public string Copy_to { get; set; }
        //*********************************************
        public string Id_solicitud_dscto { get; set; }
        public string Id_articulo_subclase { get; set; }
        public string Id_articulo_grupo { get; set; }
        public string Id_condicion_pago { get; set; }
        public string Id_planta { get; set; }
        public decimal Factor_sin_igv { get; set; }
        public decimal Factor_con_igv { get; set; }
        public int Id_solicitud_dscto_det { get; set; }
        public string Id_moneda { get; set; }
        public string Id_punto_venta { get; set; }
        public string Id_almacen { get; set; }
    }
}
