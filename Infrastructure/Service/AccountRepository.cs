using Application.Abstraction;
using Application.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Service;

public class AccountRepository : IAccountRepository
{
    private readonly IAppDbContext _dbcontext;

    public AccountRepository(IAppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Account> CreateAsync(Account entity)
    {
        _dbcontext.Accounts.Attach(entity);

        return await Save() ? entity : null;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Account account = await _dbcontext.Accounts.FindAsync(id);
        if (account == null) return false;

        _dbcontext.Accounts.Remove(account);

        return await Save();
    }

    public async Task<IQueryable<Account>> GetAllAsync(Expression<Func<Account, bool>> expression)
    {
        return await Task.FromResult(_dbcontext.Accounts.Where(expression));
    }

    public async Task<Account> GetAsync(Guid id)
    {
        Account account = await _dbcontext.Accounts.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (account == null) return null;

        return await Task.FromResult(account);
    }


    public async Task<Account> UpdateAsync(Account entity)
    {
        Account? account = _dbcontext.Accounts.Where(x=> x.Id == entity.Id)
            .Include(x=> x.Image).FirstOrDefault();
        if (account == null) return null;

        account.Account_image = entity.Account_image;
        account.Image.FilePath = entity.Image.FilePath;

        return await Save() ? entity : null;
    }

    public async Task<bool> Save()
    {
        int result = await _dbcontext.SaveChangesAsync();

        return await Task.FromResult(result > 0 ? true : false);
    }
}
