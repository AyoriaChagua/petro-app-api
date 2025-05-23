using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    [Table("PURCHASE_ORDER_PVO")]
    public class PurchaseRequest
    {
        public string Cia { get; set; }
        public string Nro_scop { get; set; }
        public string Id_planta { get; set; }
        public string Id_cliente { get; set; }
        public string Id_punto_venta { get; set; }
        public string Placa_cisterna { get; set; }
        public string Placa_tractor { get; set; }
        public string Id_chofer { get; set; }
        public string Flag_registro_movil { get; set; }
        public string? Flag_validado { get; set; }
        public DateTime? Fecha_pedido { get; set; }
        public DateTime? Fecha_despacho { get; set; }
        public string? Asesor_valida { get; set; }
        public string? Usuario_sistema { get; set; }
        public string? Usuario_mod { get; set; }
        public string? Usuario_anula { get; set; }
        public string? Usuario_reactiva { get; set; }
        public string? Id_condicion_pago { get; set; }
        public string? Estado_pvo { get; set; }

    }
}
