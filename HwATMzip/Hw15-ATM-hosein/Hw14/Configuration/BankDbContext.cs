using Hw14.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hw14.Configuration
{
    public class BankDbContext : DbContext
    {
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transactiion> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CardConfig());
            modelBuilder.ApplyConfiguration(new TransactionConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                (@"Server=DESKTOP-78B19T2\SQLEXPRESS; Initial Catalog=HW_14; User Id=sa; Password=13771377; TrustServerCertificate=True;");
        }
    }
}
