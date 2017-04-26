
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace LagoVista.IoT.Logging
{

    public static class ErrorCodes
    {
        static Dictionary<string, ErrorCode> _errorCodes = new Dictionary<string, ErrorCode>();

        public static void Register(Type errorCodeType)
        {

            foreach(var prop in errorCodeType.GetRuntimeProperties())
            {
                if(prop.PropertyType == typeof(ErrorCode))
                {
                    var errorCode = (prop.GetValue(null) as ErrorCode);
                    _errorCodes.Add(errorCode.Code, errorCode);
                }
            }
        }

        public static Dictionary<string, ErrorCode> Codes { get { return _errorCodes; } }
    }
}