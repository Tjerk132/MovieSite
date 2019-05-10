using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public string Autor { get; set; }
    }
}
