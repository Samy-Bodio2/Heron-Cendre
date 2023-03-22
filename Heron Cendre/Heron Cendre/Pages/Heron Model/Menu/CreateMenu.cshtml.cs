using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Heron_Cendre.Pages.Heron_Model.Chambre;
using Heron_Cendre.Services;

namespace Heron_Cendre.Pages.Heron_Model.Menu
{
    public class CreateMenuModel : PageModel
    {
        public string errorMesssage = "";
        public string successMessage = "";
        Menu menu = new Menu();
        public void OnGet()
        {
        }
        public void OnPost()
        {
            menu.titre_menu = Request.Form["titre"];
            menu.tarif_menu = int.Parse(Request.Form["tarif"]);
            menu.photo1_menu = Request.Form["photo"];
            string sourcePath = @"D:\Images ASP HEron";
            string targetPath = @"C:\Users\CLIENT\Desktop\Projet ASP\Heron Cendre\Heron Cendre\Images";
            CopierPhoto Cp = new CopierPhoto();
            Cp.CopierP(menu.photo1_menu, sourcePath, targetPath);

            if (menu.titre_menu.Length == 0 || menu.tarif_menu == null )
            {
				errorMesssage = "Veuillez remplir tous les champs !!!";
				return;
			}
            try
            {
				string connectionString = "Data Source=.;Initial Catalog=HeronRestau;Integrated Security=True";
				using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string script = "INSERT INTO Menu " +
                        "(titre_menu,tarif_menu,photo1_menu) VALUES" +
                        "(@titre_menu,@tarif_menu,@photo1_menu)";
                    Console.WriteLine("Menu Reussi");
                    using(SqlCommand cmd = new SqlCommand(script, con))
                    {
                        cmd.Parameters.AddWithValue("@titre_menu", menu.titre_menu);
                        cmd.Parameters.AddWithValue("@tarif_menu", menu.tarif_menu);
                        cmd.Parameters.AddWithValue("@photo1_menu", "/Images/" + menu.photo1_menu);
                        Console.WriteLine("Menu Reussi2");
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Menu Reussi3");
                    }
				}
                successMessage = "Ajout effectuer avec succes";
                menu.titre_menu = "";
                menu.tarif_menu = null;
				Response.Redirect("IndexMenu");
			}
            catch(Exception ex)
            {
				errorMesssage = ex.Message;
			}
        }
        
    }
}
