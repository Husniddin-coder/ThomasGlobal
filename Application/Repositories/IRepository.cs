using System.Linq.Expressions;

namespace Application.Repositories
{
    public interface IRepository<T>
    {
        public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression);

        public Task<T> GetAsync(Guid id);

        public Task<T> CreateAsync(T entity);

        public Task<T> UpdateAsync(T entity);

        public Task<bool> DeleteAsync(Guid id);

        public Task<bool> Save();
    }
}
