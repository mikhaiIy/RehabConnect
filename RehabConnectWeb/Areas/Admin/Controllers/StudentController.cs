using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
            List<Student> objStudentList = _unitOfWork.Student.GetAll().ToList();
            return View(objStudentList);
        }

        public IActionResult Approve(int? childid)
        {
            if (childid == null || childid == 0)
            {
                return NotFound();
            }

            Student? studentFromDb = _unitOfWork.Student.Get(u => u.StudentID == childid);

            if (studentFromDb == null)
            {
                return NotFound();
            }

            return View(studentFromDb);
        }

        [HttpPost]
        public IActionResult Approve(Student obj)
        {
            Student? studentFromDb = _unitOfWork.Student.Get(u => u.StudentID == obj.StudentID);

            if (studentFromDb == null)
            {
                return NotFound();
            }

            studentFromDb.ApprovalStatus = "Approved";
            _unitOfWork.Student.Update(studentFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Student approved successfully";
            return RedirectToAction("Index");
        }

        public IActionResult NotApprove(int? childid)
        {
            if (childid == null || childid == 0)
            {
                return NotFound();
            }

            Student? studentFromDb = _unitOfWork.Student.Get(u => u.StudentID == childid);

            if (studentFromDb == null)
            {
                return NotFound();
            }

            return View(studentFromDb);
        }

        [HttpPost]
        public IActionResult NotApprove(Student obj)
        {
            Student? studentFromDb = _unitOfWork.Student.Get(u => u.StudentID == obj.StudentID);

            if (studentFromDb == null)
            {
                return NotFound();
            }

            studentFromDb.ApprovalStatus = "NotApproved";
            _unitOfWork.Student.Update(studentFromDb);
            _unitOfWork.Save();

            TempData["success"] = "Student not approved";
            return RedirectToAction("Index");
        }
    }
}
