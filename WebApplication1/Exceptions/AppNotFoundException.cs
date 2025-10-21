using System.Runtime.Serialization;

namespace WebApplication1.Exceptions
{
    [Serializable]
    public class AppNotFoundException : Exception
    {
        public AppNotFoundException(string message) : base(message) { }
        protected AppNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
