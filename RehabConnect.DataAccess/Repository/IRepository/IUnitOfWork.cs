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

        //Program
        IStepRepository Step { get; }
        IRoadmapRepository Roadmap { get; }
        IProgramRepository Program { get; }

        //Billing
        IShoppingCartRepository ShoppingCart { get; }
        void Save();
    }
}
