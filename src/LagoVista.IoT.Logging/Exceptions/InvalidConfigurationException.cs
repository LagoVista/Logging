using LagoVista.Core.Validation;
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

        public InvalidConfigurationException(ErrorCode errCode) : this(errCode, null)
        {

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
            Error.ConfigurationType = configurationType.FullName;
            Error.Details = details;
        }

        public Error Error { get; private set; }

        public static InvalidConfigurationException FromErrorCode(ErrorCode errorCode)
        {
            return new InvalidConfigurationException(errorCode);
        }

        public static InvalidConfigurationException FromErrorCode(ErrorCode errorCode, string details)
        {
            return new InvalidConfigurationException(errorCode, details);
        }

        public InvokeResult ToFailedInvocation()
        {
            var result = new InvokeResult();
            var errMessage = new ErrorMessage(Error.ErrorCode, Error.Message);
            result.Errors.Add(errMessage);
            return result;
        }
    }
}
