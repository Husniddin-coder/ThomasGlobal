using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstraction
{
    public interface IAppDbContext
    {
        public  DbSet<Product> Products { get; set; }

        public  DbSet<Account> Accounts { get; set; }

        public  DbSet<Basket> Baskets { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Image> Images { get; set; }

        public DbSet<Accept> Accepts { get; set; }

        public DbSet<Story> Stories { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
