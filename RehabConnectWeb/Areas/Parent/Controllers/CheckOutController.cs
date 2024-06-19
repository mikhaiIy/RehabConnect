using Microsoft.AspNetCore.Mvc;
using RehabConnect.DataAccess.Repository;
using Stripe.Checkout;
using Stripe;
using RehabConnect.DataAccess.Repository.IRepository;

namespace RehabConnectWeb.Areas.Parent.Controllers
{
  [Area("Parent")]
  public class CheckOutController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    public CheckOutController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
      IEnumerable<RehabConnect.Models.Program> objProgramList = _unitOfWork.Program.Find(u => u.Step.StepId == 3);
      return View(objProgramList);
    }

    //public IActionResult OrderConfirmation()
    //{
    //    var service = new Stripe.Checkout.SessionService();
    //    Stripe.Checkout.Session session = service.Get(TempData["Session"].ToString());
    //    if (session.PaymentStatus == "paid")
    //    {
    //        var transaction = session.PaymentIntentId.ToString();
    //        return View("Success");
    //    }
    //    return View("Failed");
    //}

    public IActionResult OrderConfirmation()
    {
      var service = new SessionService();
      var session = service.Get(TempData["Session"].ToString());
      if (session.PaymentStatus == "paid")
      {
        // Create an invoice
        var options = new InvoiceCreateOptions
        {
          Customer = session.CustomerId, // Replace with your actual customer ID
          AutoAdvance = true, // Automatically charge the invoice
          CollectionMethod = "charge_automatically",
          //BillingReason = "manual",
          Currency = session.Currency,
          //CustomerEmail = session.CustomerEmail,
          Description = "Rehab Session Invoice",
          Metadata = new Dictionary<string, string>
                    {
                        { "session_id", session.Id }
                    }
        };

        var invoiceService = new InvoiceService();
        var invoice = invoiceService.Create(options);

        TempData["InvoiceId"] = invoice.Id; // Store invoice ID for later use

        var transaction = session.PaymentIntentId.ToString();
        //return RedirectToAction("Invoice", new { invoiceId = invoice.Id });

        return View("Success");
      }
      return View("Failed");
    }

    public IActionResult Success()
    {
      return View();
    }

    public IActionResult Failed()
    {
      return View();
    }

    public IActionResult CheckOut()
    {
      IEnumerable<RehabConnect.Models.Program> objProgramList = _unitOfWork.Program.Find(u => u.Step.StepId == 3);
      var domain = "https://localhost:7230/";

      // Create or retrieve customer
      var customerService = new CustomerService();
      var customerOptions = new CustomerCreateOptions
      {
        Email = "adamfahmi.taufiq02@gmail.com",
        Name = "Adam Fahmi"
      };
      var customer = customerService.Create(customerOptions);

      var options = new SessionCreateOptions
      {
        SuccessUrl = domain + $"Parent/CheckOut/OrderConfirmation",
        CancelUrl = domain + "Parent/CheckOut/Failed",
        LineItems = new List<SessionLineItemOptions>(),
        Mode = "payment",
        //CustomerEmail = "testdefault@gmail.com",
        Customer = customer.Id, // Attach customer ID to the session
        ClientReferenceId = customer.Id, // Set client reference ID for customer
      };

      var today = DateTime.Now.DayOfWeek;
      var isWeekend = (today == DayOfWeek.Saturday || today == DayOfWeek.Sunday);

      foreach (var obj in objProgramList)
      {
        var price = isWeekend ? obj.PriceWeekend : obj.PriceWeekday;
        var sessionListItem = new SessionLineItemOptions
        {
          PriceData = new SessionLineItemPriceDataOptions
          {
            UnitAmount = (long)price * 100,
            Currency = "myr",
            ProductData = new SessionLineItemPriceDataProductDataOptions
            {
              Name = obj.ProgramName.ToString(),
            }
          },
          Quantity = 1
        };
        options.LineItems.Add(sessionListItem);

        // Add session_id to metadata
        options.Metadata = new Dictionary<string, string>
                {
                    { "session_id", obj.SessionID.ToString() }
                };
      }


      var service = new Stripe.Checkout.SessionService();
      Stripe.Checkout.Session session = service.Create(options);
      TempData["Session"] = session.Id;
      Response.Headers.Append("Location", session.Url);
      return new StatusCodeResult(303);
    }

  }
}
