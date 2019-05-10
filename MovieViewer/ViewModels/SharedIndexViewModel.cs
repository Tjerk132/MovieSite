using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieSite.Models.ViewModels
{
    public class SharedIndexViewModel
    {
        public AccountViewModel AccountViewModel { get; set; }
        public List<Movie> Movies{ get; set; }

    }

}