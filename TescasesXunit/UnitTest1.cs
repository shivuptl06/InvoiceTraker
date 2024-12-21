using InvoiceTracker.Entities;
using System.ComponentModel.DataAnnotations;

namespace TescasesXunit
{
    public class UnitTest1
    {
        [Fact]
        public void CustomerName_Required()
        {
        
            var customer = new Customer();

           
            var result = ValidateModel(customer);

          
            Assert.Contains(result, v => v.MemberNames.Contains(nameof(Customer.CustomerName)));
            Assert.Contains(result, v => v.ErrorMessage == "Customer Name is Required.");
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