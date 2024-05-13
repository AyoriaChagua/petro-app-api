using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Docs_Cliente_Mes
    {
        public string Documento { get; set; }
        public string Planta { get; set; }
        public DateTime Fecha { get; set; }
        public string Articulo { get; set; }
        public decimal? Cantidad { get; set; }
    }
}
