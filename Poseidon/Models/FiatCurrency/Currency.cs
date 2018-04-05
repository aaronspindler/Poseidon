using System;
namespace Poseidon.Models.FiatCurrency
{
    public class Currency
    {
        public Currency(string n, float r){
            Name = n;
            Rate = r;
        }
        string Name { get; set; }
        float Rate { get; set; }
    }
}
