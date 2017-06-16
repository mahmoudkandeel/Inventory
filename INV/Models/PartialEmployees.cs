using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INV.Models
{
    [MetadataType(typeof(ValidationEmployee))]
    public partial class Employee
    {
    }

    public class ValidationEmployee
    {
        public int emp_id { get; set; }

        [Required]
        [DisplayName("Employee Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Range(1000, 10000)]
        public int? Salary { get; set; }

        public string Address { get; set; }

        public string Photo { get; set; }

        //[DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage =
            "Phone format should be like 0123456789, 012-345-6789, (012)-345-6789 etc.")]
        public string Phone { get; set; }

        public string Notes { get; set; }


        [DataType(DataType.Date)]
        public DateTime? HireDate { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Privilege { get; set; }

        public string Title { get; set; }
    }
}