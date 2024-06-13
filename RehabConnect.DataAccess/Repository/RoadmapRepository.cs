using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.DataAccess.Repository
{
    public class RoadmapRepository :Repository<Roadmap>, IRoadmapRepository
    {
        private ApplicationDbContext _db;
        public RoadmapRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Roadmap obj)
        {
            _db.Roadmap.Update(obj);
        }
    }
}