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
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public IStepRepository Step { get; private set; }
        public IRoadmapRepository Roadmap { get; private set; }
        public IProgramRepository Program { get; private set; }
        public ITherapistRepository Therapist { get; private set; }
        public ICustomerSupportRepository CustomerSupport { get; private set; }
        public IParentDetailRepository ParentDetail { get; private set; }
        public IStudentRepository Student { get; private set; }
        public IScheduleRepository Schedule { get; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Step = new StepRepository(_db);
            Roadmap = new RoadmapRepository(_db);
            Program = new ProgramRepository(_db);
            Therapist = new TherapistRepository(_db);
            CustomerSupport = new CustomerSupportRepository(_db);
            ParentDetail = new ParentDetailRepository(_db);
            Student = new StudentRepository(_db);
            Schedule = new ScheduleRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
