using InvoiceTracker.Entities;

namespace JeeshantPatelAssignment3.Models
{
    public class CInvoiceViewModel
    {
        public Customer? Customer { get; set; }
        public List<Invoice> Invoices { get; set; }
        public PaymentTerms PaymentTerms { get; set; }
        public Invoice selectedInvoice { get; set; }
        public int? DeletedCustomerId { get; set; }
    }
}
