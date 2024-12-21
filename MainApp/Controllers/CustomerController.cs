using InvoiceTracker.Entities;
using JeeshantPatelAssignment3.Services;
using JeeshantPatelAssignment3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace JeeshantPatelAssignment3.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustService _custService;
        public CustomerController(ICustService cs)
        {
            _custService = cs;
        }

       // public IActionResult Index(char? group = null)
       // {
       //     // Retrieve customers grouped alphabetically
       //     var customers = _context.Customers
       //.OrderBy(c => c.Name)
       //.GroupBy(c => char.ToUpper(c.Name.FirstOrDefault()))
       //.ToList();



       //     ViewBag.CurrentGroup = group ?? 'A'; // Default to 'A' if group is not specified

       //     if (group != null)
       //     {
       //         customers = customers.Where(g => g.Key == ViewBag.CurrentGroup).ToList();
       //     }

       //     return View(customers);
       // }

        [HttpGet("/add-Customer/{range}")]
        //display the add customer form based on the provided range
        public IActionResult AddCustomerRequest(string range)
        {
            ViewBag.CurrentRange = range;
           // showinf the add customer view with a new Customer model
            return View("Add", new Customer());
        }
        [HttpPost("/Customer-details/{range}")]

        // hanle submision of the form 
        public IActionResult AddNewCustomer(Customer cust, string range)
        {
            ViewBag.CurrentRange = range;
            if (ModelState.IsValid)
            {
                _custService.AddCust(cust);

                return RedirectToAction("GetCustDetailRange", new { range });
            }
            else
            {
                return View("Add", cust);
            }
        }
        [HttpGet("/Customer/{id}/edit-customer/{range}")]
        public IActionResult EditCustomerById(int id, string range)
        {
            ViewBag.CurrentRange = range;
            Customer cust = _custService.GetCustById(id);
            return View("Edit", cust);
        }
        [HttpPost("/Customer/edit-customer/{id}/{range}")]
        public IActionResult EditCustomerRequestProcess(int id, Customer cust, string range)
        {
            ViewBag.CurrentRange = range;
            if (id != cust.CustomerId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _custService.UpdateCust(cust);

                return RedirectToAction("GetCustDetailRange", new { range });
            }
            else
            {
                return View("Edit", cust);
            }
        }
       // public IActionResult Index(char? group = null)
       // {
       //     // Retrieve customers grouped alphabetically
       //     var customers = _context.Customers
       //.OrderBy(c => c.Name)
       //.GroupBy(c => char.ToUpper(c.Name.FirstOrDefault()))
       //.ToList();



       //     ViewBag.CurrentGroup = group ?? 'A'; // Default to 'A' if group is not specified

       //     if (group != null)
       //     {
       //         customers = customers.Where(g => g.Key == ViewBag.CurrentGroup).ToList();
       //     }

       //     return View(customers);
       // }
        [HttpGet("/Customer-details/{range}")]
        public IActionResult GetCustDetailRange(string range)
        {
            ViewBag.CurrentRange = range;
            List<Customer> getCustomers = _custService.GetCustByRange(_custService.GetActiveCusts(), range);

            return View("CustomerDetails", getCustomers);
        }
        [HttpGet("/Customer/delete/{id}/{range}")]
        public IActionResult SoftDeleteCustomerRequestProcess(int id, string range)
        {
            Customer customer = _custService.GetCustById(id);

            _custService.SoftDeleteCust(id);
            TempData["DeletedCustomerId"] = id;
            ViewBag.deletedCustId = id;
            TempData["UndoMessage"] = $"The customer '{customer.CustomerName}' was deleted. <a href='/Customer/undo-delete/{range}' class='text-danger'>Undo</a> this delete.";

            return RedirectToAction("GetCustDetailRange", new { range });
        }
        [HttpGet("/Customer/undo-delete/{range}")]
        public IActionResult UndoDeleteCustomer(string range)
        {
            if (TempData["DeletedCustomerId"] != null)
            {
                int deletedCustomerId = (int)TempData["DeletedCustomerId"];

                _custService.UndoSoftDeleteCust(deletedCustomerId);
                TempData.Remove("DeletedCustomerId");
                TempData.Remove("UndoMessage");

            }

            return RedirectToAction("GetCustDetailRange", new { range });
        }
        [HttpPost("/Customer/deleted/{id}")]
        public IActionResult DeleteCustomerRequestProcess(int id)
        {
            Customer customer = _custService.GetCustById(id);
            _custService.DeleteCust(id);

            string range = "A-E";
            return RedirectToAction("GetCustDetailRange", new { range });
        }

        [HttpGet("/Customer/CustomerInvoices/{customerId}")]
        public IActionResult CustomerInvoices(int customerId, string range)
        {
            Customer customer = _custService.GetCustById(customerId);
            var invoices = _custService.GetInvoiceForCust(customerId);
            ViewBag.CurrentRange = range;
            var viewModel = new CInvoiceViewModel
            {
                Customer = customer,
                PaymentTerms = customer.Invoices.FirstOrDefault()?.PaymentTerms,
                Invoices = invoices.ToList()
            };
            return View("Invoice", viewModel);
        }
        [HttpPost("/Customer/add-invoice")]
        //public List<Customer> GetCustByRange(List<Customer> customers, string range)
        //{
        //    switch (range)
        //    {
        //        case "A-E":
        //            return customers.Where(c => c.CustomerName.StartsWith("A", "B", "C", "D", "E")).ToList();
        //        case "F-K":
        //            return customers.Where(c => c.CustomerName.StartsWith("F", "G", "H", "I", "J", "K")).ToList();
        //        case "L-R":
        //            return customers.Where(c => c.CustomerName.StartsWith("L", "M", "N", "O", "P", "Q", "R")).ToList();
        //        case "S-Z":
        //            return customers.Where(c => c.CustomerName.StartsWith("S", "T", "V", "W", "X", "Y", "Z")).ToList();
        //        default:
        //            throw new ArgumentException("Invalid range was given.");

        //    }
        //}
        public IActionResult AddInvoice(int customerId, Invoice invoice, int paymentTerms, DateTime date, string range)
        {
            ViewBag.CurrentRange = range;
            Customer customer = _custService.GetCustById(customerId);
            invoice.PaymentTermsId = paymentTerms;
            invoice.InvoiceDate = date;
            invoice.Customer = customer;

            _custService.AddInvoice(invoice);

            return RedirectToAction("CustomerInvoices", new { customerId, range });
        }
       // public IActionResult Index(char? group = null)
       // {
       //     // Retrieve customers grouped alphabetically
       //     var customers = _context.Customers
       //.OrderBy(c => c.Name)
       //.GroupBy(c => char.ToUpper(c.Name.FirstOrDefault()))
       //.ToList();



       //     ViewBag.CurrentGroup = group ?? 'A'; // Default to 'A' if group is not specified

       //     if (group != null)
       //     {
       //         customers = customers.Where(g => g.Key == ViewBag.CurrentGroup).ToList();
       //     }

       //     return View(customers);
       // }
        [HttpGet("/Customer/CustomerInvoices/{customerId}/Line-items/{range}")]
        public IActionResult InvoiceDetails(int customerId, int id, string range)
        {
            var customer = _custService.GetCustInvoiceDetails(customerId);

            ViewBag.CurrentRange = range;
            var ViewModel = new CInvoiceViewModel
            {
                Customer = customer,
                Invoices = customer.Invoices.ToList(),
                selectedInvoice = customer.Invoices.FirstOrDefault(i => i.InvoiceId == id)
            };
            return View("Invoice", ViewModel);
        }
        //[HttpPost("/Customer/CustomerInvoices/{customerId}/{range}/add-item")]
        //public IActionResult AddLineItem(int customerId, int invoiceId, string description, double amount, string range)
        //{
        //    var invoice = _custService.GetInvoiceForCust(customerId).FirstOrDefault(i => i.InvoiceId == invoiceId);

        //    if (invoice != null)
        //    {
        //        var newItem = new InvoiceLineItem
        //        {
        //            Description = description,
        //            InvoiceId = invoiceId,
        //            Amount = amount,
        //        };
        //        _custService.AddInvoicesLineItem(newItem);
        //    }
        //    return RedirectToAction("InvoiceDetails", new { customerId = customerId, id = invoiceId, range = range });
        //}
        [HttpPost("/Customer/CustomerInvoices/{customerId}/{range}/add-item")]
        public IActionResult AddLineItem(int customerId, int invoiceId, string description, double amount, string range)
        {
            var invoice = _custService.GetInvoiceForCust(customerId).FirstOrDefault(i => i.InvoiceId == invoiceId);

            if (invoice != null)
            {
                var newItem = new InvoiceLineItem
                {
                    Description = description,
                    InvoiceId = invoiceId,
                    Amount = amount,
                };
                _custService.AddInvoicesLineItem(newItem);
            }
            return RedirectToAction("InvoiceDetails", new { customerId = customerId, id = invoiceId, range = range });
        }

    }
}
