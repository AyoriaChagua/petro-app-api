using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Vehiculo
    {
        public string Placa_tractor { get; set; }
        public string Placa_cisterna { get; set; }
        public string Cia { get; set; }
        public string Id_estado { get; set; }
    }
}
