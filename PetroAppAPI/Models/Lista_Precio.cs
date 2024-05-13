using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Lista_Precio
    {
        [Key]
        public string Id_lista_precio { get; set; }
        public string Lista_precio { get; set; }
    }
}
