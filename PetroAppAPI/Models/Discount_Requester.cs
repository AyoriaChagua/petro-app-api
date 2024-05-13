using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class Discount_Requester
    {
        public string Id_Requester { get; set; }
        public string Requester { get; set; }
        public string Id_celular { get; set; }
    }
}
