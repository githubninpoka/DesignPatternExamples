namespace Oreilly_HF_StrategyPattern
{
    internal class Program
    {
        // A classic example of the Strategy Pattern:
        // "Defines a family of algorithms (e.g. addition and substraction, or in this case fly behaviors of ducks),
        // encapsulates each one, and makes them interchangeable. Strategy lets the algorithm vary independently from
        // clients that use it."

        // Practically: just let any duck type inherit from duck. and 'load it up' with the appropriate behaviors.
        // no appropriate behavior available? just add the behavior to the library of available behaviors.
        // Open for extension, closed for modification...

        // Design principles:
        // Encapsulate those aspects of your class that change and separate them from the rest into other types
        // Program to an interface, not a concrete implementation
        // Favor composition over inheritance
        static void Main(string[] args)
        {
            Duck rubberDuck = new RubberDuck();
            rubberDuck.Description();
            rubberDuck.Swim();
            rubberDuck.MakeSound();
            rubberDuck.PerformFly();
            rubberDuck.flyBehavior = new FluentFly();
            rubberDuck.PerformFly();
        }
    }

    public interface QuackBehavior
    {
        void Quack();
    }
    public interface FlyBehavior
    {
        void Fly();
    }

    public abstract class Duck
    {
        public QuackBehavior quackBehavior { get; set; }
        public FlyBehavior flyBehavior { get; set; }

        public abstract void Description();

        public void MakeSound()
        {
            quackBehavior.Quack();
        }
        public void PerformFly()
        {
            flyBehavior.Fly();
        }

        public void Swim()
        {
            Console.WriteLine("I swim.");
        }
    }

    // implementations of behavior
    public class LoudQuack : QuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("QUACK");
        }
    }
    public class MuteQuack : QuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("quack..");
        }
    }
    public class Squeek : QuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("Squeek..");
        }
    }

    public class FluentFly : FlyBehavior
    {
        public void Fly()
        {
            Console.WriteLine("Soaring through the sky");
        }
    }

    public class NoFly : FlyBehavior
    {
        public void Fly()
        {
            Console.WriteLine("You shall not fly");
        }
    }

    // concrete duck
    public class RubberDuck : Duck
    {
        public RubberDuck()
        {
            flyBehavior = new NoFly();
            quackBehavior = new Squeek();
        }
        public override void Description()
        {
            Console.WriteLine("I am the rubber duck");
        }
    }
}
