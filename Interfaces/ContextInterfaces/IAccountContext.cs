using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.ContextInterfaces
{
    public interface IAccountContext
    {
        Account LoginUser(Account account);
        void CreateNew(Account account);
        List<Review> GetUserReviews(Account account);
    }
}
