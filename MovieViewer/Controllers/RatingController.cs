using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces.Interfaces;
using DataLayer.Data;
using LogicLayer.Logic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MovieSite.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRatingContext _context;
        public RatingController(IRatingContext ratingcontext)
        {
            _context = ratingcontext;
        }
        [HttpPost]
        public ActionResult SubmitRating(int Rating, int MovieId)
        {
            if (Rating >= 0 && Rating <= 100)
            {
                var RatingLogic = new RatingLogic(_context);
                Account account = HttpContext.Session.GetObject<Account>("User");
                string Message = RatingLogic.SubmitRating(Rating, MovieId, account);
                HttpContext.Session.SetString("Message", Message);
            }
            else HttpContext.Session.SetString("Message", "Please insert a rating between 0 and 100");

            return RedirectToAction("Index","Movies");
        }
    }
}