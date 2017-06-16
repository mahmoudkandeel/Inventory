using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INV.Models
{
    [MetadataType(typeof(ValidationOrder))]
    public partial class Order
    {
    }

    public class ValidationOrder
    {
        public int ord_id { get; set; }
        public int cust_id { get; set; }
        public string emp_id { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateTime { get; set; }

        [DisplayName("Order Description")]
        public string Description { get; set; }

        [DisplayName("Order #")]
        public string orderNo { get; set; }

    }
}