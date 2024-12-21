using InvoiceTracker.Entities;
using Microsoft.EntityFrameworkCore;

namespace JeeshantPatelAssignment3.Services
{
    public class CustService: ICustService
    {
        private readonly AppDataDbContext context;

        public CustService(AppDataDbContext ctx)
        {
            this.context = ctx;
        }
        //public void DeleteCust(int custId)
        //{
        //    var cst = context.Customers.Find(custId);
        //    if (cst != null)
        //    {
        //        context.Customers.Remove(cst);
        //        context.SaveChanges();
        //    }
        //}

        public Customer GetCustById(int customerId)
        {
            return context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

     
        public Customer GetCustInvoiceDetails(int customerId)
        {
            return context.Customers.Include(c => c.Invoices).ThenInclude(i => i.PaymentTerms).Include(c => c.Invoices).ThenInclude(i => i.LineItems).FirstOrDefault(c => c.CustomerId == customerId);
        }
        public void AddCust(Customer cust)
        {
            context.Customers.Add(cust);
            context.SaveChanges();
        }
        //public void DeleteCust(int custId)
        //{
        //    var cst = context.Customers.Find(custId);
        //    if (cst != null)
        //    {
        //        context.Customers.(cst);
        //        context.SaveChanges();
        //    }
        //}
        public void UpdateCust(Customer cust)
        {
            context.Customers.Update(cust);
            context.SaveChanges();
        }
        public void DeleteCust(int custId)
        {
            var cst = context.Customers.Find(custId);
            if (cst != null)
            {
                context.Customers.Remove(cst);
                context.SaveChanges();
            }
        }
        public IEnumerable<Invoice> GetInvoiceForCust(int customerId)
        {
            return context.Invoices.Where(i => i.CustomerId == customerId).ToList();
        }
        public void AddPaymentTerms(PaymentTerms paymentTerm)
        {
            context.PaymentTerms.Add(paymentTerm);
            context.SaveChanges();
        }
   
        public IEnumerable<PaymentTerms> GetPaymentTerms()
        {
            return context.PaymentTerms.ToList();
        }
        public void AddInvoice(Invoice invoice)
        {
            context.Invoices.Add(invoice);
            context.SaveChanges();
        }
        public IEnumerable<InvoiceLineItem> GetInvoiceLineItems(int invoiceId)
        {
            return context.InvoiceLineItem.Where(item => item.InvoiceId == invoiceId).ToList();
        }
        public void AddInvoicesLineItem(InvoiceLineItem invoiceLineItem)
        {
            context.InvoiceLineItem.Add(invoiceLineItem);
            context.SaveChanges();
        }
        public List<Customer> GetCustByRange(List<Customer> customers, string range)
        {
            switch (range)
            {
                case "A-E":
                    return customers.Where(c => c.CustomerName.StartsWith("A", "B", "C", "D", "E")).ToList();
                case "F-K":
                    return customers.Where(c => c.CustomerName.StartsWith("F", "G", "H", "I", "J", "K")).ToList();
                case "L-R":
                    return customers.Where(c => c.CustomerName.StartsWith("L", "M", "N", "O", "P", "Q", "R")).ToList();
                case "S-Z":
                    return customers.Where(c => c.CustomerName.StartsWith("S", "T", "V", "W", "X", "Y", "Z")).ToList();
                default:
                    throw new ArgumentException("Invalid range was given.");

            }
        }
        public void UndoSoftDeleteCust(int deletedCustomerId)
        {
            var cst = context.Customers.Find(deletedCustomerId);
            if (cst != null)
            {
                cst.IsDeleted = false;
                context.SaveChanges();
            }
        }
        public void SoftDeleteCust(int customerId)
        {

            var cst = context.Customers.Find(customerId);
            if (cst != null)
            {
                cst.IsDeleted = true;
                context.SaveChanges();
            }
        }
        //public void AddInvoicesLineItem(InvoiceLineItem invoiceLineItem)
        //{
        //    ctx.InvoiceLineItem.Add(invoiceLineItem);
        //    context.SaveChanges();
        //}

        public List<Customer> GetCust()
        {
            return context.Customers.ToList();
        }

        public List<Customer> GetActiveCusts()
        {
            return context.Customers.Where(c => !c.IsDeleted).ToList();
        }
    }
}
