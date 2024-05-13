using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class EstadoVentasCliente
    {
        [Key]
        public string ESTADO_VENTAS_CLIENTE { get; set; }
    }
}
