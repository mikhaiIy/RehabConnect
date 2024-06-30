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
      model.Item = _unitOfWork.InvoiceItem.GetAll().ToList();

      return View(model);
    }

    public IActionResult Billing(int? id)
    {
      if (id == null || id == 0)
      {
        return NotFound();
      }

      var objBillingList = _unitOfWork.Billing.report(b => b.InvoiceID == id, includeProperties: "Invoice,Invoice.ParentDetail").ToList();
      return View(objBillingList);
    }

    public IActionResult Add()
    {
      var model = new InvoiceAddVM();

      return View(model);
    }

    [HttpPost]
    public IActionResult CreateInvoice(InvoiceAddVM obj)
    {
      if (ModelState.IsValid)
      {
        // Ensure InvoiceItems are added to the Invoice object
        _unitOfWork.Invoice.Add(obj.Invoice);
        _unitOfWork.Save();

        obj.Item.InvoiceId = obj.Invoice.InvoiceId;

        _unitOfWork.InvoiceItem.Add(obj.Item);
        _unitOfWork.Save();

        return RedirectToAction(nameof(Index)); // Redirect to your desired action
      }

      // If ModelState is not valid, return to the view with the invalid invoice object
      return View(obj);
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

    public IActionResult Edit(int id)
    {
      var invoice = _unitOfWork.Invoice.Get(i => i.InvoiceId == id, includeProperties:"ParentDetail");

      InvoiceEditVM obj = new InvoiceEditVM()
      {
        Invoice = invoice,
        Item = _unitOfWork.InvoiceItem.Get(i => i.InvoiceId == id)
      };

      return View(obj);
    }

    [HttpPost]
    public IActionResult Edit()
    {


      return View();
    }

    public IActionResult Preview(int id)
    {
      var invoice = _unitOfWork.Invoice.Get(i => i.InvoiceId == id, includeProperties: "ParentDetail");

      InvoicePreviewVM vm = new InvoicePreviewVM()
      {
        Invoice = invoice,
        Item = _unitOfWork.InvoiceItem.Get(i => i.InvoiceId == id)
      };

      return View(vm);
    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
      var invoiceToBeDeleted = _unitOfWork.Invoice.Get(u => u.InvoiceId == id);
      if (invoiceToBeDeleted == null)
      {
        return Json(new { success = false, message = "Error while deleting" });
      }
      else
      {
        _unitOfWork.Invoice.Remove(invoiceToBeDeleted);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful" });
        return RedirectToAction(nameof(Index));
      }

    }

    #region API CALLS

    [HttpGet]
    public IActionResult GetInvoices()
    {
      InvoiceBillingVM invoiceBillingVm = new InvoiceBillingVM()
      {
        Invoice = _unitOfWork.Invoice.GetAll(includeProperties: "ParentDetail").ToList(),
        Billing = _unitOfWork.Billing.GetAll(includeProperties: "Invoice").ToList(),
        Item = _unitOfWork.InvoiceItem.GetAll().ToList()
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
    public List<InvoiceItem> Item { get; set; }
  }
}


