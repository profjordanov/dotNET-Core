using System;

namespace UsingDeclarations
{
    class Program
    {
        static void Main(string[] args)
        {
            using var resource = new DisposableResource();

            Console.WriteLine($"Using resource: {resource.Value}");

            Console.WriteLine("All finished!");
        }
    }
}
