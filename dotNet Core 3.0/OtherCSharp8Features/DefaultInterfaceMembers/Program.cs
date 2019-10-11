using AmazingAnimalWidgets;
using System;

namespace DefaultInterfaceMembers
{
    class Program
    {
        static void Main(string[] args)
        {
            IAnimalWidget.SetAmountToFeed(20);
            var dog = new DogWidget();
            var cat = new CatWidget();
            var hamster = new HamsterWidget();

            var animals = new IAnimalWidget[] { dog, cat, hamster };

            dog.Feed();

            foreach (var animal in animals)
            {
                animal.Feed();
                Console.WriteLine($"Happiness level for {animal.Name}: {animal.Happiness}");
            }
        }
    }
}
