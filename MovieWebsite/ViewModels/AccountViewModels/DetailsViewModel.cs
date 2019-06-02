using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovieSite.ViewModels.AccountViewModels
{
    public class DetailsViewModel
    {
        public Account Account { get; set; }
        public List<Review> Reviews { get; set; }
    }
}