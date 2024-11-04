using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace WebApiFilters.Filters.ResourceFilters;

public class WeatherForecastResourceFilterAttribute : IResourceFilter
{
    private readonly ILogger<WeatherForecastResourceFilterAttribute> logger;

    public WeatherForecastResourceFilterAttribute(ILogger<WeatherForecastResourceFilterAttribute> logger)
    {
        this.logger = logger;
    }

    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        logger.LogInformation("This is ran even before Model binding, but after authorization filters");
    }
    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        logger.LogInformation("This is ran right after the ResultFilter has done its job");
        
    }
}
