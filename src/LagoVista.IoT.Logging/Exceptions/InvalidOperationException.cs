// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: 304277222b832a46145236086716aa82740498e220e073b6a65e586166fec5ec
// IndexVersion: 0
// --- END CODE INDEX META ---
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
