using MovieSite.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace MovieSite.Models.ViewModels
{
    public class ReviewViewModel
    {
        public List<Review> Reviews { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; }
    }
}
