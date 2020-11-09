using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetWhereInclude(Expression<Func<T, bool>> filter,
            string includeProperties);
        Task<T> Get(Expression<Func<T, bool>> where);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> ListAllAsync();

        Task<List<T>> GetListWhereInclude(Expression<Func<T, bool>> filter,
            string includeProperties);
        Task<List<T>> GetList(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetList(
            Expression<Func<T, bool>> where = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        Task<int> CountAsync();

        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
        void Delete(object id);
    }
}
