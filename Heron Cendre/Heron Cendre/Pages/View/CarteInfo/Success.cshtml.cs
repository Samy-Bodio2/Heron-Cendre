using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Heron_Cendre.Pages.View.CarteInfo
{
    public class SuccessModel : PageModel
    {
        public string nomCom;
        public void OnGet()
        {
            nomCom = HttpContext.Session.GetString("nomC");
        }
    }
}
