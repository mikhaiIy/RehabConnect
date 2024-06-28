using RehabConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.DataAccess.Repository.IRepository
{
    public interface ICustomerSupportRepository : IRepository<CustomerSupport>
    {
        void Update(CustomerSupport obj);
        IEnumerable<CustomerSupport> GetAll(Expression<Func<CustomerSupport, bool>> filter = null, string includeProperties = null);
    }
}
