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
    public class LoanContext : DbContext
    {
        public DbSet<Loan> Loans { get; set; }
        public DbSet<User> Users { get; set; } 
        public DbSet<Book> Books { get; set; } 
        public LoanContext()
        {
        }
        public LoanContext(DbContextOptions<LoanContext> options) : base(options)
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
            // Mappa entiteter/modeller till tabeller
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Loan>().ToTable("Loan"); 

            // Relation: Loan -> User
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.User)       //Ett lån har en användare
                .WithMany(u => u.Loans)    //En användare kan ha många lån
                .HasForeignKey(l => l.User_Id); //Foreign key i Loan-tabellen från user tabellen

            // Relation: Loan -> Book
            modelBuilder.Entity<Loan>()
                .HasOne(l => l.Book)        // Ett lån har en bok
                .WithMany(b => b.Loans)     // En bok kan ha haft många lån
                .HasForeignKey(l => l.Book_Id);   // Foreign key i Loan-tabellen från book tabellen
        }
    }
}
