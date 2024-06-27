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
    public class TherapistRepository : Repository<Therapist>, ITherapistRepository
    {
        private ApplicationDbContext _db;
        public TherapistRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Therapist> GetAll(Expression<Func<Therapist, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Therapist> query = _db.Therapists;

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

        public void Update(Therapist obj)
        {
            _db.Therapists.Update(obj);
        }
    }
}
