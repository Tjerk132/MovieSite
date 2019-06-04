using Interfaces.LogicInterfaces;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using MovieSite.ViewModels.MovieViewModels;
using MovieViewer;

namespace MovieSite.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingLogic _ratinglogic;
        private readonly IMoviesLogic _movieslogic;
        private readonly IUserSession _userSession;
        public RatingController(IRatingLogic ratinglogic, IMoviesLogic movielogic, IUserSession usersession)
        {
            _ratinglogic = ratinglogic;
            _movieslogic = movielogic;
            _userSession = usersession;
        }
        [HttpPost]
        public ActionResult SubmitRating(int Rating, int MovieId)
        {
            string Message;
            Account account = _userSession.GetSession;
            if (Rating >= 0 && Rating <= 100)
            {
                Message = _ratinglogic.SubmitRating(Rating, MovieId, account);
            }
            else Message = "Please insert a rating between 0 and 100";

            MovieIndexViewModel viewModel = new MovieIndexViewModel
            {
                Account = account,
                Movies = _movieslogic.GetMovies()
            };
            viewModel.Message = Message;

            return RedirectToAction("Views/Movies/Index.cshtml", viewModel);
        }
    }
}