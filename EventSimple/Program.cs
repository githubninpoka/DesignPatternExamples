namespace EventSimple;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        Weather weather = new Weather();
        weather.Place = "Nieuwegein";
        weather.OnTempReached += Event_Temp_Reached;
        weather.SetTemp(29);
        Console.WriteLine(29);
        weather.SetTemp(31);
        Console.WriteLine(31);

        Weather weather2 = new Weather();
        weather2.Place = "Amsterdam";
        weather2.OnTempReached += Event_Temp_Reached;
        weather2.SetTemp(25);
        Console.WriteLine("25 in adam");
        weather2.SetTemp(31);
        Console.WriteLine("31 in adam");
    }
    static void Event_Temp_Reached(object sender, MyEventArgs e)
    {
        Weather eventOrigin = (Weather)sender;
        Console.WriteLine(eventOrigin.Place + " " + e.Value);
    }

}
public class Weather
{
    public string Place { get; set; }
    public int Temperature { get; set; }

    public event EventHandler<MyEventArgs> OnTempReached;

    public void SetTemp(int temp)
    {
        if (temp > 30)
        {
            MyEventArgs e = new MyEventArgs();
            
            e.Value = "Over 30";
            OnTempReached?.Invoke(this, e);
        }
    }
}

public class MyEventArgs : EventArgs
{
    public string Value { get; set; }
}
