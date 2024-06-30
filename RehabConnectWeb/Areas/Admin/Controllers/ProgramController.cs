using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RehabConnect.Utility;
using RehabConnect.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using RehabConnect.ViewModels;
using RehabConnect.Models.ViewModel;
using RehabConnect.Models;
using RehabConnect.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;


namespace RehabConnectWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
  [Authorize(Roles = SD.Role_Admin)]
  public class ProgramController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProgramController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int? id)
        {
            IEnumerable<RehabConnect.Models.Program> objProgramList = _unitOfWork.Program.Find(u => u.Step.StepId == id);
            return View(objProgramList);
        }

        public IActionResult Upsert(int? id, int? step, int? roadmap)
        {
          StepVM stepVm = new();
          stepVm.Step = _unitOfWork.Step.Get(u => u.StepId == step);
          stepVm.Roadmap = _unitOfWork.Roadmap.Get(u => u.RoadmapId == roadmap);


          if (id is null or 0)
          {
            // Create New Program
            return View(stepVm);
          }
          else
          {
            // Update Program

            stepVm.Program = _unitOfWork.Program.Get(u => u.ProgramID == id);
            return View(stepVm);
          }

        }

        [HttpPost]
        public IActionResult Upsert(StepVM obj, int? step)
        {

          if (ModelState.IsValid)
          {

            if (obj.Program.ProgramID == 0)
            { // Create new Program
              _unitOfWork.Program.Add(obj.Program);
            }
            else
            { // Update Program
              _unitOfWork.Program.Update(obj.Program);
            }
            _unitOfWork.Save();
            TempData["success"] = "Program Created Successfully";
            return RedirectToAction("Index", new {id = step});
          }
          else
          {
            // returning and populating the form with the previously entered data.
            return View(obj);
          }
        }


        public IActionResult Delete(int? id)
        {
          if (id is null or 0)
          {
            return NotFound();
          }

          RehabConnect.Models.Program programFromDb = _unitOfWork.Program.Get(u => u.ProgramID == id);
          if (programFromDb == null)
          {
            return NotFound();
          }

          return View(programFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id) /*since the name and parameters is the same as above, cannot have the same name*/
        {
          RehabConnect.Models.Program obj = _unitOfWork.Program.Get(u => u.ProgramID == id);

          var stepId = obj.StepId;

          if (obj == null)
          {
            return NotFound();
          }

          _unitOfWork.Program.Remove(obj);
          _unitOfWork.Save(); //hey now do it.
          TempData["success"] = "Step Deleted Successfully";
          return RedirectToAction("Index", "Program", new{id = stepId}); //go to the Step controller, Index page.

        }
    }
}
