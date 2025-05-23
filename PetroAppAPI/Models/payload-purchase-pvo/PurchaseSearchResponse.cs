using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ApiTestIIS.Models
{
    public class PurchaseSearchResponse
    {
        [JsonPropertyName("pedido")]
        public PurchaseSearch Pedido { get; set; }
    }
}
