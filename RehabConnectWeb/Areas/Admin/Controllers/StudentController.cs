using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  public class StudentController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;

    public StudentController(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
      var objStudentList = _unitOfWork.Student.GetAll(includeProperties: "Therapist").ToList();
      return View(objStudentList);
    }

    public IActionResult Upsert(int? Studentid)
    {
      Student student = new Student();
      PopulateTherapistDropDownList();

      if (Studentid == null || Studentid == 0)
      {
        // create
        return View(student);
      }
      else
      {
        // update
        student = _unitOfWork.Student.Get(u => u.StudentID == Studentid, includeProperties: "Therapist");
        if (student == null)
        {
          return NotFound();
        }
        PopulateTherapistDropDownList(student.TherapistID);
        return View(student);
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(Student student)
    {
      if (ModelState.IsValid)
      {
        if (student.StudentID == 0)
        {
          _unitOfWork.Student.Add(student);
        }
        else
        {
          _unitOfWork.Student.Update(student);
        }
        _unitOfWork.Save();
        TempData["success"] = "Student saved successfully";
        return RedirectToAction("Index");
      }
      else
      {
        PopulateTherapistDropDownList(student.TherapistID);
        return View(student);
      }
    }

    private void PopulateTherapistDropDownList(object selectedTherapist = null)
    {
      var therapistsQuery = _unitOfWork.Therapist.GetAll().OrderBy(t => t.TherapistName);
      ViewBag.Therapists = new SelectList(therapistsQuery, "TherapistID", "TherapistName", selectedTherapist);
    }

    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
      var objStudentList = _unitOfWork.Student.GetAll(includeProperties: "Therapist")
          .Select(s => new
          {
            s.StudentID,
            s.ChildName,
            s.ChildAge,
            s.ChildRace,
            s.ChildSex,
            s.ChildIC,
            TherapistName = s.Therapist != null ? s.Therapist.TherapistName : "No Therapist Assigned"
          }).ToList();

      return Json(new { data = objStudentList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
      var studentToBeDeleted = _unitOfWork.Student.Get(u => u.StudentID == id);
      if (studentToBeDeleted == null)
      {
        return Json(new { success = false, message = "Error while deleting" });
      }

      _unitOfWork.Student.Remove(studentToBeDeleted);
      _unitOfWork.Save();

      return Json(new { success = true, message = "Delete successful" });
    }
    #endregion
  }
}
