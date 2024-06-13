using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RehabConnect.Utility;
using RehabConnect.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using RehabConnect.ViewModels;
using RehabConnect.Models.ViewModel;
using RehabConnect.Models;
using RehabConnect.DataAccess.Repository.IRepository;


namespace RehabConnectWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProgramController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProgramController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int id)
        {
            IEnumerable<RehabConnect.Models.Program> objProgramList = _unitOfWork.Program.Find(u => u.Step.StepId == id);
            return View(objProgramList);
        }

        public IActionResult Upsert(int? id)
        {
            return View();

        }

        public IActionResult Delete()
        {
            return View();
        }
    }
}