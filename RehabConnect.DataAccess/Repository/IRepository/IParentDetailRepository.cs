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
        IEnumerable<ParentDetail> GetAll(Expression<Func<ParentDetail, bool>> filter = null);
        void Update(ParentDetail obj);
    }
}
