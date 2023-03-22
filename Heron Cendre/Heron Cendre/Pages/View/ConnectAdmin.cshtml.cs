using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Heron_Cendre.Pages.View
{
    public class ConnectAdminModel : PageModel
    {
        public string errorMesssage = "";
        public string successMessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            string nomAdmin = Request.Form["nom"];
            string pwd = Request.Form["pwd"];   

            if (nomAdmin.Length == 0 || pwd.Length == 0)
            {
                errorMesssage = "Veuillez remplir tous les champs !!!";
                return;
            }
            if(nomAdmin == "admin" && pwd == "admin")
            {
                successMessage = "Ajout effectuer avec succes";
                nomAdmin = "";
                pwd = "";
                Console.WriteLine("Reussite");
                Response.Redirect("/Heron Model/AdminInterface");
            }
            else
            {
                errorMesssage = "Mot de passe ou nom administrateur errone!";
            }
        }
    }
}
