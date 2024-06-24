using RehabConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.DataAccess.Repository.IRepository
{
    public interface IParentDetailRepository : IRepository<ParentDetail>
    {
        void Update(ParentDetail obj);
        IEnumerable<ParentDetail> GetAll(Expression<Func<ParentDetail, bool>> filter = null, string includeProperties = null);
    }
}
