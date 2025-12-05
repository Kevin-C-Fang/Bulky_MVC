using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BulkyWebRazor_Temp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // Get/post endpoints typically seen in controller.
        // Default handler for get action method, naming must match -> "On" + "Get/Post/Etc".
        public void OnGet()
        {

        }
    }
}
