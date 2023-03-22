using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Heron_Cendre.Pages.Heron_Restau_Cendre.Prestation;
using Heron_Cendre.Pages.Heron_Model.Chambre;
using Heron_Cendre.Services;

namespace Heron_Cendre.Pages.Heron_Model.Prestation
{
    public class createPrestationModel : PageModel
    {
        public string errorMesssage = "";
        public string successMessage = "";
        prestation presta = new prestation();

        public void OnGet()
        {
        }
        public void OnPost()
        {
            presta.titre_prestation = Request.Form["titre"];
            presta.tarif_prestation = int.Parse(Request.Form["tarif"]);
            presta.planing_prestation = Request.Form["Planning"];
            presta.photo1 = Request.Form["photo"];
            string sourcePath = @"D:\Images ASP HEron";
            string targetPath = @"C:\Users\CLIENT\Desktop\Projet ASP\Heron Cendre\Heron Cendre\wwwroot\Images";
            CopierPhoto Cp = new CopierPhoto();
            Cp.CopierP(presta.photo1, sourcePath, targetPath);

            if (presta.titre_prestation.Length == 0 || presta.tarif_prestation == null
                || presta.planing_prestation.Length == 0 )
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
                    string script = "INSERT INTO prestation " +
                        "(titre_prestation,tarif_prestation,planing_prestation,photo1_prestation) VALUES" +
                        "(@titre_prestation,@tarif_prestation,@planing_prestation,@photo1_prestation)";
                    Console.WriteLine("Reussi1");
                    using (SqlCommand cmd = new SqlCommand(script, con))
                    {
                        cmd.Parameters.AddWithValue("@titre_prestation", presta.titre_prestation);
                        cmd.Parameters.AddWithValue("@tarif_prestation", presta.tarif_prestation);
                        cmd.Parameters.AddWithValue("@planing_prestation", presta.planing_prestation);
                        cmd.Parameters.AddWithValue("@photo1_prestation", "/Images/"+presta.photo1);
                        Console.WriteLine("Reussi2");
                        //cmd.Parameters.AddWithValue("@photo1_prestation", presta.photo1);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Reussi3");
                    }
                }
                successMessage = "Ajout effectuer avec succes";
                presta.titre_prestation = "";
                presta.tarif_prestation = null;
                presta.planing_prestation = "";
                Response.Redirect("IndexPrestation");

                //presta.photo1 = "";

            }
            catch (Exception ex)
            {
                errorMesssage = ex.Message;
            }
        }
    }
}
