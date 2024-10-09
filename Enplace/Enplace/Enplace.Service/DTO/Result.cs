using System.Text.Json;
using System.Text.Json.Serialization;

namespace Enplace.Service.DTO
{
    public class Result<T>
    {
        public string EntityName => typeof(T).Name;
        public T? Entity { get; set; }
        public ResultStatus Status { get; set; }
        public ResultError? Error { get; set; } = null;
    }
    public class Result
    {
        public static Result<TEntity> WrapOk<TEntity>(TEntity entity)
        {
            return new() { Status = ResultStatus.Success, Entity = entity };
        }
        public static Result<TEntity> WrapError<TEntity>(ErrorType error, string message)
        {
            return new Result<TEntity>() { Status = ResultStatus.Success, Error = new() { Error = error, Message = message } };
        }

    }

    public class ResultJsonConverter<T> : JsonConverter<Result<T>>
    {
        public override Result<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException(); // Implement deserialization if needed
        }

        public override void Write(Utf8JsonWriter writer, Result<T> value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            // Use the concrete type name as the label for the Entity
            writer.WritePropertyName(typeof(T).Name);
            JsonSerializer.Serialize(writer, value.Entity, options);

            writer.WriteString("Status", value.Status.ToString());

            if (value.Error != null)
            {
                writer.WritePropertyName("Error");
                JsonSerializer.Serialize(writer, value.Error, options);
            }

            writer.WriteEndObject();
        }
    }
}
