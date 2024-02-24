using Application.Abstraction;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext , IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {}

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Account> Accounts { get; set; }

        public virtual DbSet<Basket> Baskets { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<Accept> Accepts { get; set; }

        public virtual DbSet<Story> Stories { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>().HasIndex(x=> x.FilePath).IsUnique();
        }
    }
}
