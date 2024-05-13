using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class G_Client_for_Debt
    {
        public decimal Limite_credito { get; set; }
        public decimal Saldo { get; set; }
        public string Asesor { get; set; }
        public string Asesor_cell { get; set; }
        public string Asesor_email { get; set; }
    }
}
