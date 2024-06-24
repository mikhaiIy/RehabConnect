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
    public class ParentDetailController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ParentDetailController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<RehabConnect.Models.ParentDetail> objParentDetailList = _unitOfWork.ParentDetail.GetAll().ToList();

            return View(objParentDetailList);

        }

        public IActionResult Upsert(int? parentid)
        {
            if (parentid == null || parentid == 0)
            {
                // create
                return View(new RehabConnect.Models.ParentDetail());
            }
            else
            {
                // update
                RehabConnect.Models.ParentDetail ParentDetailObj = _unitOfWork.ParentDetail.Get(u => u.ParentID == parentid);
                if (ParentDetailObj == null)
                {
                    return NotFound();
                }
                return View(ParentDetailObj);
            }
        }



        [HttpPost]
        public IActionResult Upsert(RehabConnect.Models.ParentDetail ParentDetailObj)
        {
      if(ModelState.IsValid) { 
                if(ParentDetailObj.ParentID == 0)
                {
                    _unitOfWork.ParentDetail.Add(ParentDetailObj);
                }
                else
                {
                    _unitOfWork.ParentDetail.Update(ParentDetailObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "ParentDetail created successfully";
                return RedirectToAction("Index");
      }
      else
      {
        return View(ParentDetailObj) ;
      }

    }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<RehabConnect.Models.ParentDetail> objParentDetailList = _unitOfWork.ParentDetail.GetAll().ToList();
            return Json(new { data = objParentDetailList });
        }


    [HttpDelete]
    public IActionResult Delete(int? id)
    {
      var parentToBeDeleted = _unitOfWork.ParentDetail.Get(u => u.ParentID == id);
      if (parentToBeDeleted == null)
      {
        return Json(new { success = false, message = "Error while deleting" });
      }

      _unitOfWork.ParentDetail.Remove(parentToBeDeleted);
      _unitOfWork.Save();

      return Json(new { success = true, message = "Delete successful" });
    }

    #endregion
  }
}
