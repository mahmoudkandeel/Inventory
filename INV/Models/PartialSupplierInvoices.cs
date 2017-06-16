using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INV.Models
{
    [MetadataType(typeof(ValidationSupplierInvoice))]
    public partial class SupplierInvoice
    {
    }

    public class ValidationSupplierInvoice
    {
        public int Sinv_id { get; set; }

        public int sup_id { get; set; }

        public int pro_id { get; set; }

        public int emp_id { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateTime { get; set; }

        public int? UnitPrice { get; set; }

        public string Quantity { get; set; }

        public int? Discount { get; set; }
    }
}