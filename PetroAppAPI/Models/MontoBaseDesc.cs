using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class MontoBaseDesc
    {
        [Key]
        public string Id_lista_precio { get; set; }
        public double Monto_base { get; set; }
    }
}
