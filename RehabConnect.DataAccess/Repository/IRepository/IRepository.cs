using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        IEnumerable<T> report(Expression<Func<T, bool>> filter = null, string includeProperties = null);
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression, string? includeProperties = null);

        int Count();
        
        // Add Object
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}
