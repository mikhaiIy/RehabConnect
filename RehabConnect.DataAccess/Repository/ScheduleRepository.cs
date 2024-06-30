using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.DataAccess.Repository
{
    public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
    {
        private ApplicationDbContext _db;

        public ScheduleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }
        
        public void AddRange(IEnumerable<Schedule> schedules)
        {
            _db.Schedules.AddRange(schedules);
            _db.SaveChanges();
        }

        public void Update(Schedule obj)
        {
            throw new NotImplementedException();
        }
    }
}
