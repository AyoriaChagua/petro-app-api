using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Vehiculo_Compartimiento
    {
        public string Placa_tractor { get; set; }
        public string Placa_cisterna { get; set; }
        public string Cia { get; set; }
        public string Id_estado { get; set; }
        public int Nro_Compartimiento { get; set; }
        public decimal Capacidad { get; set; }
    }
}
