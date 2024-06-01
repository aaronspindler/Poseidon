namespace Poseidon.Models.Kraken
{
    public class Ticker
    {
        public Ticker()
        {
            ask = new Ask();
            bid = new Bid();
            lastClosed = new LastClosed();
            volume = new Volume();
            volumeWeightedByPrice = new VolumeWeightedByPrice();
            numTrades = new NumTrades();
            low = new Low();
            high = new High();
        }

        public Ask ask { get; set; }
        public Bid bid { get; set; }
        public LastClosed lastClosed { get; set; }
        public Volume volume { get; set; }
        public VolumeWeightedByPrice volumeWeightedByPrice { get; set; }
        public NumTrades numTrades { get; set; }
        public Low low { get; set; }
        public High high { get; set; }

        public decimal opening { get; set; }

        public class Ask
        {
            public decimal price { get; set; }
            public decimal wholeVolume { get; set; }
            public decimal volume { get; set; }
        }

        public class Bid
        {
            public decimal price { get; set; }
            public decimal wholeVolume { get; set; }
            public decimal volume { get; set; }
        }

        public class LastClosed
        {
            public decimal price { get; set; }
            public decimal volume { get; set; }
        }

        public class Volume
        {
            public decimal today { get; set; }
            public decimal last24hours { get; set; }
        }

        public class VolumeWeightedByPrice
        {
            public decimal today { get; set; }
            public decimal last24hours { get; set; }
        }

        public class NumTrades
        {
            public decimal today { get; set; }
            public decimal last24hours { get; set; }
        }

        public class Low
        {
            public decimal today { get; set; }
            public decimal last24hours { get; set; }
        }

        public class High
        {
            public decimal today { get; set; }
            public decimal last24hours { get; set; }
        }
    }
}