using Interfaces.LogicInterfaces;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using MovieSite.ViewModels.MovieViewModels;
using MovieViewer;
using System.Threading.Tasks;

namespace MovieSite.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingLogic _ratinglogic;
        private readonly IUserSession _userSession;
        public RatingController(IRatingLogic ratinglogic, IUserSession usersession)
        {
            _ratinglogic = ratinglogic;
            _userSession = usersession;
        }
        [HttpPost]
        public IActionResult SubmitRating(int Rating, int MovieId)
        {
            string Message;
            Account account = _userSession.GetSession;
            if (Rating >= 0 && Rating <= 100)
            {
                Message = _ratinglogic.SubmitRating(Rating, MovieId, account);
            }
            else Message = "Please insert a rating between 0 and 100";

            return RedirectToAction("Index","Movies", new { Message });
        }
    }
}