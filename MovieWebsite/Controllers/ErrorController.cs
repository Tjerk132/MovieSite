using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieSite.ViewModels.ErrorViewModels;
using LogicLayer.Logic;
using Helpers;

namespace MovieViewer.Controllers
{
    public class ErrorController : Controller
    {
        [AllowAnonymous]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                StatusCodeBuilder builder = new StatusCodeBuilder();
                ErrorViewModel viewModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    StatusCode = statusCode,
                    StatusCodeTitle = builder.GetStatusCodeTitle(statusCode),
                    StatusCodeInfo = builder.GetStatusCodeInfo(statusCode)
                };
                return View(viewModel);
            }
            else return View();
        }
    }
}