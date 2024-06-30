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
    public class AnnouncementRepository : Repository<Announcement>, IAnnouncementRepository
    {
        private ApplicationDbContext _db;
        public AnnouncementRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Announcement obj)
        {
            var objFromDb = _db.Announcements.FirstOrDefault(u => u.AnnouncementID == obj.AnnouncementID);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }

            }
        }

    }
}
