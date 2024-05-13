using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Ventas_Det_Resumen
    {
        [Key]
        public string Id_planta { get; set; }
        public string Descripcion { get; set; }
        public decimal? Cont_terce { get; set; }
        public decimal? Cred_Propi { get; set; }
        public decimal? Total { get; set; }
    }
}
