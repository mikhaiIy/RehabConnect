using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class InvoiceController : Controller
  {
    private readonly ApplicationDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public InvoiceController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      var model = new DashboardViewModel();

      // Fetch dashboard data
      model.ParentsCount = _unitOfWork.ParentDetail.Count();
      model.InvoicesCount = _unitOfWork.Invoice.Count();
      model.TotalPaid = _unitOfWork.Billing.Sum();
      model.TotalUnpaid = _unitOfWork.Billing.Sum();

      

      return View(model);
    }

    public IActionResult Add()
    {
      return View();
    }

    public IActionResult Edit()
    {
      return View();
    }

    public IActionResult Preview()
    {
      return View();
    }

    public IActionResult Print()
    {
      return View();
    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetInvoices()
    {
      InvoiceBillingVM invoiceBillingVm = new InvoiceBillingVM()
      {
        Invoice = _unitOfWork.Invoice.GetAll(includeProperties: "ParentDetail").ToList(),
        Billing = _unitOfWork.Billing.GetAll(includeProperties: "Invoice").ToList()
      };
      return Json(new { data = invoiceBillingVm });
    }

    #endregion
  }

  public class DashboardViewModel
  {
    public int ParentsCount { get; set; }
    public int InvoicesCount { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal TotalUnpaid { get; set; }
  }
}


