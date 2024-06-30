using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Collections.Generic;
using RehabConnect.Models.ViewModel;

namespace RehabConnectWeb.Areas.CustomerSupport.Controllers
{
  [Area("CustomerSupport")]
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

    public IActionResult StudentProgram()
    {
      return View();
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
    public IActionResult Upsert(Student student)
    {
      if (ModelState.IsValid)
      {
        Student existingStudent = null;

        if (student.StudentID != 0)
        {
          // Retrieve the existing student from the context
          existingStudent = _unitOfWork.Student.Get(u => u.StudentID == student.StudentID, includeProperties: "Therapist");
          if (existingStudent == null)
          {
            return NotFound();
          }

          // Update existing student entity with new values
          existingStudent.ChildName = student.ChildName;
          existingStudent.ChildIC = student.ChildIC;
          existingStudent.ChildAge = student.ChildAge;
          existingStudent.ChildDOB = student.ChildDOB;
          existingStudent.ChildPassportNo = student.ChildPassportNo;
          existingStudent.ChildNationality = student.ChildNationality;
          existingStudent.ChildRace = student.ChildRace;
          existingStudent.ChildBirthPlace = student.ChildBirthPlace;
          existingStudent.ChildSex = student.ChildSex;
          existingStudent.ChildAddress = student.ChildAddress;
          existingStudent.ChildPostcode = student.ChildPostcode;
          existingStudent.ChildCity = student.ChildCity;
          existingStudent.ChildCountry = student.ChildCountry;
          existingStudent.Pediatricians = student.Pediatricians;
          existingStudent.HospReccommendation = student.HospReccommendation;
          existingStudent.DeadlineDiagnose = student.DeadlineDiagnose;
          existingStudent.DiagnosisOrCondition = student.DiagnosisOrCondition;
          existingStudent.OccupationalTheraphyPlace = student.OccupationalTheraphyPlace;
          existingStudent.SpeechTheoryPlace = student.SpeechTheoryPlace;
          existingStudent.OthersUnitPlace = student.OthersUnitPlace;
          existingStudent.TherapistID = student.TherapistID;
        }
        else
        {
          // New student, ensure UserId is set appropriately
          // For example, if Parent is logged in, set the UserId
          // Otherwise, ensure it's properly set from the logged-in user's context
          // student.UserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Example of setting UserId based on current user

          _unitOfWork.Student.Add(student);
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
    public IActionResult GetStudentProgram()
    {
      // Getting all Students Details
      var studentList = _unitOfWork.Student.GetAll(includeProperties: "Therapist");
      var studentPrograms = new List<StudentProgramVM>();

      foreach (var obj in studentList)
      {
        var student = _unitOfWork.Student.Get(u => u.StudentID == obj.StudentID);
        var studentProgram = _unitOfWork.StudentProgram.Get(u => u.StudentID == student.StudentID && u.Status==StudentStatus.Ongoing);
        var program = _unitOfWork.Program?.Get(u => u.ProgramID == studentProgram.ProgramID);
        var step = _unitOfWork.Step.Get(u => u.StepId == program.StepId);
        var roadmap = _unitOfWork.Roadmap.Get(u => u.RoadmapId == step.RoadmapId);

        var studentProgramVm = new StudentProgramVM
        {
          StudentId = student.StudentID,
          StudentName = student.ChildName,
          RoadmapName = roadmap.Name,
          StepName = step.Title,
          ProgramName = program.ProgramName,
          Status = studentProgram.Status.ToString()
        };

        studentPrograms.Add(studentProgramVm);
      }
      return Json(new { data = studentPrograms });
    }

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
