namespace Poseidon.Misc
{
    /// <summary>
    ///     Class for storing globals used throught the program
    /// </summary>
    public static class Globals
    {
        /// <summary>
        ///     Amount of milliseconds in 1 day
        /// </summary>
        public static int WAIT_ONE_DAY = 86400000; // 1 Day

        /// <summary>
        ///     Amount of milliseconds in 1 hour
        /// </summary>
        public static int WAIT_ONE_HOUR = 3600000;

        /// <summary>
        ///     Sleep time for the crypto data collection thread
        /// </summary>
        public static int CRYPTO_DATA_COLLECTION_RATE = 5;

        /// <summary>
        ///     Sleep time for the network polling thread
        /// </summary>
        public static int NETWORK_POLL_RATE = 2500;
    }
}