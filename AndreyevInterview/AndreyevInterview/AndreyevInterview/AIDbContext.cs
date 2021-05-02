using Microsoft.EntityFrameworkCore;

namespace AndreyevInterview
{
    public class AIDbContext : DbContext
    {
        public AIDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<LineItem> LineItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Invoice
            modelBuilder.Entity<Invoice>().Property(b => b.Description).IsRequired();

            // Line item
            modelBuilder.Entity<LineItem>().Property(b => b.InvoiceId).IsRequired();
            modelBuilder.Entity<LineItem>().Property(b => b.Quantity).IsRequired();
            modelBuilder.Entity<LineItem>().Property(b => b.Cost).IsRequired();
        }
    }

    public class Invoice
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalBillableValue { get; set; }
        public decimal TotalNumberLineItems { get; set; }
    }

    public class LineItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; }

        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        // Bill Status; 1 for Billable, 2 for Non-billable
        public int Bill { get; set; }

        public Invoice Invoice { get; set; }
    }
}