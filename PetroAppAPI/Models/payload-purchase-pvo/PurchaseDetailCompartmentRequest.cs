using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    [Table("PURCHASE_ORDER_DET_COMPARTMENT")]
    public class PurchaseDetailCompartmentRequest
    {
        public string Cia { get; set; }
        public string Nro_scop { get; set; }
        public string Id_articulo { get; set; }
        public string Placa_cisterna{ get; set; }
        public string Placa_tractor { get; set; }
        public int Nro_compartimiento { get; set; }
        public int Volumen_ocupado { get; set; }
    }
}
