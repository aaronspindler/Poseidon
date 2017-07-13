using System;
using System.Runtime.Serialization;
using Poseidon.Models.Old;

namespace Poseidon
{
    [Serializable]
    public class KrakenException : Exception
    {
        public KrakenException()
        {
        }

        public KrakenException(string message) : base(message)
        {
        }

        public KrakenException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public KrakenException(string message, ResponseBase response) : base(message)
        {
            Response = response;
        }

        protected KrakenException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ResponseBase Response { get; }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
                throw new ArgumentNullException("info");

            info.AddValue("Response", Response);
            base.GetObjectData(info, context);
        }
    }
}