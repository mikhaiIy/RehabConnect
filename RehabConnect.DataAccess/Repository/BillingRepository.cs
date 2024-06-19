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
    public class BillingRepository : Repository<Billing>, IBillingRepository
    {
        private ApplicationDbContext _db;
        public BillingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public decimal Sum()
        {
            return _db.Billings.Where(b => b.Status == "Paid").Sum(b => b.Amount);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Billing obj)
        {
            _db.Billings.Update(obj);
        }
    }
}
