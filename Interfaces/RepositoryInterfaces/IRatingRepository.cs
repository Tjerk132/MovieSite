using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace Interfaces.RepositoryInterfaces
{
    public interface IRatingRepository
    {
        string SubmitRating(int Rating, int MovieId, Account account); 
    }
}
