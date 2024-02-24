using Application.Repositories;
using Domain.Models;
using System.Linq.Expressions;

namespace Infrastructure.Service
{
    public class AcceptRepository : IAcceptRepository
    {
        public Task<Accept> CreateAsync(Accept entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Accept>> GetAllAsync(Expression<Func<Accept, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<Accept> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save()
        {
            throw new NotImplementedException();
        }

        public Task<Accept> UpdateAsync(Accept entity)
        {
            throw new NotImplementedException();
        }
    }
}
