using IndividuelltProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndividuelltProjekt.Data
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public OrderContext()
        {
        }
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conn = ConfigurationManager.ConnectionStrings["LibraryDatabaseConnectionString"]?.ConnectionString;
                if (string.IsNullOrWhiteSpace(conn))
                    throw new InvalidOperationException("Connection string 'LibraryDatabaseConnectionString' not found in App.config.");

                optionsBuilder.UseSqlServer(conn);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ensure EF maps the entity to the existing table named "User"
            modelBuilder.Entity<Order>().ToTable("Order");
        }
    }
}
