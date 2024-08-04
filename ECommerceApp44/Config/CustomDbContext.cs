using System.Collections.Generic;
using System.Reflection.Emit;
using ECommerceApp44.Model;
using Microsoft.EntityFrameworkCore;
using Model;
namespace ECommerceApp44.Config
{
    public class CustomDbContext : DbContext
    {
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
            .HasKey(i => i.ItemId);

            modelBuilder.Entity<Order>()
            .HasKey(i => i.OrderId);

            modelBuilder.Entity<CartItem>()
            .HasKey(i => i.CartItemId);

            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerId);

            modelBuilder.Entity<Item>()
                .Property(i => i.ItemId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Order>()
                .Property(i => i.OrderId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<CartItem>()
                .Property(i => i.CartItemId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Customer>()
                .Property(i => i.CustomerId)
                .ValueGeneratedOnAdd();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=127.0.0.1;Database=ecommerce_db;Username=postgres;Password=073092");
            }
        }
    }
}

