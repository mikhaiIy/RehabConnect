using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;

namespace RehabConnect.DataAccess.Repository;

public class StudentProgramRepository : Repository<StudentProgram>, IStudentProgramRepository

{
    private ApplicationDbContext _db;
    public StudentProgramRepository(ApplicationDbContext db) : base(db) 
    {
        _db = db;
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public void Update(StudentProgram obj)
    {
        _db.StudentPrograms.Update(obj);
    }
}