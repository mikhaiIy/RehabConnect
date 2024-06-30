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
    public class InvoiceItemRepository : Repository<InvoiceItem>, IInvoiceItemRepository
    {
        private ApplicationDbContext _db;
        public InvoiceItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(InvoiceItem obj)
        {
            _db.InvoiceItems.Update(obj);
        }
    }
}
