using Microsoft.EntityFrameworkCore;
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
        public IShoppingCartRepository ShoppingCart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IInvoiceRepository Invoice { get; private set; }
        public IBillingRepository Billing { get; private set; }
        public IReportRepository Report { get; private set; }
        
        public ISessionRepository Session { get; private set; }
        public IStudentProgramRepository  StudentProgram { get; private set; }



        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Report = new ReportRepository(_db);
            Billing = new BillingRepository(_db);
            Invoice = new InvoiceRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            Step = new StepRepository(_db);
            Roadmap = new RoadmapRepository(_db);
            Program = new ProgramRepository(_db);
            Therapist = new TherapistRepository(_db);
            CustomerSupport = new CustomerSupportRepository(_db);
            ParentDetail = new ParentDetailRepository(_db);
            Student = new StudentRepository(_db);
            Schedule = new ScheduleRepository(_db);
            StudentProgram = new StudentProgramRepository(_db);

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
