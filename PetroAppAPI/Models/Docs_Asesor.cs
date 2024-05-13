using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Docs_Asesor
    {
        public string Cliente { get; set; }
        public DateTime Fecha { get; set; }
        public string Planta { get; set; }
        public string Documento { get; set; }
        public decimal? Cantidad { get; set; }
    }
}
