using System.Threading;

namespace Poseidon
{
    /// <summary>
    ///     The main manager for all Fiat Currency functions
    /// </summary>
    public class FiatManager
    {
        private Thread ecbThread;
        private Thread bocThread;
        private Thread fixThread;
        
        private EuropeanCentralBankManager ecbManager;
        private BankOfCanadaManager bocManager;
        private FixerManager fixManager;

        /// <summary>
        /// Constructor for Fiat Currency Manager
        /// </summary>
        /// <param name="ecbManager">European Central Bank Manager</param>
        /// <param name="bocManager">Bank of Canada Manager</param>
        /// <param name="fixManager">Fixer Manager</param>
        public FiatManager(EuropeanCentralBankManager ecbManager, BankOfCanadaManager bocManager, FixerManager fixManager)
        {
            this.ecbManager = ecbManager;
            this.bocManager = bocManager;
            this.fixManager = fixManager;
            
            ecbThread = new Thread(UpdateECBData);
            
            bocThread = new Thread(UpdateBOCData);
            
            fixThread = new Thread(UpdateFixData);
            
        }

        /// <summary>
        /// Starts fiat data collection threads
        /// </summary>
        public void StartThreads()
        {
            ecbThread.Start();
            bocThread.Start();
            fixThread.Start();
        }

        /// <summary>
        /// Aborts fiat data collection threads
        /// </summary>
        public void StopThreads()
        {
            ecbThread.Abort();
            bocThread.Abort();
            fixThread.Abort();
        }

        /// <summary>
        /// Updates European Central Bank Data
        /// </summary>
        private void UpdateECBData()
        {
            while (true)
            {
                ecbManager.GetFiatRates();
                Thread.Sleep(Globals.WAIT_ONE_DAY);
            }
        }

        /// <summary>
        /// Updates Bank of Canada Data
        /// </summary>
        private void UpdateBOCData()
        {
            while (true)
            {
                bocManager.GetFiatRates();
                Thread.Sleep(Globals.WAIT_ONE_DAY);
            }
        }

        /// <summary>
        /// Updates FIXER API Data
        /// </summary>
        private void UpdateFixData()
        {
            while (true)
            {
                fixManager.GetFiatRates();
                Thread.Sleep(Globals.WAIT_ONE_HOUR);
            }
        }
    }
}