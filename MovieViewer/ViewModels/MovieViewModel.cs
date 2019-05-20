﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieSite.Models.ViewModels
{
    public class MovieViewModel
    {
        public Account Account { get; set; }
        public string Message { get; set; }
        public List<Movie> Movies{ get; set; }

    }

}