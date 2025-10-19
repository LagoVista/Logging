// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: eba7d27c259a2e6e8be285745479e0c3010300b8b8fcf13f9ba41a3e5595c0ee
// IndexVersion: 0
// --- END CODE INDEX META ---
/*1/12/2024 8:11:11 AM*/
using System.Globalization;
using System.Reflection;

//Resources:LoggingResources:LogRecord_Description
namespace LagoVista.IoT.Logging.Resources
{
	public class LoggingResources
	{
        private static global::System.Resources.ResourceManager _resourceManager;
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private static global::System.Resources.ResourceManager ResourceManager 
		{
            get 
			{
                if (object.ReferenceEquals(_resourceManager, null)) 
				{
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("LagoVista.IoT.Logging.Resources.LoggingResources", typeof(LoggingResources).GetTypeInfo().Assembly);
                    _resourceManager = temp;
                }
                return _resourceManager;
            }
        }
        
        /// <summary>
        ///   Returns the formatted resource string.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private static string GetResourceString(string key, params string[] tokens)
		{
			var culture = CultureInfo.CurrentCulture;;
            var str = ResourceManager.GetString(key, culture);

			for(int i = 0; i < tokens.Length; i += 2)
				str = str.Replace(tokens[i], tokens[i+1]);
										
            return str;
        }
        
        /// <summary>
        ///   Returns the formatted resource string.
        /// </summary>
		/*
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private static HtmlString GetResourceHtmlString(string key, params string[] tokens)
		{
			var str = GetResourceString(key, tokens);
							
			if(str.StartsWith("HTML:"))
				str = str.Substring(5);

			return new HtmlString(str);
        }*/
		
		public static string LogRecord_Description { get { return GetResourceString("LogRecord_Description"); } }
//Resources:LoggingResources:LogRecords_Title

		public static string LogRecords_Title { get { return GetResourceString("LogRecords_Title"); } }

		public static class Names
		{
			public const string LogRecord_Description = "LogRecord_Description";
			public const string LogRecords_Title = "LogRecords_Title";
		}
	}
}

