using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Docs_con_Saldo
    {
        [Key]
        public string Documento { get; set; }
        public string Planta { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime? Fecha_vencimiento { get; set; }
        public decimal? Saldo { get; set; }
    }
}
