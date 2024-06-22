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
    public class StepController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public StepController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index(int id)
        {
          var stepIds = _unitOfWork.Step.Find(u => u.RoadmapId == id).Select(s => s.StepId).ToList();

          StepProgramVM stepProgramVm = new StepProgramVM()
          {
            Step = _unitOfWork.Step.Find(u => u.RoadmapId == id),
            Program = _unitOfWork.Program.Find(p => stepIds.Contains(p.StepId))
          };


          // IEnumerable<Step> objStepList = _unitOfWork.Step.Find(u => u.Roadmap.RoadmapId == id);
          return View(stepProgramVm);
        }

        public IActionResult Create(int? id, int? roadmap)
        {
          StepVM stepVm = new();


          if (id is null or 0)
          {
            // Create New Step
            stepVm.Roadmap = _unitOfWork.Roadmap.Get(u=>u.RoadmapId==roadmap);
            return View(stepVm);
          }
          else
          {
            // Update Step
            stepVm.Step = _unitOfWork.Step.Get(u => u.StepId == id);
            stepVm.Roadmap = _unitOfWork.Roadmap.Get(u=>u.RoadmapId==roadmap);
            return View(stepVm);
          }

        }

        public IActionResult Upsert(int? id, int? roadmap)
        {
          StepVM stepVm = new();
          stepVm.Roadmap = _unitOfWork.Roadmap.Get(u=>u.RoadmapId==roadmap);

            if (id is null or 0)
            {
              // Create New Step

                return View(stepVm);
            }
            else
            {
              // Update Step
                stepVm.Step = _unitOfWork.Step.Get(u => u.StepId == id);
                return View(stepVm);
            }

        }

        [HttpPost]
        public IActionResult Upsert(StepVM obj)
        {
          var roadmapId = obj.Step.RoadmapId;

            if (ModelState.IsValid)
            {
                if(obj.Step.StepId==0)
                {
                    _unitOfWork.Step.Add(obj.Step); //hey add this Step obj into categories table(telling what it will do)
                }
                else
                {
                    _unitOfWork.Step.Update(obj.Step); //hey add this Step obj into categories table(telling what it will do)
                }

                _unitOfWork.Save(); //hey now do it.
                TempData["success"] = "Step Created Successfully";
                return RedirectToAction("Index", "Step", new{id = roadmapId}); //go to the Step controller, Index page.
            }
            else
            {
                //populating the input with the data previously entered, and not gives out the ugly exception.
                return View(obj);
            }

        }

        public IActionResult Delete(int? id)
        {
          if (id is null or 0)
          {
            return NotFound();
          }

          Step? stepFromDb = _unitOfWork.Step.Get(u => u.StepId == id);
          if (stepFromDb == null)
          {
            return NotFound();
          }

          return View(stepFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id) /*since the name and parameters is the same as above, cannot have the same name*/
        {
          Step? obj = _unitOfWork.Step.Get(u => u.StepId == id);

          var roadmapId = obj.RoadmapId;

          if (obj == null)
          {
            return NotFound();
          }

          _unitOfWork.Step.Remove(obj);
          _unitOfWork.Save(); //hey now do it.
          TempData["success"] = "Step Deleted Successfully";
          return RedirectToAction("Index", "Step", new{id = roadmapId}); //go to the Step controller, Index page.

        }
    }
}
