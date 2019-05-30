using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DataLayer.Data;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using MovieSite.Models.ViewModels.AccountViewModels;
using Interfaces.Interfaces;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Routing;
using LogicLayer;
using MovieViewer;

namespace MovieSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountLogic _context;
        public AccountController(AccountLogic accountcontext)
        {
            _context = accountcontext;
        }

        public IActionResult Index()
        {
            AccountIndexViewModel viewModel = new AccountIndexViewModel();
            if (HttpContext.Session.GetObject<Account>("User") != null)
            {
                viewModel.Account = HttpContext.Session.GetObject<Account>("User");            
            }
            return View(viewModel);
        }
        public IActionResult CreateNew()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Details()
        {
            DetailsViewModel viewModel = new DetailsViewModel
            {
                Account = HttpContext.Session.GetObject<Account>("User"),
            };
            viewModel.Reviews = _context.GetUserReviews(viewModel.Account);
            return View(viewModel);
        }
        public IActionResult LogoutUser()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult LoginUser(Account account)
        {
            account = _context.LoginUser(account);

            LoginViewModel viewmodel = new LoginViewModel()
            {
                Account = account
            };
            if (account.Name == null)
            {
                viewmodel.Message = "Wrong account information";
                return View("Login", viewmodel);
            }
            else
            {
                HttpContext.Session.SetObject("User", account);
                return RedirectToAction("Index", "Movies");
            }
        }
        [HttpPost]
        public IActionResult CreateNew(Account account)
        {
            _context.CreateNew(account);
            HttpContext.Session.SetObject("User", account);
            return RedirectToAction("Index"/*, new RouteValueDictionary(account)*/);
        }
    }
}