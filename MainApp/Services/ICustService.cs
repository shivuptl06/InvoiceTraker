using InvoiceTracker.Entities;

namespace JeeshantPatelAssignment3.Services
{
    public interface ICustService
    {
       // List<Customer> GetCustByRange(List<Customer> customers, string range);
        void AddCust(Customer customer);
        List<Customer> GetCust();
        Customer GetCustById(int customerId);
    
        void UpdateCust(Customer customer);
        void DeleteCust(int customerId);
  
        void AddInvoice(Invoice invoice);
        IEnumerable<PaymentTerms> GetPaymentTerms();
        void AddPaymentTerms(PaymentTerms paymentTerms);
        IEnumerable<InvoiceLineItem> GetInvoiceLineItems(int invoiceId);
        void AddInvoicesLineItem(InvoiceLineItem invoiceLineItem);
       List<Customer> GetCustByRange(List<Customer> customers, string range);
        void SoftDeleteCust(int customerId); 
        void UndoSoftDeleteCust(int deletedCustomerId);
        List<Customer> GetActiveCusts();
      //  IEnumerable<Invoice> GetInvoiceForCust(int customerId);
        Customer GetCustInvoiceDetails(int customerId);
        IEnumerable<Invoice> GetInvoiceForCust(int customerId);
    }
}
