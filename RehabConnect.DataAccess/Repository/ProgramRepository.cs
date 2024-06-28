using Microsoft.EntityFrameworkCore;
using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using System.Linq.Expressions;

namespace RehabConnect.DataAccess.Repository
{
    public class ProgramRepository :Repository<Program>, IProgramRepository
    {
        private ApplicationDbContext _db;
        public ProgramRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public IEnumerable<Program> GetAll(Expression<Func<Program, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Program> query = _db.Programs;

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

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Program obj)
        {
            _db.Programs.Update(obj);
        }
    }
}

