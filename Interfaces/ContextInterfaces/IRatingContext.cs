using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interfaces.ContextInterfaces
{
    public interface IRatingContext
    {
        string SubmitRating(int Rating, int MovieId, Account account); 
    }
}
