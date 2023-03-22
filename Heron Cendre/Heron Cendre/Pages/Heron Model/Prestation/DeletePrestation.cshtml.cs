using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Heron_Cendre.Pages.Heron_Model.Prestation;

namespace Heron_Cendre.Pages.Heron_Model.Prestation
{
    public class DeletePrestationModel : PageModel
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
                    string script = "DELETE FROM prestation WHERE id_prestation = @Id";
                    using (SqlCommand command = new SqlCommand(script, con))
                    {

                        command.Parameters.AddWithValue("@Id", Id);
                        command.ExecuteNonQuery();
                    }
                }
                Response.Redirect("IndexPrestation");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }
        public void OnPost() 
        {

        }
    }
}
