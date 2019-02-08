namespace Poseidon.Models.FiatCurrency
{
    /// <summary>
    ///     A data model for each fiat currency. All currencies will base based on CAD
    /// </summary>
    public class FiatCurrency
    {
        private string _name;
        private double _price;

        public FiatCurrency(string name, double price)
        {
            _name = name;
            _price = price;
        }
    }
}