using Application.Abstraction;
using Application.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Service;

public class ProductRepository : IProductRepository
{
    private readonly IAppDbContext _dbcontext;

    public ProductRepository(IAppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Product> CreateAsync(Product entity)
    {
        _dbcontext.Products.Attach(entity);

        return await Save() ? entity : null;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Product product = await _dbcontext.Products.FindAsync(id);
        if (product == null) return false;

        _dbcontext.Products.Remove(product);

        return await Save();
    }

    public async Task<IQueryable<Product>> GetAllAsync(Expression<Func<Product, bool>> expression)
    {
        return await Task.FromResult(_dbcontext.Products.Where(expression));
    }

    public async Task<Product> GetAsync(Guid id)
    {
        Product product = await _dbcontext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (product == null) return null;

        return await Task.FromResult(product);
    }


    public async Task<Product> UpdateAsync(Product entity)
    {
        _dbcontext.Products.Update(entity);

        return await Save() ? entity : null;
    }

    public async Task<bool> Save()
    {
        int result = await _dbcontext.SaveChangesAsync();

        return await Task.FromResult(result > 0 ? true : false);
    }
}
