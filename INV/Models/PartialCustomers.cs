using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INV.Models
{
    [MetadataType(typeof(ValidationCustomer))]
    public partial class Customer
    {
    }

    public class ValidationCustomer
    {
        [DisplayName("Customer ID")]
        public int cust_id { get; set; }

        [Required]
        [DisplayName("Customer Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [MinLength(9, ErrorMessage = "Phone number must be at least 9 digits")]
        public string Phone { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PostalCode)]
        public string Zipcode { get; set; }

        public string Photo { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Currency)]
        [DefaultValue(0)]
        public int? Balance { get; set; }

        public string Notes { get; set; }
    }
}