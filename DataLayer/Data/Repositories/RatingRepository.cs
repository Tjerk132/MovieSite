using Interfaces.Interfaces;
using DataLayer.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data
{
    public class RatingRepository
    {
        private readonly IRatingContext _context;
        public RatingRepository(IRatingContext context)
        {
            _context = context;
        }
        public string SubmitRating(int RatingNumber, int MovieId, Account account)
        {
            return _context.SubmitRating(RatingNumber, MovieId, account);
        }
    }
}
