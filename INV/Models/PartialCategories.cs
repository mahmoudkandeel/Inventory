using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INV.Models
{
    [MetadataType(typeof(ValidationCategory))]
    public partial class Category

    {
    }
    public class ValidationCategory
    {
        public int cat_id { get; set; }

        [DisplayName("Category Name")]
        public string Name { get; set; }

        [DisplayName("Category Description")]
        public string Description { get; set; }

        public string Notes { get; set; }
    }

}