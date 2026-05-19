using E_CommerceSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_CommerceSystem
{
    public class ApplicationDbContext : DbContext 
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            //connection to database
            options.UseSqlServer(" Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=E-CommerceSystem; Integrated Security=true; TrustServerCertificate=True ");
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
