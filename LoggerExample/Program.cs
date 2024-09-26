namespace LoggerExample;

internal class Program
{
    public static ILogger logger = new Logger();
    static void Main(string[] args)
    {
        logger.Log("Hello Log World! Imma launch a boat");
        DreamBoat boat = new DreamBoat(logger);
        boat.Launch();
    }
}
public class DreamBoat
{
    private readonly ILogger logger;

    public DreamBoat(ILogger logger)
    {
        this.logger = logger;
    }

    public void Launch()
    {
        logger.Log("Splash");
    }
}


public interface ILogger
{
    void Log(string message);
}
public class Logger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"= {message} =");
    }
}