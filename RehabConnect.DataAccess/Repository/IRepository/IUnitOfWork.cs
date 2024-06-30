using Microsoft.AspNetCore.Http;
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
        IApplicationUserRepository ApplicationUser { get; }
        IStudentProgramRepository StudentProgram { get; }

        //Program
        IStepRepository Step { get; }
        IRoadmapRepository Roadmap { get; }
        IProgramRepository Program { get; }
        IScheduleRepository Schedule { get; }

        //Billing
        IBillingRepository Billing { get; }
        IInvoiceRepository Invoice { get; }

        //Report
        IReportRepository Report { get; }

        ISessionRepository Session { get; }

        IInvoiceItemRepository InvoiceItem { get; }
        IAnnouncementRepository Announcement { get; }
        void Save();
    }
}
