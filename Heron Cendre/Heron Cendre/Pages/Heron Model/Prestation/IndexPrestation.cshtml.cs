using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Heron_Cendre.Pages.Heron_Restau_Cendre.Prestation;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

namespace Heron_Cendre.Pages.Heron_Model.Prestation
{
    public class IndexPrestationModel : PageModel
    {
        public List<prestation> Presta = new List<prestation>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=HeronRestau;Integrated Security=True;";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string script = "SELECT * FROM prestation";
                    using (SqlCommand cmd = new SqlCommand(script, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                prestation prsta = new prestation();
                                prsta.id_prestation =reader.GetInt32(0);
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
