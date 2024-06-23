using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehabConnect.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        //User Management
        ITherapistRepository Therapist { get; }
        ICustomerSupportRepository CustomerSupport { get; }
        IParentDetailRepository ParentDetail { get; }
        IStudentRepository Student { get; }
        
        IStudentProgramRepository StudentProgram { get; }

        //Program
        IStepRepository Step { get; }
        IRoadmapRepository Roadmap { get; }
        IProgramRepository Program { get; }
        IScheduleRepository Schedule { get; }

        //Billing
        IBillingRepository Billing { get; }
        IInvoiceRepository Invoice { get; }
        IShoppingCartRepository ShoppingCart { get; }

        //Report
        IReportRepository Report { get; }
        void Save();
    }
}
