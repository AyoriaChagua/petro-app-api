using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class VencimientoCubicacion
    {
        [Key]
        public string Placa { get; set; }
        public DateTime Fecha_fin { get; set; }
        public string Dias { get; set; }
        public string Empresa_transporte { get; set; }
        public int Orden { get; set; }
    }
}
