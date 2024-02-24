using Application.Abstraction;
using Application.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Service;

public class BasketRepository : IBasketRepository
{
    private readonly IAppDbContext _dbcontext;

    public BasketRepository(IAppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Basket> CreateAsync(Basket entity)
    {
        _dbcontext.Baskets.Attach(entity);

        return await Save() ? entity : null;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Basket basket = await _dbcontext.Baskets.FindAsync(id);
        if (basket == null) return false;

        _dbcontext.Baskets.Remove(basket);

        return await Save();
    }

    public async Task<IQueryable<Basket>> GetAllAsync(Expression<Func<Basket, bool>> expression)
    {
        return await Task.FromResult(_dbcontext.Baskets.Where(expression));
    }

    public async Task<Basket> GetAsync(Guid id)
    {
        Basket basket = await _dbcontext.Baskets.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (basket == null) return null;

        return await Task.FromResult(basket);
    }


    public async Task<Basket> UpdateAsync(Basket entity)
    {
        _dbcontext.Baskets.Update(entity);

        return await Save() ? entity : null;
    }

    public async Task<bool> Save()
    {
        int result = await _dbcontext.SaveChangesAsync();

        return await Task.FromResult(result > 0 ? true : false);
    }
}
