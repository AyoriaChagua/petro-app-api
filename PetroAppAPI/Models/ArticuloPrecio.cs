using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class ArticuloPrecio
    {
        [Key]
        public string Articulo { get; set; }
        public double Precio { get; set; }
    }
}
