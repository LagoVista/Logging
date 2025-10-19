// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: 4b980e978259c3a771557c30ff9cec5a8657a4e65d0b1274b9351a57b36358a2
// IndexVersion: 0
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
				new KeyValuePair<string, string>("Message", ex.Message),
				new KeyValuePair<string, string>("StackTrace", ex.StackTrace),
				new KeyValuePair<string, string>("Source", ex.Source),
				new KeyValuePair<string, string>("InnerException", ex.InnerException != null ? JsonConvert.SerializeObject(ex.InnerException) :  "[]")
			};

			return Tuple.Create(errorCode, pairs);
		}
	}
}
