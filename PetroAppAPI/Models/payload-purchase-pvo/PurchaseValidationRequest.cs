using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class PurchaseValidationRequest
    {
        public string Cia { get; set; }
        public string Nro_scop { get; set; }
        public DateTime Fecha_despacho { get; set; }
        public string Asesor_valida { get; set; }
    }
}
