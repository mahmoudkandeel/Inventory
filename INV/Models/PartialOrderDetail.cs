using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INV.Models
{
    [MetadataType(typeof(ValidationOrderDetail))]
    public partial class PartialOrderDetail
    {
    }

    class ValidationOrderDetail
    {
        public int ord_id { get; set; }
        public int pro_id { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? Quantity { get; set; }
        public int? Discount { get; set; }
    }
}