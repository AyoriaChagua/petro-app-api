using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Docs_con_Saldo_Credito
    {
        [Key]
        public string Autogenerado { get; set; }
        public string Planta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? Monto_credito { get; set; }
        public decimal? Saldo { get; set; }
    }
}
