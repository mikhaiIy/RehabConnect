using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using RehabConnect.DataAccess.Repository.IRepository;
using RehabConnect.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
  [Area("Admin")]
  [Authorize(Roles = "Admin")] // Adjust according to your role setup
  public class AnnouncementController : Controller
  {
    private readonly IUnitOfWork _unitOfWork;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public AnnouncementController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
    {
      _unitOfWork = unitOfWork;
      _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
      List<Announcement> objAnnouncementList = _unitOfWork.Announcement.GetAll().ToList();
      return View(objAnnouncementList);
    }

    public IActionResult Upsert(int? announcementid)
    {
      Announcement announcement = new Announcement();

      if (announcementid == null || announcementid == 0)
      {
        // Create new announcement
        return View(announcement);
      }
      else
      {
        // Edit existing announcement
        announcement = _unitOfWork.Announcement.Get(u => u.AnnouncementID == announcementid);
        return View(announcement);
      }
    }

    [HttpPost]
    [HttpPost]
    public IActionResult Upsert(Announcement announcement, IFormFile? file)
    {
      if (ModelState.IsValid)
      {
        string wwwRootPath = _webHostEnvironment.WebRootPath;
        if (file != null)
        {
          // To make sure file is in .png, .jpg or .jpeg
          if (Path.GetExtension(file.FileName).ToLower() != ".png" && Path.GetExtension(file.FileName).ToLower() != ".jpg" && Path.GetExtension(file.FileName).ToLower() != ".jpeg")
          {
            ModelState.AddModelError("file", "Only .png, .jpg and .jpeg files are allowed.");
            return View(announcement);
          }

          // Check image dimensions
          using (var image = Image.FromStream(file.OpenReadStream()))
          {
            if (image.Width != 1920 || image.Height != 1080)
            {
              ModelState.AddModelError("file", "The image dimensions must be 1920px width and 1080px height.");
              return View(announcement);
            }
          }

          string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
          string productPath = Path.Combine(wwwRootPath, @"img\announcements");

          if (!string.IsNullOrEmpty(announcement.ImageUrl))
          {
            // Delete the old image
            var oldImagePath = Path.Combine(wwwRootPath, announcement.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
              System.IO.File.Delete(oldImagePath);
            }
          }

          using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
          {
            file.CopyTo(fileStream);
          }

          announcement.ImageUrl = @"\img\announcements\" + fileName;
        }

        if (announcement.AnnouncementID == 0)
        {
          _unitOfWork.Announcement.Add(announcement);
        }
        else
        {
          _unitOfWork.Announcement.Update(announcement);
        }
        _unitOfWork.Save();
        TempData["success"] = "Announcement created successfully";
        return RedirectToAction("Index");
      }
      else
      {
        return View(announcement);
      }
    }


    #region API CALLS
    [HttpGet]
    public IActionResult GetAll()
    {
      List<Announcement> AnnouncementList = _unitOfWork.Announcement.GetAll().ToList();
      return Json(new { data = AnnouncementList });
    }

    [HttpDelete]
    public IActionResult Delete(int? id)
    {
      var announcementToBeDeleted = _unitOfWork.Announcement.Get(u => u.AnnouncementID == id);

      if (announcementToBeDeleted == null)
      {
        return Json(new { success = false, message = "Error  while deleting" });
      }

      var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, announcementToBeDeleted.ImageUrl.TrimStart('\\'));

      if (System.IO.File.Exists(oldImagePath))
      {
        System.IO.File.Delete(oldImagePath);
      }

      _unitOfWork.Announcement.Remove(announcementToBeDeleted);
      _unitOfWork.Save();

      return Json(new { success = true, message = "Delete successful" });
    }
    #endregion
  }
}
