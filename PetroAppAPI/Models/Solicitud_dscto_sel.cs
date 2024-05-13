using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Solicitud_dscto_sel
    {
        [Key]
        public int Id_solicitud_dscto { get; set; }
        public string Id_cliente { get; set; }
        public string Desc_cliente { get; set; }
        public string Desc_usuario { get; set; }        
        public DateTime Fecha { get; set; }
        public string Id_estado { get; set; }
        public string Comentario { get; set; }
        public string Comentario_resp { get; set; }
        public string Id_celular { get; set; }
        public string User_approved { get; set; }
        public string Image { get; set; }
        public string Copy_to { get; set; }
        public DateTime Fecha_sistema { get; set; }
    }
}
