using System;
using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    [Serializable]
    public class WebMarketException : Exception
    {
        private readonly string _parameterCausedException;
        public string ParameterCausedException => _parameterCausedException;

        public WebMarketException(string message) : base(message)
        { }
        public WebMarketException(string message, string parameterName) : base(message)
        {
            _parameterCausedException = parameterName;
        }
        protected WebMarketException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
