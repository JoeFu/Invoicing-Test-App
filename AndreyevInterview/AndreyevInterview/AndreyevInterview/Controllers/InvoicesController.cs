using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AndreyevInterview.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoicesController : ControllerBase
    {
        private readonly AIDbContext _context;

        public InvoicesController(AIDbContext context)
        {
            _context = context;
        }

        // Invoice List

        [HttpGet]
        public IEnumerable<Invoice> GetInvoices()
        {
            return _context.Invoices.ToList();
        }

        // Create Invoice

        [HttpPut]
        public Invoice CreateInvoice(InvoiceInput input)
        {
            var invoice = new Invoice();
            invoice.Description = input.Description;
            _context.Add(invoice);
            _context.SaveChanges();
            return invoice;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Invoice(int id, InvoiceInput input)
        {
            var invoice = await _context.Invoices.FindAsync(id);
            if(invoice ==null)
            {

                return NotFound();
            }

            invoice.TotalBillableValue = input.TotalBillableValue;
            invoice.TotalNumberLineItems = input.TotalNumberLineItems;
            invoice.TotalValue = input.TotalValue;

            _context.Invoices.Update(invoice);
            
            await _context.SaveChangesAsync();
            return NoContent();

        }



        // Item List
        [HttpGet("{id}")]
        public IEnumerable<LineItem> GetInvoiceLineItems(int id)
        {
            return _context.LineItems.Where(x => x.InvoiceId == id).ToList();
        }


        // Delete the item in the Invoice
        [HttpDelete("{id}")]
        public async Task<ActionResult<LineItem>> DeleteInvoiceLineItem(int? id)
        {
            var item = await _context.LineItems.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.LineItems.Remove(item);


            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Add Item
        [HttpPost("{id}")]
        public LineItem AddLineItemToInvoice(int id, LineItemInput input)
        {
            var lineItem = new LineItem();
            lineItem.InvoiceId = id;
            lineItem.Description = input.Description;
            lineItem.Quantity = input.Quantity;
            lineItem.Cost = input.Cost;
            lineItem.Bill = input.Bill;
            _context.Add(lineItem);
            _context.SaveChanges();
            return lineItem;
        }
    }



    public class InvoiceInput
    {
        public string Description { get; set; }
        public int Id { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalBillableValue { get; set; }
        public decimal TotalNumberLineItems { get; set; }
    }

    //public class InvoiceUpdate
    //{
    //    public int Id { get; set; }
    //    public decimal TotalValue { get; set; }
    //    public decimal TotalBillableValue { get; set; }
    //    public decimal TotalNumberLineItems { get; set; }
    //}

    public class LineItemInput
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public int Bill { get; set; }
    }


}