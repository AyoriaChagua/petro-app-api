using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class PurchaseEditRequest
    {
        [Key]
        public string Nro_scop { get; set; }
        public string Id_planta { get; set; }
        public string Id_cliente { get; set; }
        public string Placa_cisterna { get; set; }
        public string Placa_tractor { get; set; }
        public string Id_chofer { get; set; }
        public DateTime? Fecha_pedido { get; set; }
        public string? Usuario_mod { get; set; }
        public string? Id_condicion_pago { get; set; }
    }
}
