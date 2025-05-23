using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class PurchaseReactivateRequest
    {
        public string Cia { get; set; }
        public string Nro_scop { get; set; }
        public string Usuario_reactiva { get; set; }
    }
}
