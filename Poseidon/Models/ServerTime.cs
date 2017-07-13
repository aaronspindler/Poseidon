namespace Poseidon.Models
{
    public class ServerTime
    {
        public object[] error { get; set; }
        public Result result { get; set; }

        public class Result
        {
            public int unixtime { get; set; }
            public string rfc1123 { get; set; }
        }
    }
}