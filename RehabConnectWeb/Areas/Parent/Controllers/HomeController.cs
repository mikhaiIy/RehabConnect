using Microsoft.AspNetCore.Mvc;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;

namespace RehabConnectWeb.Areas.Parent.Controllers
{
  [Area("Parent")]
  public class HomeController : Controller
  {
    public HomeController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    private readonly IUnitOfWork _unitOfWork;

    public IActionResult Index() => View();

    public IActionResult Add() => View();
    public IActionResult Edit() => View();
    public IActionResult Preview() => View();
    public IActionResult Print() => View();

    public IActionResult Details(int SessionID)
    {
      ShoppingCart cart = new()
      {
        Program = _unitOfWork.Program.Get(u => u.SessionID == SessionID),
        SessionID = SessionID
      };
      return View(cart);
    }
  }
}


