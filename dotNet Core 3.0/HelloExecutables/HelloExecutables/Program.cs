using System;

namespace HelloExecutables
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("What version am I? " + Environment.Version);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
