using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RehabConnect.DataAccess.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Student obj)
        {
            _db.Students.Update(obj);
        }

        public IEnumerable<Student> GetAll(Expression<Func<Student, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<Student> query = _db.Students;

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
    }
}
