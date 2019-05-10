using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MovieSite.Models.ViewModels
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }

        public string Title { get; set; }
 
        public int Watched { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int Rating { get; set; }
    }
}