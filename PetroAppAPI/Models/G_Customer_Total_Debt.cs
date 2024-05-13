using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class G_Customer_Total_Debt
    {
        [Key]
        public string Document { get; set; }
        public string Planta { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Fecha_vencimiento { get; set; }
        public decimal Saldo { get; set;}
        public string Cond_pago { get; set; }
        public int Cond_pago_dias { get; set; }
        public string Currency { get; set; }
        public decimal Total_fac { get; set; }
        public decimal Total_per { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }
        public int? Days_difference { get; set; }
    }
}
