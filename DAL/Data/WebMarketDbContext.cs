using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class WebMarketDbContext : DbContext
    {
        public WebMarketDbContext(DbContextOptions<WebMarketDbContext> options) : base(options)
        { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasMany(customer => customer.Orders)
                .WithOne(order => order.Customer);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(orderDetail => orderDetail.Order)
                .WithMany(order => order.OrderDetails)
                .HasForeignKey(orderDetail => orderDetail.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(orderDetail => orderDetail.Product)
                .WithMany(product => product.OrderDetails)
                .HasForeignKey(orderDetail => orderDetail.ProductId);

            modelBuilder.Entity<OrderDetail>()
                .HasKey(orderDetail => orderDetail.Id);

            modelBuilder.Entity<OrderDetail>()
                .HasAlternateKey(orderDetail => new { orderDetail.ProductId, orderDetail.OrderId });

            modelBuilder.Entity<OrderDetail>()
                .HasAlternateKey(orderDetail => orderDetail.Id);

            modelBuilder.Entity<Product>()
                .HasOne(product => product.Category)
                .WithMany(productCategory => productCategory.Products)
                .HasForeignKey(product => product.ProductCategoryId);

            modelBuilder.Entity<Person>()
                .HasKey(person => person.Id);
        }
    }
}
