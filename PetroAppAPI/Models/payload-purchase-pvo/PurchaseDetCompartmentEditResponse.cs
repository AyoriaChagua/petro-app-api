using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class PurchaseDetCompartmentEditResponse
    {
        public string Nro_scop { get; set; }
        public int Nro_compartimiento { get; set; }
        public decimal Capacidad { get; set; }
        public string Descripcion_articulo { get; set; }
        public string Id_articulo { get; set; }
        public string Placa_cisterna { get; set; }
        public string Placa_tractor { get; set; }
        public string Cia { get; set; }
        public int Volumen_ocupado { get; set; }
    }
}
