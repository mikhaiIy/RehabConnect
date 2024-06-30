using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using RehabConnect.Utility;
using RehabConnectWeb.Areas.Admin.Controllers;
using System.Security.Claims;

namespace RehabConnectWeb.Areas.Parent.Controllers
{
  [Area("Parent")]
  [Authorize(Roles = SD.Role_Parent)]
  public class PaymentController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public PaymentController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
      _unitOfWork = unitOfWork;
      _webHostEnvironment = webHostEnvironment;
    }
    //public IActionResult Index()
    //{
    //  string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    //  if (userId == null)
    //  {
    //    return NotFound("User ID not found");
    //  }

    //  List<Invoice> objInvoiceList = _unitOfWork.Invoice
    //      .report(r => r.ParentDetail.UserId == userId, includeProperties: "ParentDetail")
    //      .ToList();

    //  return View(objInvoiceList);
    //}

    public IActionResult Index()
    {
      var currentUserEmail = User.Identity.Name;
      var objInvoiceList = _unitOfWork.Invoice.GetAll(
          includeProperties: "ParentDetail")
          .Where(i => i.Email == currentUserEmail)
          .ToList();
      return View(objInvoiceList);
    }

    public IActionResult History(int? id)
    {
      if (id == null || id == 0)
      {
        return NotFound();
      }

      var objBillingList = _unitOfWork.Billing.report(b => b.InvoiceID == id, includeProperties: "Invoice").ToList();
      return View(objBillingList);
    }


    public IActionResult Pay(int id)
    {
      var invoice = _unitOfWork.Invoice.Get(i => i.InvoiceId == id, includeProperties: "ParentDetail");
      if (invoice == null)
      {
        return NotFound();
      }

      var paymentViewModel = new PaymentVM
      {
        InvoiceId = invoice.InvoiceId,
        ParentName = invoice.ParentDetail.FatherName,
        TotalAmount = invoice.Total,
      };

      return View(paymentViewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Pay(PaymentVM model, IFormFile? file)
    {
      if (ModelState.IsValid)
      {
        string wwwRoothPath = _webHostEnvironment.WebRootPath;
        if (file != null)
        {
          string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
          string productPath = Path.Combine(wwwRoothPath, @"img\reciept");

          if (!string.IsNullOrEmpty(model.Reciept))
          {
            //Delete old image
            var oldImagePath =
                Path.Combine(wwwRoothPath, model.Reciept.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
              System.IO.File.Delete(oldImagePath);
            }
          }
          using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
          {
            file.CopyTo(fileStream);
          }
          model.Reciept = @"\img\reciept\" + fileName;
        }
        var billing = new Billing
        {
          InvoiceID = model.InvoiceId,
          Amount = model.Amount,
          Reciept = model.Reciept,
          Status = "Pending",  // or any logic to determine the status
          ConfirmStatus = false
        };

        _unitOfWork.Billing.Add(billing);
        _unitOfWork.Save();

        return RedirectToAction(nameof(Index));
      }
      return View(model);
    }
  }
}
