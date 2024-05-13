using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Cliente
    {
        [Key]
        public string id_cliente { get; set; }
        public string descripcion { get; set; }
        public string Nro_di { get; set; }
        public string Cia { get; set; }
        public decimal Limite_credito { get; set; }
        public decimal Saldo { get; set; }
        public string Id_estado { get; set; }
    }
}
