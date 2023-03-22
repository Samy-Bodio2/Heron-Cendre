using Heron_Cendre.Pages.Heron_Restau_Cendre.Prestation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Heron_Cendre.Pages.View
{
    public class PrestationModel : PageModel
    {
        public string test { get; set; }
        public List<prestation> Presta = new List<prestation>();
        public void OnGet()
        {
            try
            {
                test = HttpContext.Session.GetString("test");
                string connectionString = "Data Source=.;Initial Catalog=HeronRestau;Integrated Security=True;";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string script = "SELECT * FROM prestation";
                    using (SqlCommand cmd = new SqlCommand(script, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                prestation prsta = new prestation();
                                prsta.id_prestation = reader.GetInt32(0);
                                prsta.titre_prestation = reader.GetString(1);
                                prsta.tarif_prestation = reader.GetDouble(2);
                                prsta.planing_prestation = reader.GetString(3);
                                prsta.photo1 = reader.GetString(4);

                                Presta.Add(prsta);
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
