using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class TherapistController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TherapistController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<RehabConnect.Models.Therapist> objTherapistList = _unitOfWork.Therapist.GetAll().ToList();

            return View(objTherapistList);

        }

        public IActionResult Upsert(int? therapistid)
        {
            if (therapistid == null || therapistid == 0)
            {
                // create
                return View(new RehabConnect.Models.Therapist());
            }
            else
            {
                // update
                RehabConnect.Models.Therapist therapistObj = _unitOfWork.Therapist.Get(u => u.TherapistID == therapistid);
                if (therapistObj == null)
                {
                    return NotFound();
                }
                return View(therapistObj);
            }
        }



        [HttpPost]
        public IActionResult Upsert(RehabConnect.Models.Therapist TherapistObj)
        {
                if(TherapistObj.TherapistID == 0)
                {
                    _unitOfWork.Therapist.Add(TherapistObj);
                }
                else
                {
                    _unitOfWork.Therapist.Update(TherapistObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Therapist created successfully";
                return RedirectToAction("Index");

        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<RehabConnect.Models.Therapist> objTherapistList = _unitOfWork.Therapist.GetAll().ToList();
            return Json(new { data = objTherapistList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var TherapistToBeDeleted = _unitOfWork.Therapist.Get(u => u.TherapistID == id);

            if (TherapistToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Therapist.Remove(TherapistToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful" });
        }

        #endregion
    }
}
