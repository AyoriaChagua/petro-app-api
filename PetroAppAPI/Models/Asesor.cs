using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Asesor
    {
        [Key]
        public string Id_Asesor { get; set; }
        public string Nombre { get; set; }
        public string Cia { get; set; }
        public string Id_estado { get; set; }
        public string Cellphone { get; set; }
        public string E_mail { get; set; }
    }
}
