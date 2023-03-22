using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Heron_Cendre.Services;

namespace Heron_Cendre.Pages.Heron_Model.Chambre
{
    public class CreateChambreModel : PageModel
    {
        public string errorMesssage = "";
        public string successMessage = "";
        Chambre chambre= new Chambre();
        public void OnGet()
        {
        }
        public void OnPost() 
        {
            chambre.photo_chambre = Request.Form["photo"];
            chambre.tarif_chambre = int.Parse(Request.Form["tarif"]);
            string sourcePath = @"D:\Images ASP HEron";
            string targetPath = @"C:\Users\CLIENT\Desktop\Projet ASP\Heron Cendre\Heron Cendre\wwwroot\Images";
            CopierPhoto Cp = new CopierPhoto();
            Cp.CopierP(chambre.photo_chambre, sourcePath, targetPath);
            if (chambre.tarif_chambre == null)
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
                    string script = "INSERT INTO Chambre " +
                        "(tarif_chambre,photo_chambre) VALUES" + "(@tarif_chambre,@photo_chambre)";
                    Console.WriteLine("Reussi1");
                    using (SqlCommand cmd = new SqlCommand(script, con))
                    {
                        cmd.Parameters.AddWithValue("@tarif_chambre", chambre.tarif_chambre);
                        cmd.Parameters.AddWithValue("@photo_chambre", "/Images/"+chambre.photo_chambre);
                        Console.WriteLine("Reussi2");
                        //cmd.Parameters.AddWithValue("@photo1_prestation", presta.photo1);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Reussi3");
                    }
                }
                successMessage = "Ajout effectuer avec succes";
                chambre.tarif_chambre = null;
                Response.Redirect("IndexChambre");
            }
            catch (Exception ex)
            {
                errorMesssage = ex.Message;
            }
        }
    }
}
