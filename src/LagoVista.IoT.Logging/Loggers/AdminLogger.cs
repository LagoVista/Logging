using LagoVista.Core.Validation;
using LagoVista.IoT.Logging.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LagoVista.IoT.Logging.Loggers
{
    public class AdminLogger : LoggerBase, IAdminLogger
    {
        public AdminLogger(ILogWriter writer) : base(writer)
        {

        }

        public async void AddError(ErrorCode errorCode, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord()
            {
                ErrorCode = errorCode.Code,
                Message = errorCode.Message,
            };

            logRecord.AddKVPs(args);

            await InsertEventAsync(logRecord);
        }

        public async void LogInvokeResult(string tag, InvokeResult result, params KeyValuePair<string, string>[] args)
        {
            var logRecord = new LogRecord()
            {
                Tag = tag,
            };

            var firstError = result.Errors.FirstOrDefault();
            if (firstError != null)
            {
                logRecord.ErrorCode = firstError.ErrorCode;
                logRecord.Message = firstError.Message;
            }

            if (result.Errors.Count > 1)
            {
                logRecord.Details = JsonConvert.SerializeObject(result.Errors);
            }
            else
            {
                logRecord.Details = firstError.Details;
            }


            logRecord.AddKVPs(args);
            await InsertEventAsync(logRecord);
        }

        public void LogInvokeResult<TResultType>(string tag, InvokeResult<TResultType> result, params KeyValuePair<string, string>[] args)
        {
            LogInvokeResult(tag, result.ToInvokeResult(), args);
        }

        protected override void SetRecordIdentifiers(LogRecord log) { }
    }
}
