using Data.Entities;
using Data.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options)
            : base(options)
        {

        }
        public DbSet<DocPageImage> DocPageImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        public DbSet<CustomerBasket> CustomerBaskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Product>()
                .Property(p => p.Price).HasColumnType("decimal(18,2)");

            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.ProductBrand).WithMany()
            //    .HasForeignKey(p => p.ProductBrandId);
            //modelBuilder.Entity<Product>()
            //    .HasOne(p => p.ProductType).WithMany()
            //    .HasForeignKey(p => p.ProductTypeId);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.SubCategories)
                .WithOne(c => c.ParentCategory)
                .HasForeignKey(c => c.ParentId);

            modelBuilder.Entity<Order>()
                .OwnsOne(o => o.ShipToAddress, a =>
            {
                a.WithOwner();
            });

            modelBuilder.Entity<Order>()
                .Property(s => s.Status)
                .HasConversion(
                    o => o.ToString(),
                    o => (OrderStatus)Enum.Parse(typeof(OrderStatus), o)
                );

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .OwnsOne(i => i.ItemOrderd, io => { io.WithOwner(); });

            modelBuilder.Entity<OrderItem>()
                .Property(i => i.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<DeliveryMethod>()
                .Property(d => d.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
