using System.Runtime.Serialization;

namespace DomainName.Utility.Exception
{
    [Serializable]
    public sealed class FailedRequestException : System.Exception
    {
        public FailedRequestException()
        {
        }

        public FailedRequestException(string message) : base(message)
        {
        }

        public FailedRequestException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        private FailedRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}