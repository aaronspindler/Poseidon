using System;
namespace Poseidon.Models.FiatCurrency
{
    public class Currency
    {
        string Name { get; set; }
        float Rate { get; set; }
        float CDNConversion { get; set; }
    }
}
