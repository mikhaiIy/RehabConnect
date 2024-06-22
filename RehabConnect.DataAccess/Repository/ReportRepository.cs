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
    public class ReportRepository : Repository<Report>, IReportRepository
    {
        private ApplicationDbContext _db;
        public ReportRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Report obj)
        {
            _db.Reports.Update(obj);
        }
    }
}
