using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Movie
    {
        public int MovieId { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int Watched { get; set; }
        public int Rating { get; set; }
        public Movie (int movieid, string title, DateTime releasedate, int watched, int rating)
        {
            MovieId = movieid;
            Title = title;
            ReleaseDate = releasedate;
            Watched = watched;
            Rating = rating;
        }
        public Movie(string title, DateTime releasedate)
        {
            Title = title;
            ReleaseDate = releasedate;
        }
    }
}
