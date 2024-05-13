using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class VentasAlCliente
    {
        [Key]
        public string id_cliente { get; set; }
        public decimal Mes_act_die { get; set; }
        public decimal Mes_act_gas { get; set; }
        public decimal Mes_1_die { get; set; }
        public decimal Mes_1_gas { get; set; }
        public decimal Mes_2_die { get; set; }
        public decimal Mes_2_gas { get; set; }
        public decimal Mes_3_die { get; set; }
        public decimal Mes_3_gas { get; set; }
        public decimal Prom_1_3_die { get; set; }
        public decimal Prom_1_3_gas { get; set; }
    }
}
