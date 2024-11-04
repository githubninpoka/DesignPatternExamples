using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiFilters.Filters.ResultFilters;

public class WeatherForecastResultFilterAttribute : ResultFilterAttribute
{
    private readonly ILogger<WeatherForecastResultFilterAttribute> logger;

    public WeatherForecastResultFilterAttribute(ILogger<WeatherForecastResultFilterAttribute> logger)
    {
        this.logger = logger;
    }
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        logger.LogInformation("This is logged just before preparation of the Http Result");
        base.OnResultExecuting(context);
    }
    public override void OnResultExecuted(ResultExecutedContext context)
    {
        logger.LogInformation("This is logged just after the Http Result was prepared");
        base.OnResultExecuted(context);
    }
}
