using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using DataLayer.Data;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;
using Models;
using Interfaces.Interfaces;

namespace LogicLayer.Logic
{
    public class RatingLogic
    {
        public RatingLogic(IRatingContext context)
        {
            Repository = new RatingRepository(context);
        }
        private RatingRepository Repository { get; }

        public string SubmitRating(int RatingNumber, int MovieId, Account account)
        {
            return Repository.SubmitRating(RatingNumber, MovieId, account);
        }

    }
}