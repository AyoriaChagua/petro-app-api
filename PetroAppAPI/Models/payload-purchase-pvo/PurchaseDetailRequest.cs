using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    [Table("PURCHASE_ORDER_DET_PVO")]
    public class PurchaseDetailRequest
    {
        public string Cia { get; set; }
        public string Nro_scop { get; set; }
        public string Id_articulo { get; set; }
        public int Volumen_pvo { get; set; }
        public string Observaciones { get; set; }
        public string Flag_compartido { get; set; }
        public int? Id_descuento { get; set; }
        public string? Id_almacen { get; set; }
        public string? Id_lista_precio { get; set; }
    }
}
