using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace WebApiFilters.Filters.ActionFilters;

public class WeatherForecastActionFilterAttribute : ActionFilterAttribute
{
    private readonly ILogger<WeatherForecastActionFilterAttribute> logger;

    public WeatherForecastActionFilterAttribute(ILogger<WeatherForecastActionFilterAttribute> logger)
    {
        this.logger = logger;
    }
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        logger.LogInformation($"This is logged just before the action is ran. Triggered from {context.HttpContext.Connection.RemoteIpAddress!.ToString()}", [context.HttpContext.Connection.RemoteIpAddress!.ToString()]);
        
    }
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);
        logger.LogInformation("This is logged just after the action is ran.", [2]) ;
    }
}
