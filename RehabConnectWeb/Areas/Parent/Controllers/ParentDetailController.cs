using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using RehabConnect.Utility;

namespace RehabConnectWeb.Areas.Parent.Controllers
{
  [Area("Parent")]
  [Authorize(Roles = SD.Role_Parent)]
  public class ParentDetailController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public ParentDetailController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var objParentDetailList = _unitOfWork.ParentDetail.GetAll(u => u.UserId == userId).ToList();
      return View(objParentDetailList);
    }

    public IActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Create(ParentDetail obj)
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      obj.UserId = userId; // Set the user ID

      if (ModelState.IsValid)
      {
        _unitOfWork.ParentDetail.Add(obj);
        _unitOfWork.Save();
        TempData["success"] = "ParentDetail created successfully";
        return RedirectToAction("Index", "Student", new { Area = "Parent" });
      }
      else
      {
        return View(obj);
      }
    }

    public IActionResult Edit(int? parentid)
    {
      if (parentid == null || parentid == 0)
      {
        return NotFound();
      }

      var parentDetailFromDb = _unitOfWork.ParentDetail.Get(u => u.ParentID == parentid);
      if (parentDetailFromDb == null)
      {
        return NotFound();
      }

      return View(parentDetailFromDb);
    }

    [HttpPost]
    public IActionResult Edit(ParentDetail obj)
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      obj.UserId = userId; // Set the user ID

      if (ModelState.IsValid)
      {
        _unitOfWork.ParentDetail.Update(obj);
        _unitOfWork.Save();
        TempData["success"] = "ParentDetail updated successfully";
        return RedirectToAction("Index");
      }
      else
      {
        return View(obj);
      }
    }

    public IActionResult Delete(int? parentid)
    {
      if (parentid == null || parentid == 0)
      {
        return NotFound();
      }

      var parentDetailFromDb = _unitOfWork.ParentDetail.Get(u => u.ParentID == parentid);
      if (parentDetailFromDb == null)
      {
        return NotFound();
      }

      return View(parentDetailFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? parentid)
    {
      var obj = _unitOfWork.ParentDetail.Get(u => u.ParentID == parentid);
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
