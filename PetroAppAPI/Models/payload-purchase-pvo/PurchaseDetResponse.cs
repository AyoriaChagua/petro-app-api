using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class PurchaseDetResponse
    {
        public string Nro_scop { get; set; }
        public string Descripcion_corta { get; set; }
        public int Volumen_pvo { get; set; }
        public double Factor_con_igv { get; set; }
        public double Factor_sin_igv { get; set; }
        public string Direccion_almacen { get; set; }
        public string Descripcion_almacen { get; set; }
        public string Flag_compartido { get; set; }
        public string Observaciones { get; set; }
        public string Id_articulo { get; set; }
        public string Condicion_pago { get; set; }
    }
}
