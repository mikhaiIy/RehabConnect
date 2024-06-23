using RehabConnect.Models;

namespace RehabConnect.DataAccess.Repository.IRepository;

public interface IStudentProgramRepository : IRepository<StudentProgram>
{
    void Update(StudentProgram obj);
}