using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceTracker.Entities
{
    public class PaymentTerms
    {
        public int PaymentTermsId { get; set; }
        public string Description { get; set; } = null!;
        public double DueDays { get; set; }

        public List<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
