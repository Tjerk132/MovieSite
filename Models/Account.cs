using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Enumeration;

namespace Models
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Priority Priority { get; set; }
        public int Watched { get; set; }
        public string passwordhash { get; set; }
    }
}
