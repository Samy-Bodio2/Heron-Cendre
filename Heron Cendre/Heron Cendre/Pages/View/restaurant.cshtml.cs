using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Heron_Cendre.Pages.View
{
    public class restaurantModel : PageModel
    {
        public string? test;
        public void OnGet()
        {
            test = HttpContext.Session.GetString("test");
        }
    }
}
