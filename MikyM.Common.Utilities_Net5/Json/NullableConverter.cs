using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MikyM.Common.Utilities_Net5.Json
{
    /// <summary>
    /// Converts from to and from <see cref="Nullable"/>.
    /// </summary>
    /// <typeparam name="TValue">The inner nullable value.</typeparam>
    public class NullableConverter<TValue> : JsonConverter<TValue?>
        where TValue : struct
    {
        /// <inheritdoc />
        public override TValue? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.TokenType switch
            {
                JsonTokenType.Null => null,
                _ => JsonSerializer.Deserialize<TValue>(ref reader, options)
            };
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, TValue? value, JsonSerializerOptions options)
        {
            if (!value.HasValue)
            {
                writer.WriteNullValue();
                return;
            }

            JsonSerializer.Serialize(writer, value.Value, options);
        }
    }
}