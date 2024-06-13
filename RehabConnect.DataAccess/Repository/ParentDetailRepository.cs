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

        public void Update(ParentDetail obj)
        {
            _db.ParentDetails.Update(obj);
        }
    }
}

