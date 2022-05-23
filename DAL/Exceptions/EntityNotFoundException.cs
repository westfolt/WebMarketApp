using System;
using System.Runtime.Serialization;

namespace DAL.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        private readonly string _parameterCausedException;
        public string ParameterCausedException => _parameterCausedException;

        public EntityNotFoundException(string message) : base(message)
        { }
        public EntityNotFoundException(string message, string parameterName) : base(message)
        {
            _parameterCausedException = parameterName;
        }
        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
