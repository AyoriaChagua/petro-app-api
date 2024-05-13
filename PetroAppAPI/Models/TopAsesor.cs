using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class TopAsesor
    {
        [Key]
        public string Id_asesor { get; set; }
        public string Asesor { get; set; }
        public decimal? Total_mes_2 { get; set; }
        public decimal? Total_mes_1 { get; set; }
        public decimal? Crecimiento { get; set; }
    }
}
