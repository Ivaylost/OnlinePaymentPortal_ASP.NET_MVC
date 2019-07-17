using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlinePaymentPortal.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = "admin")]
    public class ErrorHandlerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}