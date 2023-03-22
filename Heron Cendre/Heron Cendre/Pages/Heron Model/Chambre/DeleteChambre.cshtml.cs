using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Heron_Cendre.Pages.Heron_Model.Chambre
{
    public class DeleteChambreModel : PageModel
    {
        public void OnGet()
        {
            string Id = Request.Query["Id"];
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=HeronRestau;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string script = "DELETE FROM chambre WHERE id_chambre = @Id";
                    using (SqlCommand command = new SqlCommand(script, con))
                    {

                        command.Parameters.AddWithValue("@Id", Id);
                        command.ExecuteNonQuery();
                    }
                }
                Response.Redirect("IndexChambre");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
    }
}
