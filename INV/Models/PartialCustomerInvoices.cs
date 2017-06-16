using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INV.Models
{
    [MetadataType(typeof(ValidationCustomerInvoice))]
    public partial class CustomerInvoice
    {
    }

    public class ValidationCustomerInvoice
    {
        public int Cinv_id { get; set; }

        public int ord_id { get; set; }

        public int pro_id { get; set; }

        public int cust_id { get; set; }

        public int emp_id { get; set; }

        [DisplayName("Order DateTime")]
        [DataType(DataType.Date)]
        public DateTime? DateTime { get; set; }

        [DataType(DataType.Currency)]
        [Required]
        public int? UnitPrice { get; set; }

        [Required]
        public string Quantity { get; set; }

        public int? Discount { get; set; }
    }
}