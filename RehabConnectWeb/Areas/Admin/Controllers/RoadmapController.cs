using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RehabConnect.Models;
using RehabConnect.Models.ViewModel;
using RehabConnect.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using RehabConnect.Utility;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
  [Authorize(Roles = SD.Role_Admin)]
  public class RoadmapController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoadmapController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Roadmap> objRoadmapList = _unitOfWork.Roadmap.GetAll().ToList();
            return View(objRoadmapList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id is null or 0)
            {
                return View();
            }

            Roadmap? roadmapFromDb = _unitOfWork.Roadmap.Get(u => u.RoadmapId == id);
            if (roadmapFromDb == null)
            {
                return NotFound();
            }

            return View(roadmapFromDb);
        }

        [HttpPost]
        public IActionResult Upsert(Roadmap obj)
        {
            
            // if (ModelState.IsValid)
            // {
                if (obj.RoadmapId == 0)
                {
                    _unitOfWork.Roadmap.Add(obj);

                }
                else
                {
                    _unitOfWork.Roadmap.Update(obj);
                }

                //Saving
                _unitOfWork.Save();
                TempData["success"] = "Roadmap Created Successfully!";
                return RedirectToAction("Index", "Roadmap");
            // }
            //
            // return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id is null or 0)
            {
                return NotFound();
            }

            Roadmap? roadmapFromDb = _unitOfWork.Roadmap.Get(u => u.RoadmapId == id);
            if (roadmapFromDb == null)
            {
                return NotFound();
            }

            return View(roadmapFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id) /*since the name and parameters is the same as above, cannot have the same name*/
        {
            Roadmap? obj = _unitOfWork.Roadmap.Get(u => u.RoadmapId == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Roadmap.Remove(obj);
            _unitOfWork.Save(); //hey now do it.
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index", "Roadmap"); //go to the Category controller, Index page.
        }
    }
}

