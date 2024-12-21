using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTracker.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? InvoiceDueDate
        {
            get
            {
                return InvoiceDate?.AddDays(Convert.ToDouble(PaymentTerms?.DueDays));
            }
        }
        public int PaymentTermsId { get; set; }
        public PaymentTerms? PaymentTerms { get; set; }
        public double? PaymentTotal { get; set; } = 0.0;
        public DateTime? PaymentDate { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public List<InvoiceLineItem> LineItems { get; set; } = new List<InvoiceLineItem>();
    }
}
