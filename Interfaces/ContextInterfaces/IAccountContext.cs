﻿using Models;
using System.Collections.Generic;

namespace Interfaces.ContextInterfaces
{
    public interface IAccountContext
    {
        Account LoginUser(Account account);
        void CreateNew(Account account);
        List<Review> GetUserReviews(Account account);
    }
}
