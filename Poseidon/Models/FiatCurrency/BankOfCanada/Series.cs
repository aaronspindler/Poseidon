namespace Poseidon.Models.FiatCurrency.BankOfCanada
{
    public class Series
    {
        private readonly string _description;
        private readonly string _id;
        private readonly string _label;

        public Series(string id, string label, string description)
        {
            _id = id;
            _label = label;
            _description = description;
        }

        public string GetID()
        {
            return _id;
        }

        public string GetLabel()
        {
            return _label;
        }

        public string GetDescription()
        {
            return _description;
        }
    }
}