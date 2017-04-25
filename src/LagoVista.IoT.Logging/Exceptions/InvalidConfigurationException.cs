using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException(Error errorCode)
        {
            Error = errorCode;
        }

        public InvalidConfigurationException(Error errorCode, string details)
        {
            Error = errorCode;
            Error.Details = details;
        }

        public InvalidConfigurationException(ErrorCode errorCode, string details)
        {
            Error = errorCode.ToError();
            Error.Details = details;
        }

        public InvalidConfigurationException(ErrorCode errorCode, Type configurationType, string id, string details = "")
        {
            Error = errorCode.ToError();
            Error.ConfigurationId = id;
            Error.ConfiguratinType = configurationType.FullName;
            Error.Details = details;
        }

        public Error Error { get; private set; }
    }
}
