using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace starting_project.Models
{
    public class QuestionDtoConverter : JsonConverter<QuestionDto>
    {
        public override QuestionDto Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            using (JsonDocument document = JsonDocument.ParseValue(ref reader))
            {
                if (!document.RootElement.TryGetProperty("Type", out JsonElement typeProperty))
                {
                    throw new JsonException("Missing property 'Type'");
                }

                string type = typeProperty.GetString();
                switch (type)
                {
                    case "Paragraph":
                        return JsonSerializer.Deserialize<ParagraphQuestionDto>(document.RootElement.GetRawText(), options);
                    case "YesNo":
                        return JsonSerializer.Deserialize<YesNoQuestionDto>(document.RootElement.GetRawText(), options);
                    case "Dropdown":
                        return JsonSerializer.Deserialize<DropdownQuestionDto>(document.RootElement.GetRawText(), options);
                    case "MultipleChoice":
                        return JsonSerializer.Deserialize<MultipleChoiceQuestionDto>(document.RootElement.GetRawText(), options);
                    case "Date":
                        return JsonSerializer.Deserialize<DateQuestionDto>(document.RootElement.GetRawText(), options);
                    case "Number":
                        return JsonSerializer.Deserialize<NumberQuestionDto>(document.RootElement.GetRawText(), options);
                    default:
                        throw new JsonException($"Unknown type: {type}");
                }
            }
        }

        public override void Write(Utf8JsonWriter writer, QuestionDto value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, value.GetType(), options);
        }
    }
}
