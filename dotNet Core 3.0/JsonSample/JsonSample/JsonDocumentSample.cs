using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace JsonSample
{
    public static class JsonDocumentSample
    {
        public static void RunSample()
        {
            Console.WriteLine("**JsonDocument Sample***");
            using var stream = File.OpenRead("sample.json");
            using var doc = JsonDocument.Parse(stream);

            var root = doc.RootElement;

            var firstName = root
                   .GetProperty("author")
                   .GetProperty("firstName")
                   .GetString();

            Console.WriteLine($"Author first name: {firstName}");

            EnumerateElement(root);
        }

        private static void EnumerateElement(JsonElement root)
        {
            foreach (var prop in root.EnumerateObject())
            {
                if (prop.Value.ValueKind == JsonValueKind.Object)
                {
                    Console.WriteLine($"{prop.Name}:");
                    Console.WriteLine($"--BEGIN OBJECT--");
                    EnumerateElement(prop.Value);
                    Console.WriteLine($"--END OBJECT--");
                }
                else
                {
                    Console.WriteLine($"{prop.Name}: {prop.Value.GetRawText()}");
                }
            }
        }

    }
}
