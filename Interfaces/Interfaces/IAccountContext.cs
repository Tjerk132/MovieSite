﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Interfaces
{
    public interface IAccountContext
    {
        Account LoginResult(Account Account);
        void CreateNew(Account Account);
    }

}