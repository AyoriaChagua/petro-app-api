using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class CondiPago
    {
        [Key]
        public string Id_Condicion_Pago { get; set; }
        public string Descripcion { get; set; }
    }
}
