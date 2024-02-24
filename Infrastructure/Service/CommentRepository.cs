using Application.Abstraction;
using Application.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Service;

public class CommentRepository : ICommentRepository
{
    private readonly IAppDbContext _dbcontext;

    public CommentRepository(IAppDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }

    public async Task<Comment> CreateAsync(Comment entity)
    {
        _dbcontext.Comments.Attach(entity);

        return await Save() ? entity : null;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        Comment comment = await _dbcontext.Comments.FindAsync(id);
        if (comment == null) return false;

        _dbcontext.Comments.Remove(comment);

        return await Save();
    }

    public async Task<IQueryable<Comment>> GetAllAsync(Expression<Func<Comment, bool>> expression)
    {
        return await Task.FromResult(_dbcontext.Comments.Where(expression));
    }

    public async Task<Comment> GetAsync(Guid id)
    {
        Comment comment = await _dbcontext.Comments.Where(x => x.Id == id).FirstOrDefaultAsync();

        if (comment == null) return null;

        return await Task.FromResult(comment);
    }


    public async Task<Comment> UpdateAsync(Comment entity)
    {
        _dbcontext.Comments.Update(entity);

        return await Save() ? entity : null;
    }

    public async Task<bool> Save()
    {
        int result = await _dbcontext.SaveChangesAsync();

        return await Task.FromResult(result > 0 ? true : false);
    }
}
