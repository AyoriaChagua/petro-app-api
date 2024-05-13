using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class UsuarioInfo
    {
        [Key]
        public string Id_usuario { get; set; }
        public string Pass_word { get; set; }
        public string Descripcion { get; set; }
        //public string Id_estado { get; set; }
    }
}
