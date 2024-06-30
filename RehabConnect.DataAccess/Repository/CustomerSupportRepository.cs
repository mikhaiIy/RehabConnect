using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RehabConnect.DataAccess.Repository
{
    public class CustomerSupportRepository : Repository<CustomerSupport>, ICustomerSupportRepository
    {
        private ApplicationDbContext _db;
        public CustomerSupportRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<CustomerSupport> GetAll(Expression<Func<CustomerSupport, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<CustomerSupport> query = _db.CustomerSupports;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.ToList();
        }

        public void Update(CustomerSupport obj)
        {
            _db.CustomerSupports.Update(obj);
        }
    }
}
