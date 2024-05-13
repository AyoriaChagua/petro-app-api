using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Ingresos_Det_XVentas
    {
        [Key]
        public string Banco { get; set; }
        public string Cta_Bancaria { get; set; }
        public string Moneda { get; set; }
        public decimal? Monto { get; set; }
    }
}
