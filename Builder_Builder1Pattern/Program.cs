namespace Builder_Builder1Pattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // just an example that i made

            DogBuilder dogBuilder = new();
            dogBuilder.GiveEars("brown");
            dogBuilder.GiveEyes("brown");
            // could be something along the lines of dogBuilder.GiveFeet(new DogFeet()); 
            // but can also be delegated to the builderclass itself, possibly in the constructor
            Dog dog = dogBuilder.Build();

            DogBuilder dogBuilder2 = new();
            dogBuilder2.GiveEars("white");
            dogBuilder2.GiveEyes("green");
            Dog dog2 = dogBuilder2.Build();

            Console.ReadLine();
        }
    }
}
