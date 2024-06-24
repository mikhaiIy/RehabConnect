using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RehabConnect.DataAccess.Repository
{
    public class ParentDetailRepository : Repository<ParentDetail>, IParentDetailRepository
    {
        private ApplicationDbContext _db;
        public ParentDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<ParentDetail> GetAll(Expression<Func<ParentDetail, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<ParentDetail> query = _db.ParentDetails;

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



        public void Update(ParentDetail obj)
        {
            _db.ParentDetails.Update(obj);
        }
    }
}

