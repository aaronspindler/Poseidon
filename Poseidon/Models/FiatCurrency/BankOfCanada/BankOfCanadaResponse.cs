using System.Collections.Generic;

namespace Poseidon.Models.FiatCurrency.BankOfCanada
{
    public class BankOfCanadaResponse
    {
        private List<BankOfCanadaEntry> _entries { get; set; }

        public BankOfCanadaResponse()
        {
            _entries = new List<BankOfCanadaEntry>();
        }

        public List<BankOfCanadaEntry> GetEntries()
        {
            return _entries;
        }

        public void AddEntry(BankOfCanadaEntry entry)
        {
            _entries.Add(entry);
        }
    }
}