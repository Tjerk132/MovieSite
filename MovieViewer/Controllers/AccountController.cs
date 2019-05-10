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

namespace MovieSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountContext _context;
        public AccountController(IAccountContext accountcontext)
        {
            _context = accountcontext;
        }

        public ActionResult Index()
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
        public ActionResult CreateNew()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        } 
        public ActionResult LogoutUser()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Login(Account account)
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
                return View(viewModel);
            }
            else
            {
                HttpContext.Session.SetObject("User", account);
                return RedirectToAction("Index", "Movies");
            }
        }
        [HttpPost]
        public ActionResult CreateNew(Account account)
        {
            var AccountLogic = new AccountLogic(_context);
            AccountLogic.CreateNew(account);
            HttpContext.Session.SetObject("User", account);
            return RedirectToAction("Index");
        }
    }
}