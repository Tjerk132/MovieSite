using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Interfaces.LogicInterfaces;
using LogicLayer.Logic;
using MovieViewer;
using MovieSite.ViewModels.AccountViewModels;
using Interfaces.ContextInterfaces;

namespace MovieSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserSession _userSession;
        private readonly IAccountLogic _logic;
        public AccountController(IAccountLogic logic, IUserSession userSession)
        {
            _userSession = userSession;
            _logic = logic;
        }

        public IActionResult Index()
        {
            AccountIndexViewModel viewModel = new AccountIndexViewModel();
            if (_userSession.GetSession != null)
            {
                viewModel.Account = _userSession.GetSession;            
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
                Account = _userSession.GetSession,
            };
            viewModel.Reviews = _logic.GetUserReviews(viewModel.Account);
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
            account = _logic.LoginUser(account);

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
                _userSession.SetSession(account);
                return RedirectToAction("Index", "Movies");
            }
        }
        [HttpPost]
        public IActionResult CreateNew(Account account)
        {
            _logic.CreateNew(account);
            _userSession.SetSession(account);
            return RedirectToAction("Index"/*, new RouteValueDictionary(account)*/);
        }
    }
}