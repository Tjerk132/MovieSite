using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieSite.Models.ViewModels;
using LogicLayer.Logic;

namespace MovieViewer.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                ErrorLogic logic = new ErrorLogic();
                ErrorViewModel viewModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    StatusCode = statusCode,
                    StatusCodeTitle = logic.GetStatusCodeTitle(statusCode),
                    StatusCodeInfo = logic.GetStatusCodeInfo(statusCode)
                };
                return View(viewModel);
            }
            else return View();
        }
    }
}