using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Articulo
    {
        [Key]
        public string Id_articulo { get; set; }
        public string Descripcion_corta { get; set; }
        public string Id_articulo_subclase { get; set; }
    }
}
