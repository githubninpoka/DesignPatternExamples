namespace AbstractFactoryPattern1
{
    internal class Program
    {
        // https://www.youtube.com/watch?v=xN7EFHU_rXA starts after 2 or 3 minutes.
        static void Main(string[] args)
        {
            NavigationBar androidNavBar = new NavigationBar(new AndroidUIElements());
            NavigationBar appleNavBar = new NavigationBar(new AppleUIElements());
            Console.ReadLine();
            
        }
    }

    public class NavigationBar
    {
        public NavigationBar(IFactoryUIElements factory)
        {
            Button = factory.CreateButton();
        }

        public Button Button { get; }
    }

    public interface IFactoryUIElements
    {
        public Button CreateButton();
        // and countless other ui elements
    }

    public class Button
    {
        public string TypeOfButton = "red";
    }

    public class AndroidUIElements : IFactoryUIElements
    {
        public Button CreateButton()
        {
            return new Button() { TypeOfButton = "AndroidButton"};
        }
    }
    public class AppleUIElements : IFactoryUIElements
    {
        public Button CreateButton()
        {
            return new Button() { TypeOfButton = "IOSButton"};
        }
    }
}
