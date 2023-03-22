using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Heron_Cendre.Pages
{


	public class IndexModel : PageModel
    {
        public string? test;
		private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            test = HttpContext.Session.GetString("test");
        }
    }
}