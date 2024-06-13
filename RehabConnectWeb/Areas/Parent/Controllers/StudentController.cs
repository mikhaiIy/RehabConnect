using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using RehabConnect.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace RehabConnectWeb.Areas.Parent.Controllers
{
    [Area("Parent")]
    //[Authorize(Roles = SD.Role_Parent)]
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student obj)
        {
            // Retrieve the user ID (assuming you're using ASP.NET Core Identity)
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            obj.UserId = userId; // Set the user ID


            _unitOfWork.Student.Add(obj);
            _unitOfWork.Save();
            TempData["success"] = "Student created successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? childid)
        {
            if (childid == null || childid == 0)
            {
                return NotFound();
            }
            Student? StudentFromDb = _unitOfWork.Student.Get(u => u.StudentID == childid);
            //Student? StudentFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Student? StudentFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();

            if (StudentFromDb == null)
            {
                return NotFound();
            }
            return View(StudentFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Student obj)
        {

                // Retrieve the user ID (assuming you're using ASP.NET Core Identity)
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                obj.UserId = userId; // Set the user ID

                _unitOfWork.Student.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Student updated successfully";
                return RedirectToAction("Index");

        }

        public IActionResult Delete(int? childid)
        {
            if (childid == null || childid == 0)
            {
                return NotFound();
            }
            Student? StudentFromDb = _unitOfWork.Student.Get(u => u.StudentID == childid);

            if (StudentFromDb == null)
            {
                return NotFound();
            }
            return View(StudentFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? childid)
        {
            Student? obj = _unitOfWork.Student.Get(u => u.StudentID == childid);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Student.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Student deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
