using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using Newtonsoft.Json;
using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using System.Runtime.InteropServices;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RehabConnectWeb.Areas.Parent.Controllers;

[Area("Parent")]
public class SessionController : Controller
{
  private readonly IUnitOfWork _unitOfWork;

  public SessionController(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public IActionResult Index()
  {
    return View();
  }

}
