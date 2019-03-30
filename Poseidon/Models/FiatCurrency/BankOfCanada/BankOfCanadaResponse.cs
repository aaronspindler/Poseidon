using System.Collections.Generic;

namespace Poseidon.Models.FiatCurrency.BankOfCanada
{
    public class BankOfCanadaResponse
    {
        public BankOfCanadaResponse()
        {
            _entries = new List<BankOfCanadaEntry>();
        }

        private List<BankOfCanadaEntry> _entries { get; }

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