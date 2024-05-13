using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Usuario
    {
        [Key]
        public string Id_usuario { get; set; }
        public string Descripcion { get; set; }
        public string Id_celular { get; set; }
        public string Id_estado { get; set; }
        public string Flag_m_aprueba_dscto { get; set; }
        public string Flag_m_request_discount { get; set; }
    }
}
