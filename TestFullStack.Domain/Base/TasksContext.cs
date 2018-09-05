using Microsoft.EntityFrameworkCore;
using TestFullStack.Domain.Entities;

namespace TestFullStack.Domain.Base
{
    public class TestFullStackContext : DbContext
    {

        public TestFullStackContext(DbContextOptions<TestFullStackContext> options) : base(options)
        {

        }

        public virtual DbSet<ItemOrder> ItemOrder { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemOrder>().ToTable("ItemOrder");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<User>().ToTable("User");

            base.OnModelCreating(modelBuilder);
        }

    }
}
