#region

using System;

#endregion

namespace Poseidon.Models.Kraken
{
    public class CurrencyLog
    {
        public static string ID { get; set; }
        public static string Name { get; set; }
        public static decimal Price { get; set; }
        public static DateTime Time { get; set; }
    }
}