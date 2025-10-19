// --- BEGIN CODE INDEX META (do not edit) ---
// ContentHash: b2905f1c5c4199f0bdf1cfed0408e1fc1ac653ac8877b1f2e4fd277fe5356d27
// IndexVersion: 0
// --- END CODE INDEX META ---
using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Logzio.DotNet.Core.Shipping
{
    internal sealed class JsonToStringConverter : JsonConverter
    {
        private readonly Type _type;

        /// <inheritdoc />
        public override bool CanRead { get; } = false;

        public JsonToStringConverter(Type type)
        {
            _type = type;
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.ToString());
            }
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException("Only serialization is supported");
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
#if NETSTANDARD1_3
            return _type.GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
#else
            return _type.IsAssignableFrom(objectType);
#endif
        }
    }
}
