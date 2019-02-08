namespace Poseidon.Models.FiatCurrency.BankOfCanada
{
    public class Series
    {
        private string _id;
        private string _label;
        private string _description;

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