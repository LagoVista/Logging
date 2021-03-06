﻿using LagoVista.Core.PlatformSupport;
using LagoVista.Core.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public interface IAdminLogger : ILogger
    {

        void AddError(string tag, string message, params KeyValuePair<string, string>[] args);

        void AddError(ErrorCode errorCode, params KeyValuePair<string, string>[] args);

        void AddConfigurationError(string tag, string message, params KeyValuePair<string, string>[] args);

        void AddMetric(string measure, double duration);

        void AddMetric(string measure, int count);

        void LogInvokeResult(string tag, InvokeResult result, params KeyValuePair<string, string>[] args);
        void LogInvokeResult<TResultType>(string tag, InvokeResult<TResultType> result, params KeyValuePair<string, string>[] args);
    }
}
