namespace Poseidon.Models
{
    public class Trade
    {
        public decimal price { get; set; }
        public decimal volume { get; set; }
        public decimal time { get; set; }
        public char buysell { get; set; }
        public char marketlimit { get; set; }
        public string misc { get; set; }
    }
}