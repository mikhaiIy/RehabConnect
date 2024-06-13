using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;

namespace RehabConnect.DataAccess.Repository
{
    public class ProgramRepository :Repository<Program>, IProgramRepository
    {
        private ApplicationDbContext _db;
        public ProgramRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
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

