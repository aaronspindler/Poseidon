using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poseidon.Models
{
    public class RecentTrade
    {
        public decimal price { get; set; }
        public decimal volume { get; set; }
        public decimal time { get; set; }
        public char buysell { get; set; }
        public char marketlimit { get; set; }
        public string misc { get; set; }
    }
}
