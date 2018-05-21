using System;
namespace Poseidon.Models.FiatCurrency
{
    public class Currency
    {
        public Currency(string n, double r){
            Name = n;
            Rate = r;
        }
        string Name { get; set; }
        double Rate { get; set; }
    }
}
