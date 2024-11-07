using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SerilogDemo.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("You requested the index page");
            try
            {
                for (int i = 0; i < 100; i++)
                {
                    if (i == 50)
                    {
                        throw new Exception("Exception for demo purposes");
                    }
                    else
                    {
                        _logger.LogInformation("The value of i is {iVariable}", i);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "we caught an exception in OnGet of IndexModel {var}", 1);
            }
        }
    }
}
