using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Heron_Cendre.Pages.Heron_Model.Chambre
{
    public class UpdateChambreModel : PageModel
    {
        public string errorMesssage = "";
        public string successMessage = "";
        public Chambre chambre = new Chambre();
        public void OnGet()
        {
            try
            {
                string Id = Request.Query["Id"];
                string connectionString = "Data Source=.;Initial Catalog=HeronRestau;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string script = "select * from chambre where id_chambre=" + Id;
                    using (SqlCommand command = new SqlCommand(script, con))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                chambre.id_chambre = reader.GetInt32(0);
                                chambre.tarif_chambre = reader.GetDouble(1);
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
            chambre.tarif_chambre = int.Parse(Request.Form["tarif"]);
            chambre.photo_chambre = Request.Form["photo"];
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
                    string script = "UPDATE chambre " +
                        "SET tarif_chambre = @tarif_chambre," +
                        "photo_chambre = @photo_chambre "+
                        "Where id_chambre = @Id;";
                    using(SqlCommand cmd = new SqlCommand(script, con))
                    {
                        cmd.Parameters.AddWithValue("@tarif_chambre", chambre.tarif_chambre);
                        cmd.Parameters.AddWithValue("@photo_chambre", "/Images/"+chambre.photo_chambre);
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                successMessage = "Modification effectuer avec succes";
                chambre.tarif_chambre = null;
                Response.Redirect("IndexChambre");
            }
            catch(Exception ex)
            {
                errorMesssage = ex.Message;
            }
        }
    }
} 
