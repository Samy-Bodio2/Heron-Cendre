using Heron_Cendre.Pages.Heron_Model.Chambre;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Heron_Cendre.Pages.View
{
    public class ChambreModel : PageModel
    {
        public string test { get; set; }
        public List<Chambre> Chambres = new List<Chambre>();
        public void OnGet()
        {
            try
            {
                test = HttpContext.Session.GetString("test");
                string connectionString = "Data Source=.;Initial Catalog=HeronRestau;Integrated Security=True;";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string script = "SELECT * FROM Chambre";
                    using (SqlCommand cmd = new SqlCommand(script, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Chambre chambre = new Chambre();
                                chambre.id_chambre = reader.GetInt32(0);
                                chambre.tarif_chambre = reader.GetDouble(1);
                                chambre.photo_chambre = reader.GetString(2);

                                Chambres.Add(chambre);
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
    }
}
