using System;

namespace StaticLocalFunctions
{
    class StaticLocalDemo
    {
        private string Field = "initial-value";
        public void Run()
        {
            var state = "42";
            DoSomethingWithState();
            DoSomethingWithField();

            Console.WriteLine($"state: {state}");
            Console.WriteLine($"Field: {Field}");

            string DoSomethingWithState()
            {
                return state = "99";
            }

            string DoSomethingWithField()
            {
                return Field = "modified!";
            }
        }
    }
}
