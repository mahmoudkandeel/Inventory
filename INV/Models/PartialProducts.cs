using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INV.Models
{
    [MetadataType(typeof(ValidationProduct))]
    public partial class Product
    {
    }

    public class ValidationProduct
    {
        public int pro_id { get; set; }

        public int brand_id { get; set; }

        public int sup_id { get; set; }

        [Required]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        public string Photo { get; set; }

        [Required]
        [DisplayName("Units In Stock")]
        public int UnitsInStock { get; set; }

        [Required]
        [DisplayName("Unite Price")]
        public int unitePrice { get; set; }

        //[DataType(DataType.Date)]
        [DisplayName("Expire Date")]
        public DateTime ExpireDate { get; set; } = DateTime.Now;

        //[DataType(DataType.Date)]
        [DisplayName("Entry Date")]
        public DateTime EntryDate { get; set; } = DateTime.Now;

        public string Notes { get; set; }

        //[DisplayName("Product Description")]
        public string Description { get; set; }
    }
}