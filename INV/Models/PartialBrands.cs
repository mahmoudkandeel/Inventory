using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INV.Models
{
    [MetadataType(typeof(ValidationBrand))]
    public partial class Brand
    {
    }

    public class ValidationBrand
    {
        public int? brand_id { get; set; }

        public int? cat_id { get; set; }

        [DisplayName("Brand Description")]
        [MaxLength(150)]
        public string Description { get; set; }

        [DisplayName("Brand Name")]
        [MaxLength(150)]
        public string Name { get; set; }
    }
}