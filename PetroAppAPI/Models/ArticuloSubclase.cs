using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class ArticuloSubclase
    {
        [Key]
        public string Id_articulo_Subclase { get; set; }
        //public string Descripcion { get; set; }
    }
}
