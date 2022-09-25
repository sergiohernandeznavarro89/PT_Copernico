using Database.BD.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.BD.Context
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> data) : base(data) { }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
    }
}
