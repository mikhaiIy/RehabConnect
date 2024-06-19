using Microsoft.AspNetCore.Mvc;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class InvoiceController : Controller
  {
    public InvoiceController(IUnitOfWork unitOfWork)
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


