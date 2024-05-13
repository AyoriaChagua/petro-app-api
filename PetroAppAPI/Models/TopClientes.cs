using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class TopClientes
    {
        [Key]
        public string Cliente { get; set; }
        public decimal? Cantidad_die { get; set; }
        public decimal? Cantidad_gas { get; set; }
        public decimal? Cantidad_Total { get; set; }

    }
}
