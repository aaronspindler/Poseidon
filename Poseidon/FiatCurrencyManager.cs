namespace Poseidon
{
    /// <summary>
    ///     The main manager for all Fiat Currency functions
    /// </summary>
    public class FiatCurrencyManager
    {
        private EuropeanCentralBankManager ecbManager;
        private BankOfCanadaManager bocManager;
        private FixerManager fixManager;

        /// <summary>
        /// Constructor for Fiat Currency Manager
        /// </summary>
        /// <param name="ecbManager">European Central Bank Manager</param>
        /// <param name="bocManager">Bank of Canada Manager</param>
        /// <param name="fixManager">Fixer Manager</param>
        public FiatCurrencyManager(EuropeanCentralBankManager ecbManager, BankOfCanadaManager bocManager, FixerManager fixManager)
        {
            this.ecbManager = ecbManager;
            this.bocManager = bocManager;
            this.fixManager = fixManager;
        }
    }
}