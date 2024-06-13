using RehabConnect.Models;

namespace RehabConnect.DataAccess.Repository.IRepository;

public interface IProgramRepository : IRepository<Program>
{
    void Update(Program obj);
}