using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Exceptions
{
    public class InvalidOperationException : Exception
    {
        public InvalidOperationException(Error errorCode)
        {
            Error = errorCode;
        }

        public InvalidOperationException(Error errorCode, string details)
        {
            Error = errorCode;
            Error.Details = details;
        }

        public InvalidOperationException(ErrorCode errorCode)
        {
            Error = errorCode.ToError();
        }

        public InvalidOperationException(ErrorCode errorCode, string details)
        {
            Error = errorCode.ToError();
            Error.Details = details;
        }

        public InvalidOperationException(ErrorCode errorCode, Type configurationType, string id, string details = "")
        {
            Error = errorCode.ToError();
            Error.ConfigurationId = id;
            Error.ConfigurationType = configurationType.FullName;
            Error.Details = details;
        }

        public Error Error { get; private set; }

        public static InvalidOperationException FromErrorCode(ErrorCode errorCode)
        {
            return new InvalidOperationException(errorCode);
        }

        public static InvalidOperationException FromErrorCode(ErrorCode errorCode, string details)
        {
            return new InvalidOperationException(errorCode, details);
        }
    }
}
