using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Info_Saldos_Cliente
    {
        [Key]
        public decimal Deuda_sin_vencer { get; set; }
        public decimal? Deuda_vencida { get; set; }
        public decimal? Deuda_vencida_min { get; set; }
        public decimal? Monto_a_favor { get; set; }
    }
}
