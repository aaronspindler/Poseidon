namespace Poseidon.Models.FiatCurrency.Fixer
{
    public class FixerResponse
    {
        public bool success { get; set; }
        public Error error { get; set; }
        
        public FixerEntry entry { get; set; }
    }
    
    public class Error
    {
        public int code { get; set; }
        public string type { get; set; }
        public string info { get; set; }
    }
}