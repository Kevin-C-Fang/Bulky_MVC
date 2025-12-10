using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    // T represents a generic stub that references any model/class.
    public interface IRepository<T> where T : class
    {
        // T - Category
        // Basic functionality that any model/table would need.
        IEnumerable<T> GetAll(string? includeProperties = null);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
