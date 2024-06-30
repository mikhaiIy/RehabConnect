using Microsoft.AspNetCore.Mvc;
using RehabConnect.Utility;
using RehabConnect.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using RehabConnect.ViewModels;
using RehabConnect.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace RehabConnectWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
  [Authorize(Roles = SD.Role_Admin)]
  public class SessionController : Controller
    {
        
    }
}

