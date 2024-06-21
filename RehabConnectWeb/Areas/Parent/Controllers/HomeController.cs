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

  }
}


