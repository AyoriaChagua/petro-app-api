using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Planta
    {
        [Key]
        public string Id_Planta { get; set; }
        public string Descripcion { get; set; }
        public string Cia { get; set; }
        public string Id_estado { get; set; }
    }
}
