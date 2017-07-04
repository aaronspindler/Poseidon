using Newtonsoft.Json;

namespace Poseidon.Models
{
    public class GetDepositMethodsResult
    {
        /// <summary>
        /// Name of deposit method.
        /// </summary>
        public string Method;

        /// <summary>
        /// Maximum net amount that can be deposited right now, or false if no limit
        /// </summary>
        public string Limit;

        /// <summary>
        /// Amount of fees that will be paid.
        /// </summary>
        public string Fee;

        /// <summary>
        /// Whether or not method has an address setup fee (optional).
        /// </summary>
        [JsonProperty(PropertyName = "address-setup-fee")]
        public bool? AddressSetupFee;
    }
}