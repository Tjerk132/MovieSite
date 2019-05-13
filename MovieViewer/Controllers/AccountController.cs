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
using MovieSite.Models.ViewModels;
using Interfaces.Interfaces;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Routing;

namespace MovieSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountContext _context;
        public AccountController(IAccountContext accountcontext)
        {
            _context = accountcontext;
        }

        public IActionResult Index()
        {
            SharedIndexViewModel viewModel = new SharedIndexViewModel();
            
            if (HttpContext.Session.GetObject<Account>("User") != null)
            {
                viewModel.AccountViewModel = new AccountViewModel()
                {
                    Account = HttpContext.Session.GetObject<Account>("User")
                };
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
            SharedIndexViewModel viewModel = new SharedIndexViewModel
            {
                AccountViewModel = new AccountViewModel()
                {
                    Account = HttpContext.Session.GetObject<Account>("User")
                }
            };
            return View(viewModel);
        }
        public IActionResult LogoutUser()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        public IActionResult LoginUser(Account account)
        {
            var AccountLogic = new AccountLogic(_context);
            account = AccountLogic.LoginUser(account);
            SharedIndexViewModel viewModel = new SharedIndexViewModel
            {
                AccountViewModel = new AccountViewModel()
                {
                    Account = account
                }
            };
            if (account.Name == null)
            {
                viewModel.AccountViewModel.Message = "Wrong account information";
                return View("Login",viewModel);
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
            var AccountLogic = new AccountLogic(_context);
            AccountLogic.CreateNew(account);
            return RedirectToAction("LoginUser", new RouteValueDictionary(account));
        }
    }
}