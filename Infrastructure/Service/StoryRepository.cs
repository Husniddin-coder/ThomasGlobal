using Application.Abstraction;
using Application.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Service;

public class StoryRepository : IStoryRepository
{
    private readonly IAppDbContext _dbcontext;

    public StoryRepository(IAppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Story> CreateAsync(Story entity)
    {
        _dbcontext.Stories.Attach(entity);

        return await Save() ? entity : null;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Story story = await _dbcontext.Stories.FindAsync(id);
        if (story == null) return false;

        _dbcontext.Stories.Remove(story);

        return await Save();
    }

    public async Task<IQueryable<Story>> GetAllAsync(Expression<Func<Story, bool>> expression)
    {
        return await Task.FromResult(_dbcontext.Stories.Where(expression));
    }

    public async Task<Story> GetAsync(Guid id)
    {
        Story story = await _dbcontext.Stories.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (story == null) return null;

        return await Task.FromResult(story);
    }


    public async Task<Story> UpdateAsync(Story entity)
    {
        _dbcontext.Stories.Update(entity);

        return await Save() ? entity : null;
    }

    public async Task<bool> Save()
    {
        int result = await _dbcontext.SaveChangesAsync();

        return await Task.FromResult(result > 0 ? true : false);
    }
}
