using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using RehabConnect.Utility;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize(Roles = SD.Role_Admin)]
  public class InvoiceController : Controller
  {
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
      model.Invoices = _unitOfWork.Invoice.GetAll(includeProperties: "ParentDetail").ToList();
      model.parentDetails = _unitOfWork.ParentDetail.GetAll().ToList();

      return View(model);
    }

    public IActionResult Add()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Add(Invoice obj)
    {
      if (ModelState.IsValid)
      {
        _unitOfWork.Invoice.Add(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category created successfully";
        return RedirectToAction("Index");
      }
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

    [HttpGet]
    public IActionResult GetParent()
    {
      IEnumerable<ParentDetail> parentDetails = _unitOfWork.ParentDetail.GetAll();
      return Json(new { data = parentDetails });
    }

    [HttpGet]
    public IActionResult GetProgramList(int? parentId)
    {
      var userId = _unitOfWork.ParentDetail.Find(u => u.ParentID == parentId).Select(i => i.UserId).FirstOrDefault();
      var childId = _unitOfWork.Student.Find(u => u.UserId == userId).Select(i => i.StudentID).FirstOrDefault();
      var programIds = _unitOfWork.StudentProgram.Find(z => z.StudentID == childId && z.Status == StudentStatus.Ongoing).Select(o => o.ProgramID).FirstOrDefault();
      var stepId = _unitOfWork.Program.Find(u => u.ProgramID == programIds).Select(i => i.StepId).FirstOrDefault();
      var programs = _unitOfWork.Program.Find(u => u.StepId == stepId).ToList();
      //var programs = _unitOfWork.Program.GetA(l => programIds.Contains(l.ProgramID)).ToList();

      //cari step
      //if combinepricing, amik price dari step, else amik price dari program based on progid

      InvoiceVM vm = new InvoiceVM()
      {
        ProgramList = programs,
        Steps = _unitOfWork.Step.Find(v => v.StepId == stepId).ToList(),
      };

      return Json(new { data = vm });
    }

    #endregion
  }

  public class DashboardViewModel
  {
    public int ParentsCount { get; set; }
    public int InvoicesCount { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal TotalUnpaid { get; set; }
    public List<ParentDetail> parentDetails { get; set; }
    public List<Invoice> Invoices { get; set; }
  }
}


