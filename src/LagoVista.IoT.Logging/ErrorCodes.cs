// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: d3ff2e0b72531e3f8b02f99a35f7d7ec07373ff8f17d254386782ed9918d14e9
// IndexVersion: 0
// --- END CODE INDEX META ---
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
			foreach (var prop in errorCodeType.GetRuntimeProperties())
			{
				if (prop.PropertyType == typeof(ErrorCode))
				{
					var errorCode = prop.GetValue(null) as ErrorCode;
					if (!_errorCodes.ContainsKey(errorCode.Code))
						_errorCodes.Add(errorCode.Code, errorCode);
				}
			}
		}

		public static Dictionary<string, ErrorCode> Codes { get { return _errorCodes; } }
	}
}