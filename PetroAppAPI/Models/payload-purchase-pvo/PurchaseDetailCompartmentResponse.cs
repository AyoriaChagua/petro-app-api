using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class PurchaseDetailCompartmentResponse
    {
        public string Nro_scop { get; set; }
        public int Nro_compartimiento { get; set; }
        public decimal Capacidad { get; set; }
        public string Descripcion_articulo { get; set; }
        public string Id_articulo { get; set; }
    }
}
