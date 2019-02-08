#region

using Newtonsoft.Json;

#endregion

namespace Poseidon.Models.Old
{
    public class GetDepositMethodsResult
    {
        /// <summary>
        ///     Whether or not method has an address setup fee (optional).
        /// </summary>
        [JsonProperty(PropertyName = "address-setup-fee")]
        public bool? AddressSetupFee;

        /// <summary>
        ///     Amount of fees that will be paid.
        /// </summary>
        public string Fee;

        /// <summary>
        ///     Maximum net amount that can be deposited right now, or false if no limit
        /// </summary>
        public string Limit;

        /// <summary>
        ///     Name of deposit method.
        /// </summary>
        public string Method;
    }
}