using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTestIIS.Models
{
    public class PurchaseDiscountResponse
    {
        [Key]
        public int Id_descuento { get; set; }
        public double Factor_con_igv { get; set; }
        public double Factor_sin_igv { get; set; }
        public string Id_condicion_pago { get; set; }
        public string Descripcion_condicion_pago { get; set; }
        public string Id_almacen { get; set; }
        public string Descripcion_almacen { get; set; }
        public string Direccion_almacen { get; set; }
    }
}
