using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace JsonSample
{
    public static class JsonWriterSample
    {
        public static void RunSample()
        {
            Console.WriteLine("**Utf8JsonWriter Sample***");

            var options = new JsonWriterOptions
            {
                Indented = true
            };

            var buffer = new ArrayBufferWriter<byte>();
            using var json = new Utf8JsonWriter(buffer, options);

            PopulateJson(json);
            json.Flush();

            var output = buffer.WrittenSpan.ToArray();
            var ourJson = Encoding.UTF8.GetString(output);
            Console.WriteLine(ourJson);
        }

        private static void PopulateJson(Utf8JsonWriter json)
        {
            json.WriteStartObject();
            json.WritePropertyName("courseName");
            json.WriteStringValue("Build Your Own Application Framework");

            json.WriteString("language", "C#");

            json.WriteStartObject("author");

            json.WriteString("firstName", "Matt");
            json.WriteString("lastName", "Honeycutt");

            json.WriteEndObject();
            json.WriteEndObject();
        }
    }
}
