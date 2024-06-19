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

namespace RehabConnect.DataAccess.Repository
{
    public class TherapistRepository : Repository<Therapist>, ITherapistRepository
    {
        private ApplicationDbContext _db;
        public TherapistRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Therapist obj)
        {
            _db.Therapists.Update(obj);
        }
    }
}
