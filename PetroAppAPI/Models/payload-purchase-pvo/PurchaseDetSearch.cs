using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace ApiTestIIS.Models
{
    public class PurchaseDetSearch
    {
        [JsonPropertyName("nro_scop")]
        public string Nro_scop { get; set; }

        [JsonPropertyName("volumen_pvo")]
        public int Volumen_pvo { get; set; }

        [JsonPropertyName("id_articulo")]
        public string Id_articulo { get; set; }

        [JsonPropertyName("articulo_descripcion")]
        public string Articulo_descripcion { get; set; }

        [JsonPropertyName("purchaseSearch")]
        public PurchaseSearch PurchaseSearch { get; set; }
    }
}
