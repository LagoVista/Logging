// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: 4b980e978259c3a771557c30ff9cec5a8657a4e65d0b1274b9351a57b36358a2
// IndexVersion: 2
// --- END CODE INDEX META ---
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace LagoVista
{
    public static class ExceptionExtensions
    {
		public static Tuple<IoT.Logging.ErrorCode, KeyValuePair<string, string>[]> GenerateLoggingParts(this Exception ex, string code, string message)
		{
			var errorCode = new IoT.Logging.ErrorCode() { Code = code, Message = message };
			var pairs = new KeyValuePair<string, string>[]
			{
				new KeyValuePair<string, string>("message", ex.Message),
				new KeyValuePair<string, string>("stackTrace", ex.StackTrace),
				new KeyValuePair<string, string>("source", ex.Source),
			};

			if(ex.InnerException != null)
			{
				new KeyValuePair<string, string>("innerException", ex.InnerException.Message);
                new KeyValuePair<string, string>("innerExceptionStackTrace", ex.InnerException.StackTrace);
                new KeyValuePair<string, string>("innerExceptionSource", ex.InnerException.Source);
            }

            return Tuple.Create(errorCode, pairs);
		}
	}
}
