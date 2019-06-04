using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.LogicInterfaces
{
    public interface IRatingLogic
    {
        string SubmitRating(int RatingNumber, int MovieId, Account account);
    }
}
