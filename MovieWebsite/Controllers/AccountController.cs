using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MovieSite.ViewModels.AccountViewModels;
using Interfaces.Interfaces;
using LogicLayer.Logic;
using MovieViewer;

namespace MovieSite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountContext _context;
        private readonly IUserSession _userSession;
        private AccountLogic Logic;
        public AccountController(IAccountContext accountcontext, IUserSession userSession)
        {
            _context = accountcontext;
            _userSession = userSession;
            Logic = new AccountLogic(_context);
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
            viewModel.Reviews = Logic.GetUserReviews(viewModel.Account);
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
            account = Logic.LoginUser(account);

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
            _context.CreateNew(account);
            _userSession.SetSession(account);
            return RedirectToAction("Index"/*, new RouteValueDictionary(account)*/);
        }
    }
}