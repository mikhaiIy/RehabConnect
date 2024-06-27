using RehabConnect.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.DataAccess.Repository.IRepository
{
    public interface ITherapistRepository : IRepository<Therapist>
    {
        void Update(Therapist obj);
        IEnumerable<Therapist> GetAll(Expression<Func<Therapist, bool>> filter = null, string includeProperties = null);
    }
}
