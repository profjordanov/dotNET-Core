using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace JsonSample
{
    public static class JsonReaderSample
    {
        public static void RunSample()
        {
            Console.WriteLine("**Utf8JsonReader Sample***");
            var jsonBytes = File.ReadAllBytes("sample.json");
            var jsonSpan = jsonBytes.AsSpan();
            var json = new Utf8JsonReader(jsonSpan);

            while (json.Read())
            {
                Console.WriteLine(GetTokenDesc(json));
            }

        }

        private static string GetTokenDesc(Utf8JsonReader json) =>
            json.TokenType switch
            {
                JsonTokenType.StartObject => "START OBJECT",
                JsonTokenType.EndObject => "END OBJECT",
                JsonTokenType.StartArray => "START ARRAY",
                JsonTokenType.EndArray => "END ARRAY",
                JsonTokenType.PropertyName => $"PROPERTY: {json.GetString()}",
                JsonTokenType.Comment => $"COMMENT: {json.GetString()}",
                JsonTokenType.String => $"STRING: {json.GetString()}",
                JsonTokenType.Number => $"NUMBER: {json.GetInt32()}",
                JsonTokenType.True => $"BOOL: {json.GetBoolean()}",
                JsonTokenType.False => $"BOOL: {json.GetBoolean()}",
                JsonTokenType.Null => $"NULL",
                _ => $"**UNHANDLED TOKEN: {json.TokenType}"
            };
    }
}
