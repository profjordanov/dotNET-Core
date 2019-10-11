using AmazingAnimalWidgets;

namespace DefaultInterfaceMembers
{
    public class DogWidget : IAnimalWidget
    {
        public string Name => "Dog";
        public int Happiness { get; set; } = 50;

        public void Feed()
        {
            Happiness = int.MaxValue;
        }
    }

    public class CatWidget : IAnimalWidget
    {
        public string Name => "Cat";
        public int Happiness { get; set; } = 0;
    }

    public class HamsterWidget : IAnimalWidget
    {
        public string Name => "Hamster";
        public int Happiness { get; set; } = 50;
    }
}
