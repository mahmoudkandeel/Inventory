using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INV.Models
{
    [MetadataType(typeof(ValidationSupplier))]
    public partial class Supplier
    {
    }

    public class ValidationSupplier
    {
        public int sup_id { get; set; }

        [Required]
        [DisplayName("Supplier Name")]
        public string Name { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MinLength(9, ErrorMessage = "Phone number must be at least 9 digits")]
        public string Phone { get; set; }

        public string Photo { get; set; }

        [DataType(DataType.PostalCode)]
        public int? Zipcode { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Notes { get; set; }
    }
}