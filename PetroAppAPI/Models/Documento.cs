using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Documento
    {
        public string Cia { get; set; }
        public string Id_planta { get; set; }
        public string Id_tipo_doc { get; set; }
        public string Serie { get; set; }
        public string Nro_documento { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Fecha_vencimiento { get; set; }
        public string Id_cliente { get; set; }
        public string Flag_condicion_pago_credito { get; set; }
        public int Condicion_pago_dias_neto { get; set; }
        public string Id_moneda_doc { get; set; }
        public decimal Total { get; set; }
        public decimal? Monto_per { get; set; }
        public decimal Saldo_doc { get; set; }
        public decimal? Saldo_per { get; set; }
        public string id_estado_doc { get; set; }        
    }
}
