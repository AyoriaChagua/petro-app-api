using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Ventas_Resumen
    {
        [Key]
        public string Descripcion { get; set; }
        public string Periodo_6 { get; set; }
        public string Periodo_5 { get; set; }
        public string Periodo_4 { get; set; }
        public string Periodo_3 { get; set; }
        public string Periodo_2 { get; set; }
        public string Periodo_1 { get; set; }
        public string Periodo_0 { get; set; }
    }
}
