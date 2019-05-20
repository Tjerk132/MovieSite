using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieSite.Models.ViewModels
{
    public class AccountDetailsViewModel
    {
        public List<Review> Reviews { get; set; }
        public Account Account { get; set; }
    }
}
