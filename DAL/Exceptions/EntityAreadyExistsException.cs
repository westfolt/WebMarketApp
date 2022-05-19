using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DAL.Exceptions
{
    [Serializable]
    public class EntityAreadyExistsException : Exception
    {
        private readonly string _parameterCausedException;
        public string ParameterCausedException => _parameterCausedException;

        public EntityAreadyExistsException(string message) : base(message)
        { }
        public EntityAreadyExistsException(string message, string parameterName) : base(message)
        {
            _parameterCausedException = parameterName;
        }
        protected EntityAreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
