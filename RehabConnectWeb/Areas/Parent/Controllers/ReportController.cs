using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Utility;

namespace RehabConnectWeb.Areas.Parent.Controllers
{
  [Area("Parent")]
  [Authorize(Roles = SD.Role_Parent)]
  public class ReportController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    public ReportController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
      List<Report> objReportList = _unitOfWork.Report.GetAll(includeProperties: "Student").ToList();
      return View(objReportList);
    }

    public IActionResult Print(int? id)
    {
      Report? reportFromDb = _unitOfWork.Report.Get(u => u.ReportID == id);
      return View(reportFromDb);
    }

    public IActionResult Info(int? id)
    {
      Report? reportFromDb = _unitOfWork.Report.Get(u => u.ReportID == id);
      return View(reportFromDb);
    }

  }
}
