// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: fb376248be9aa4c3a4d586736a5ca2a8e5f04bac3ae47e7035e76779dc6d82dd
// IndexVersion: 0
// --- END CODE INDEX META ---
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

        public ErrorMessage ToErrorMessage(String details = "")
        {
            var result = new InvokeResult();
            var errorMessage = new ErrorMessage(Code, Message);
            errorMessage.Details = details;
            return errorMessage;
        }

        public InvokeResult ToFailedInvocation(String details = "")
        {
            var result = new InvokeResult();
            var errorMessage = new ErrorMessage(Code, Message);
            errorMessage.Details = details;
            result.Errors.Add(errorMessage);
            return result;
        }

        public InvokeResult<T> ToFailedInvocation<T>(String details = "")
        {
            var result = new InvokeResult<T>();
            var errorMessage = new ErrorMessage(Code, Message);
            errorMessage.Details = details;
            result.Errors.Add(errorMessage);
            return result;
        }
    }
}
