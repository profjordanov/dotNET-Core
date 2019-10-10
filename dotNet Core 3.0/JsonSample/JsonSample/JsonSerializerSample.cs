using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace JsonSample
{
    public class Course
    {
        public string CourseName { get; set; }
        public string Language { get; set; }
        public Author Author { get; set; }
        public DateTime PublishedAt { get; set; }
        public int PublishedYear { get; set; }
        public bool IsActive { get; set; }
        public string[] Tags { get; set; }
    }

    public class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


    public static class JsonSerializerSample
    {
        public static void RunSample()
        {
            Console.WriteLine("**JsonSerializer Sample***");

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };


            var text = File.ReadAllText("sample.json");
            var course = JsonSerializer.Deserialize<Course>(text, options);

            //Console.WriteLine($"Course name: {course.CourseName}");
            //Console.WriteLine($"Author: {course.Author.FirstName} " +
            //                  $"{course.Author.LastName}");

            Console.WriteLine(JsonSerializer.Serialize(course, options));


        }
    }
}
