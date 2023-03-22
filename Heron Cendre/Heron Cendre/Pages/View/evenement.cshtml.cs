using Heron_Cendre.Pages.Heron_Model.Evenement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Heron_Cendre.Pages.View
{
    public class evenementModel : PageModel
    {
        public string test;
        public List<Evenement> Events = new List<Evenement>();
        public void OnGet()
        {
            try
            {
                test = HttpContext.Session.GetString("test");
                string connectionString = "Data Source=.;Initial Catalog=HeronRestau;Integrated Security=True;";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string script = "SELECT * from Evenement";
                    using (SqlCommand cmd = new SqlCommand(script, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Evenement even = new Evenement();
                                even.id_evenement = reader.GetInt32(0);
                                even.theme = reader.GetString(1);
                                even.date_debut_even = reader.GetString(2);
                                even.fin_debut_even = reader.GetString(3);
                                even.tarif_evenement = reader.GetDouble(4);
                                even.photo_even = reader.GetString(5);

                                Events.Add(even);
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
