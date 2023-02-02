using System.Text.Json;

namespace SharpMason.Extensions.Utils
{
    public static class JsonUtil
    {
        private static readonly JsonSerializerOptions IndentedJsonOptions = new()
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        public static string ToJson(this object obj, JsonSerializerOptions? jsonOptions = null)
        {
            jsonOptions ??= JsonOptions;
            return JsonSerializer.Serialize(obj, jsonOptions);
        }

        public static string ToIndentedJson(this object obj, JsonSerializerOptions? jsonOptions = null)
        {
            jsonOptions ??= IndentedJsonOptions;
            return JsonSerializer.Serialize(obj, jsonOptions);
        }
    }
}
