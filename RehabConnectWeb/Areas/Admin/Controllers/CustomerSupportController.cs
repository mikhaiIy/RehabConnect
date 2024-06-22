using RehabConnect.DataAccess.Data;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
//using RehabConnect.Models.ViewModels;
using RehabConnect.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CustomerSupportController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerSupportController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<RehabConnect.Models.CustomerSupport> objCustomerSupportList = _unitOfWork.CustomerSupport.GetAll().ToList();

            return View(objCustomerSupportList);

        }

        public IActionResult Upsert(int? csid)
        {

            if(csid == null || csid == 0)
            {
                //create
                return View(new RehabConnect.Models.CustomerSupport());
            }
            else
            {
                //update
                RehabConnect.Models.CustomerSupport customerSupportObj = _unitOfWork.CustomerSupport.Get(u => u.CSID == csid);
                if (customerSupportObj == null)
                {
                    return NotFound();
                }
                return View(customerSupportObj);
            }

        }

        [HttpPost]
        public IActionResult Upsert(RehabConnect.Models.CustomerSupport CustomerSupportObj)
        {
          if(ModelState.IsValid)
          { 

                    if(CustomerSupportObj.CSID == 0)
                    {
                        _unitOfWork.CustomerSupport.Add(CustomerSupportObj);
                    }
                    else
                    {
                        _unitOfWork.CustomerSupport.Update(CustomerSupportObj);
                    }
                    _unitOfWork.Save();
                    TempData["success"] = "Customer Support created successfully";
                    return RedirectToAction("Index");
          }
          else
          {
        return View(CustomerSupportObj);

          }
    }

    #region API CALLS
    [HttpGet]
        public IActionResult GetAll()
        {
            List<RehabConnect.Models.CustomerSupport> objCustomerSupportList = _unitOfWork.CustomerSupport.GetAll().ToList();
            return Json(new { data = objCustomerSupportList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CustomerSupportToBeDeleted = _unitOfWork.CustomerSupport.Get(u => u.CSID == id);

            if(CustomerSupportToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error  while deleting" });
            }

            _unitOfWork.CustomerSupport.Remove(CustomerSupportToBeDeleted);
            _unitOfWork.Save();

            return Json(new { succes = true, message = "Delete successful" });
        }

        #endregion
    }
}
