using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Simulacion_Venta
    {
        [Key]
        public double Precio { get; set; }
        public double Igv { get; set; }
        public double? Porc_per { get; set; }
    }
}
