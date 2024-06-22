using RehabConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RehabConnect.DataAccess.Repository.IRepository
{
    public interface IStudentRepository : IRepository<Student>
    {
        void Update(Student obj);
        IEnumerable<Student> GetAll(Expression<Func<Student, bool>> filter = null, string includeProperties = null);
    }
}
