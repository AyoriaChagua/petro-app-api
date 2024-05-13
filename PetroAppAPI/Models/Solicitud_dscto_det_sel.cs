using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Solicitud_dscto_det_sel
    {
        public int Id_solicitud_dscto { get; set; }
        public int Id_solicitud_dscto_det { get; set; }
        public string Id_planta { get; set; }
        public string Desc_planta { get; set; }
        public string Id_condicion_pago { get; set; }
        public string Desc_condicion_pago { get; set; }
        public string Id_articulo_subclase { get; set; }
        public string Id_articulo { get; set; }
        public string Desc_articulo { get; set; }
        public string Id_almacen { get; set; }
        public string Desc_almacen { get; set; }
        public double Factor_sin_igv { get; set; }
        public double Factor_con_igv { get; set; }
        public string Id_punto_venta { get; set; }
        public string Desc_punto_venta { get; set; }
        public string Id_estado_dscto { get; set; }

    }
}
