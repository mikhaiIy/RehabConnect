using RehabConnect.Models;
using System.Linq.Expressions;

namespace RehabConnect.DataAccess.Repository.IRepository;

public interface IProgramRepository : IRepository<Program>
{
    void Update(Program obj);
    IEnumerable<Program> GetAll(Expression<Func<Program, bool>> filter = null, string includeProperties = null);
}