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

    public IActionResult Billing(int? id)
    {
      if (id == null || id == 0)
      {
        return NotFound();
      }

      var objBillingList = _unitOfWork.Billing.report(b => b.InvoiceID == id, includeProperties: "Invoice").ToList();
      return View(objBillingList);
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

    public IActionResult Confirm(int id)
    {
      var billing = _unitOfWork.Billing.Get(b => b.BillingID == id, includeProperties: "Invoice");
      if (billing == null)
      {
        return NotFound();
      }

      billing.ConfirmStatus = true;
      billing.Status = "Paid";
      _unitOfWork.Billing.Update(billing);

      // Update the invoice total
      var invoice = _unitOfWork.Invoice.Get(i => i.InvoiceId == billing.InvoiceID);
      invoice.Total -= billing.Amount;
      if (invoice.Total == 0)
      {
        // Mark invoice as fully paid
        invoice.Status = "Fully Paid";
      }
      else
      {
        // Mark invoice as fully paid
        invoice.Status = "Partially Paid";
      }
      _unitOfWork.Invoice.Update(invoice);
      _unitOfWork.Save();

      return RedirectToAction(nameof(Index)); // Or any other view to redirect after confirmation
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


