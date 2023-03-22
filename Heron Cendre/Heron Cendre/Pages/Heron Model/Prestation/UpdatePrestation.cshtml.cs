using Heron_Cendre.Pages.Heron_Restau_Cendre.Prestation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Heron_Cendre.Pages.Heron_Model.Prestation
{
    public class UpdatePrestationModel : PageModel
    {
        public string errorMesssage = "";
        public string successMessage = "";
        public prestation presta = new prestation();
        public void OnGet()
        {
            try 
            {
                string Id = Request.Query["Id"];
                string connectionString = "Data Source=.;Initial Catalog=HeronRestau;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string script = "select * from prestation where id_prestation =" + Id;
                    using(SqlCommand command = new SqlCommand(script, con))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                presta.id_prestation = reader.GetInt32(0);
                                presta.titre_prestation = reader.GetString(1);
                                presta.tarif_prestation = reader.GetDouble(2);
                                presta.planing_prestation = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void OnPost()
        {
            string Id = Request.Form["Id"];
            presta.titre_prestation = Request.Form["Title"];
            presta.tarif_prestation = int.Parse(Request.Form["tarif"]);
            presta.planing_prestation = Request.Form["planing"];
            presta.photo1 = Request.Form["photo"];

            if(presta.titre_prestation.Length == 0 || presta.tarif_prestation == null || presta.planing_prestation.Length == 0)
            {
                errorMesssage = "Veuillez remplir tous les champs !!!";
                return;
            }
            try {
                string connectionString = "Data Source=.;Initial Catalog=HeronRestau;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string script = "UPDATE prestation " +
                        "SET titre_prestation=@titre_prestation," +
                        " tarif_prestation=@tarif_prestation," +
                        " planing_prestation=@planing_prestation," +
                        "photo1_prestation =@photo1_prestation " +
                        "WHERE id_prestation = @Id;";

                    using (SqlCommand cmd = new SqlCommand(script, con))
                    {
                        cmd.Parameters.AddWithValue("@titre_prestation", presta.titre_prestation);
                        cmd.Parameters.AddWithValue("@tarif_prestation", presta.tarif_prestation);
                        cmd.Parameters.AddWithValue("@planing_prestation", presta.planing_prestation);
                        cmd.Parameters.AddWithValue("@photo1_prestation", presta.photo1);
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                successMessage = "Modification effectuer avec succes";
                presta.titre_prestation = "";
                presta.tarif_prestation = null;
                presta.planing_prestation = "";
                Response.Redirect("IndexPrestation");
            }
            catch (Exception ex)
            {
                errorMesssage = ex.Message;
            }
        }
    }
}
