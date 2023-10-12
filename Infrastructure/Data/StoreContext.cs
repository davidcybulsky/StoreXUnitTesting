using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        private Guid _id;

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public StoreContext() : base()
        {
            _id = Guid.NewGuid();
        }

        public StoreContext(Guid id)
        {
            _id = id;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(_id.ToString());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>(c =>
            {
                c.HasKey(c => c.Id);
            });

            modelBuilder.Entity<Order>(o =>
            {
                o.HasKey(o => o.Id);

                o.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId);
            });

            modelBuilder.Entity<Product>(p =>
            {
                p.HasKey(p => p.Id);
            });
        }
    }
}
