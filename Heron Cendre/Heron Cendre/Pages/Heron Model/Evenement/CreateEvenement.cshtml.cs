using Heron_Cendre.Pages.Heron_Model.Chambre;
using Heron_Cendre.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Heron_Cendre.Pages.Heron_Model.Evenement
{
    public class CreateEvenementModel : PageModel
    {
        public string errorMesssage = "";
        public string successMessage = "";
        Evenement even = new Evenement();
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            even.theme = Request.Form["theme"];
            even.date_debut_even = Request.Form["debut"].ToString();
            even.fin_debut_even = Request.Form["fin"].ToString();
            even.tarif_evenement = int.Parse(Request.Form["tarif"]);
            even.photo_even = Request.Form["photo"];
            string sourcePath = @"D:\Images ASP HEron";
            string targetPath = @"C:\Users\CLIENT\Desktop\Projet ASP\Heron Cendre\Heron Cendre\wwwroot\Images";
            CopierPhoto Cp = new CopierPhoto();
            Cp.CopierP(even.photo_even, sourcePath, targetPath);

            if (even.theme.Length == 0 || even.date_debut_even.Length == 0 ||
                even.fin_debut_even.Length == 0 || even.tarif_evenement == null)
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
                    string script = "INSERT INTO Evenement " +
                        "(theme,date_debut_even,fin_debut_even,tarif_evenement,photo_even) VALUES" +
                        "(@theme,@date_debut_even,@fin_debut_even,@tarif_evenement,@photo_even)";
                    Console.WriteLine("Reussi1");
                    using (SqlCommand cmd = new SqlCommand(script, con))
                    {
                        cmd.Parameters.AddWithValue("@theme", even.theme);
                        cmd.Parameters.AddWithValue("@date_debut_even", even.date_debut_even);
                        cmd.Parameters.AddWithValue("@fin_debut_even", even.fin_debut_even);
                        cmd.Parameters.AddWithValue("@tarif_evenement",even.tarif_evenement);
                        cmd.Parameters.AddWithValue("@photo_even","/Images/"+ even.photo_even);
                        Console.WriteLine("Reussi2");
                        //cmd.Parameters.AddWithValue("@photo1_prestation", presta.photo1);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Reussi3");
                    }
                }
                successMessage = "Ajout effectuer avec succes";
                even.theme = "";
                even.date_debut_even = "";
                even.fin_debut_even = "";
                even.tarif_evenement = null;
                Response.Redirect("IndexEvenement");
            }
            catch (Exception ex)
            {
                errorMesssage = ex.Message;
            }
        }
    }
}
