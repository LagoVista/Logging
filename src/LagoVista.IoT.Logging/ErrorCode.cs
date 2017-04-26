using LagoVista.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging
{
    public class ErrorCode
    {
        public string Code { get; set; }
        public string Message { get; set; }

        public Error ToError()
        {
            return new Error()
            {
                ErrorCode = Code,
                Message = Message
            };
        }

        public InvokeResult ToFailedInvocation(String details = "")
        {
            var result = new InvokeResult();
            var errorMessage = new ErrorMessage(Code, Message);
            errorMessage.Details = details;
            result.Errors.Add(errorMessage);
            return result;
        }
    }
}
