using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Margen
    {
        public string Desc_almacen { get; set; }
        public string Desc_lista_precio { get; set; }
        public double Precio_venta { get; set; }
        public double Precio_compra { get; set; }
        public decimal? Descuento { get; set; }
        public double Precio_compra_con_dscto { get; set; }
        public double Margen_ganacia { get; set; }
        public string Desc_tipo_descuento { get; set; }
    }
}
