using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ApiTestIIS.Models
{
    public class PurchaseSearch
    {
        [Key]
        [JsonPropertyName("nro_scop")]
        public string Nro_scop { get; set; }

        [JsonPropertyName("descripcion_cliente")]
        public string Descripcion_cliente { get; set; }

        [JsonPropertyName("estado_pvo")]
        public string Estado_pvo { get; set; }

        [JsonPropertyName("fecha_pedido")]
        public DateTime Fecha_pedido { get; set; }

        [JsonPropertyName("fecha_despacho")]
        public DateTime? Fecha_despacho { get; set; }

        [JsonPropertyName("productos")]
        public List<PurchaseDetSearch> Detalle { get; set; } = new List<PurchaseDetSearch>();
    }
}
