using System.Text.Json;
using System.Text.Json.Serialization;

namespace XTI_Hub.Abstractions;

public sealed class AppVersionNameJsonConverter : JsonConverter<AppVersionName>
{
    public override AppVersionName? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            return new AppVersionName(reader.GetString() ?? "");
        }
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject token");
        }
        var value = "";
        while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
        {
            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propName = reader.GetString();
                if (propName == "Value")
                {
                    reader.Read();
                    value = reader.GetString();
                }
            }
        }
        return new AppVersionName(value ?? "");
    }

    public override void Write(Utf8JsonWriter writer, AppVersionName value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Value", value.Value);
        writer.WriteString("DisplayText", value.DisplayText);
        writer.WriteEndObject();
    }
}
