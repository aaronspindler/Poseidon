using System.Collections.Generic;

namespace Poseidon.Models.FiatCurrency.Fixer
{
    public class FixerResponse
    {
        public bool success { get; set; }
        public Error error { get; set; }
        public string baseCurrency { get; set; }
        public string date { get; set; }
        public Dictionary<string, double> rates { get; set; }
        public Dictionary<string, double> rebasedRates { get; set; }
    }

    public class Error
    {
        public int code { get; set; }
        public string type { get; set; }
        public string info { get; set; }
    }
}