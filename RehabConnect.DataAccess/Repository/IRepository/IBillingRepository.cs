using RehabConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.DataAccess.Repository.IRepository
{
    public interface IBillingRepository : IRepository<Billing>
    {
        decimal Sum();
        void Update(Billing obj);
    }

}
