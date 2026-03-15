using LagoVista.Core.Validation;
using LagoVista.IoT.Logging.Loggers;
using LagoVista.IoT.Logging.Models;
using LagoVista.IoT.Logging.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LagoVista.IoT.Logging
{
    public class ConsoleLogger : LoggerBase, IAdminLogger
    {
        public ConsoleLogger() : base(new ConsoleLogWriter())
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

            await InsertErrorAsync(logRecord);
        }

        public async void LogInvokeResult(string tag, InvokeResult result, params KeyValuePair<string, string>[] args)
        {
            try
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

                    if (result.Errors.Count > 1)
                    {
                        logRecord.Details = JsonConvert.SerializeObject(result.Errors);
                    }
                    else
                    {
                        logRecord.Details = firstError.Details;
                    }
                }

                logRecord.AddKVPs(args);
                if (result.Successful)
                {
                    await InsertEventAsync(logRecord);
                }
                else
                {
                    await InsertErrorAsync(logRecord);
                }
            }
            catch (Exception ex)
            {
                AddException(tag, ex);
            }
        }

        public void LogInvokeResult<TResultType>(string tag, InvokeResult<TResultType> result, params KeyValuePair<string, string>[] args)
        {
            LogInvokeResult(tag, result.ToInvokeResult(), args);
        }

        protected override void SetRecordIdentifiers(LogRecord log)
        {
                log.HostId = "-consolewriter-";
        }
    }
}
