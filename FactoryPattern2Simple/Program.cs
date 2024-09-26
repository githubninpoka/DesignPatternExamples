namespace FactoryPattern2Simple;

internal class Program
{

    // https://www.youtube.com/watch?v=xN7EFHU_rXA
    // this is number 1, the simplest
    static void Main(string[] args)
    {
        // I need 2 objects that are the same type
        // but I want the implementation to be done in 1 place
        // now if I need 5 buttons, I only need to specify for instance the chosen formatting of the button in 1 place, the factory.

        IComponent component1 = ComponentFactory.Create();
        IComponent component2 = ComponentFactory.Create();
        Console.WriteLine(component1.GetMyType());
        Console.WriteLine(component2.GetMyType());
    }
}

public static class ComponentFactory
{
    public static IComponent Create()
    {
        return new Component()
        {
            Type = "whatever"
        };
    }
}

public interface IComponent
{
    string GetMyType();
}

public class Component : IComponent
{
    public string Type;

    public string GetMyType()
    {
        return Type;
    }

}
