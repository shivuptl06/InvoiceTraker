using InvoiceTracker.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TescasesXunit
{
    public class UnitTest2
    {
        [Fact]
        public void Address1_Required()
        {
           
            var customer = new Customer();

            
            var result = ValidateModel(customer);

           
            Assert.Contains(result, v => v.MemberNames.Contains(nameof(Customer.Address1)));
            Assert.Contains(result, v => v.ErrorMessage == "Address is Required.");
        }

    

        private static System.Collections.Generic.List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new System.Collections.Generic.List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, validationContext, validationResults, true);
            return validationResults;
        }
    }
}
