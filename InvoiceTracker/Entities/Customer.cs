using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTracker.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Customer Name is Required.")]
        public string CustomerName { get; set; } = null!;
        [Required(ErrorMessage = "Address is Required.")]
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        [Required(ErrorMessage = "City is Required.")]
        public string? City { get; set; } = null!;
        [Required(ErrorMessage = "Province/State is Required.")]
        [StringLength(2, ErrorMessage = "Province/State must be a 2-letter code.")]
        public string? ProvinceOrState { get; set; } = null!;
        [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?|[A-Za-z]\d[A-Za-z] \d[A-Za-z]\d$", ErrorMessage = "Invalid Postal/Zip code must be in US or Canada zip/postal code format.")]
        [Required(ErrorMessage = "Zip/Postal is Required.")]
        public string? ZipOrPostalCode { get; set; } = null!;
        [Required(ErrorMessage = "Phone Number is Required.")]
        [RegularExpression(@"^(\+?1[s-]?)?([2-9][0-8][0-9])[s-]?([2-9][0-9]{2})[s-]?([0-9]{4})", ErrorMessage = "Phone number must be in Canada or Us phone number format.")]
        public string? Phone { get; set; }
        public string? ContactLastName { get; set; }
        public string? ContactFirstName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string? ContactEmail { get; set; }
        public bool IsDeleted { get; set; } = false;

        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
