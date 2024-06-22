using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace RehabConnectWeb.Areas.Identity.Pages.Account
{
  //[Authorize(Roles = SD.Role_Parent)]
  public class RegisterMultiSteps: Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    public RegisterMultiSteps(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }
    public IActionResult Index()
    {
      List<ParentDetail> objParentDetailList = _unitOfWork.ParentDetail.GetAll().ToList();
      return View(objParentDetailList);

    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create(ParentDetail obj)
    {
      // Retrieve the user ID (assuming you're using ASP.NET Core Identity)
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      obj.UserId = userId; // Set the user ID


      _unitOfWork.ParentDetail.Add(obj);
      _unitOfWork.Save();
      TempData["success"] = "ParentDetail created successfully";
      return RedirectToAction("Index");
    }

    public IActionResult Edit(int? parentid)
    {
      if (parentid == null || parentid == 0)
      {
        return NotFound();
      }
      ParentDetail? ParentDetailFromDb = _unitOfWork.ParentDetail.Get(u => u.ParentID == parentid);
      //ParentDetail? ParentDetailFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
      //ParentDetail? ParentDetailFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

      if (ParentDetailFromDb == null)
      {
        return NotFound();
      }
      return View(ParentDetailFromDb);
    }

    [HttpPost]
    public IActionResult Edit(ParentDetail obj)
    {

      // Retrieve the user ID (assuming you're using ASP.NET Core Identity)
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      obj.UserId = userId; // Set the user ID

      _unitOfWork.ParentDetail.Update(obj);
      _unitOfWork.Save();
      TempData["success"] = "ParentDetail updated successfully";
      return RedirectToAction("Index");

    }

    public IActionResult Delete(int? parentid)
    {
      if (parentid == null || parentid == 0)
      {
        return NotFound();
      }
      ParentDetail? ParentDetailFromDb = _unitOfWork.ParentDetail.Get(u => u.ParentID == parentid);

      if (ParentDetailFromDb == null)
      {
        return NotFound();
      }
      return View(ParentDetailFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? parentid)
    {
      ParentDetail? obj = _unitOfWork.ParentDetail.Get(u => u.ParentID == parentid);
      if (obj == null)
      {
        return NotFound();
      }
      _unitOfWork.ParentDetail.Remove(obj);
      _unitOfWork.Save();
      TempData["success"] = "ParentDetail deleted successfully";
      return RedirectToAction("Index");
    }
  }
}

