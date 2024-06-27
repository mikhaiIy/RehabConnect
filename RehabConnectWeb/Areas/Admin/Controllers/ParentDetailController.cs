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
    public IActionResult Upsert(ParentDetail ParentDetailObj)
    {
      if (ModelState.IsValid)
      {
        if (ParentDetailObj.ParentID == 0)
        {
          // New parent detail, ensure UserId is set appropriately
          // For example, if Admin is creating, set the UserId
          // Otherwise, ensure it's properly set from the logged-in user's context
          // ParentDetailObj.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Example of setting UserId based on current user

          _unitOfWork.ParentDetail.Add(ParentDetailObj);
        }
        else
        {
          // Existing parent detail, retrieve existing entity and update properties
          ParentDetail existingParentDetail = _unitOfWork.ParentDetail.Get(u => u.ParentID == ParentDetailObj.ParentID);
          if (existingParentDetail == null)
          {
            return NotFound();
          }

          // Update existing parent detail entity with new values
          existingParentDetail.FatherName = ParentDetailObj.FatherName;
          existingParentDetail.FatherPhoneNum = ParentDetailObj.FatherPhoneNum;
          existingParentDetail.FatherIC = ParentDetailObj.FatherIC;
          existingParentDetail.FatherRace = ParentDetailObj.FatherRace;
          existingParentDetail.FatherOccupation = ParentDetailObj.FatherOccupation;
          existingParentDetail.FatherEmail = ParentDetailObj.FatherEmail;
          existingParentDetail.FatherAddress = ParentDetailObj.FatherAddress;
          existingParentDetail.FatherPostcode = ParentDetailObj.FatherPostcode;
          existingParentDetail.FatherCity = ParentDetailObj.FatherCity;
          existingParentDetail.FatherCountry = ParentDetailObj.FatherCountry;
          existingParentDetail.FatherWorkAddress = ParentDetailObj.FatherWorkAddress;

          existingParentDetail.MotherName = ParentDetailObj.MotherName;
          existingParentDetail.MotherPhoneNum = ParentDetailObj.MotherPhoneNum;
          existingParentDetail.MotherIC = ParentDetailObj.MotherIC;
          existingParentDetail.MotherRace = ParentDetailObj.MotherRace;
          existingParentDetail.MotherOccupation = ParentDetailObj.MotherOccupation;
          existingParentDetail.MotherEmail = ParentDetailObj.MotherEmail;
          existingParentDetail.MotherAddress = ParentDetailObj.MotherAddress;
          existingParentDetail.MotherPostcode = ParentDetailObj.MotherPostcode;
          existingParentDetail.MotherCity = ParentDetailObj.MotherCity;
          existingParentDetail.MotherCountry = ParentDetailObj.MotherCountry;
          existingParentDetail.MotherWorkAddress = ParentDetailObj.MotherWorkAddress;

          existingParentDetail.HouseholdIncome = ParentDetailObj.HouseholdIncome;

          // Optionally, set UserId here if needed based on user roles
          // existingParentDetail.UserId = ParentDetailObj.UserId;

          _unitOfWork.ParentDetail.Update(existingParentDetail);
        }

        _unitOfWork.Save();
        TempData["success"] = "ParentDetail saved successfully";
        return RedirectToAction("Index");
      }
      else
      {
        return View(ParentDetailObj);
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
