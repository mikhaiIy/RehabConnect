using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.DataAccess.Repository
{
    public class CustomerSupportRepository : Repository<CustomerSupport>, ICustomerSupportRepository
    {
        private ApplicationDbContext _db;
        public CustomerSupportRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CustomerSupport obj)
        {
            _db.CustomerSupports.Update(obj);
        }
    }
}
