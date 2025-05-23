using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class PurchaseEditResponse
    {
        [Key]
        public string Nro_scop { get; set; }
        public string Id_planta { get; set; }
        public string Descripcion_planta { get; set; }
        public string Id_cliente { get; set; }
        public string Descripcion_cliente { get; set; }
        public string Id_punto_venta { get; set; }
        public string Descripcion_punto_venta { get; set; }
        public string Placa_tractor { get; set; }
        public string Placa_cisterna { get; set; }
        public string Descripcion_chofer { get; set; }
        public string Id_chofer { get; set; }
        public string Id_condicion_pago { get; set; }
        public string Descripcion_condicion_pago { get; set; }
    }
}
