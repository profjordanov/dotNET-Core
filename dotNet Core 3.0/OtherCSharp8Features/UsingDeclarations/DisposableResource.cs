using System;

namespace UsingDeclarations
{
    class DisposableResource : IDisposable
    {
        public string Value => "disposable-resource";

        public void Dispose()
        {
            Console.WriteLine("DisposableResource: Disposing!");
        }
    }
}
