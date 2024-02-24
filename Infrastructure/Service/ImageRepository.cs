using Application.Abstraction;
using Application.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Service;

public class ImageRepository : IImageRepository
{
    private readonly IAppDbContext _dbcontext;

    public ImageRepository(IAppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Image> CreateAsync(Image entity)
    {
        _dbcontext.Images.Attach(entity);

        return await Save() ? entity : null;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Image image = await _dbcontext.Images.FindAsync(id);
        if (image == null) return false;

        _dbcontext.Images.Remove(image);

        return await Save();
    }

    public async Task<IQueryable<Image>> GetAllAsync(Expression<Func<Image, bool>> expression)
    {
        return await Task.FromResult(_dbcontext.Images.Where(expression));
    }

    public async Task<Image> GetAsync(Guid id)
    {
        Image image = await _dbcontext.Images.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (image == null) return null;

        return await Task.FromResult(image);
    }


    public async Task<Image> UpdateAsync(Image entity)
    {
        _dbcontext.Images.Update(entity);

        return await Save() ? entity : null;
    }

    public async Task<bool> Save()
    {
        int result = await _dbcontext.SaveChangesAsync();

        return await Task.FromResult(result > 0 ? true : false);
    }
}
