using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data;

namespace Heron_Cendre.Pages.Heron_Model.Menu
{
    public class IndexMenuModel : PageModel
    {
        public List<Menu> MENU = new List<Menu>(); 
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=.;Initial Catalog=HeronRestau;Integrated Security=True;";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string script = "SELECT * FROM Menu";
                    using (SqlCommand cmd = new SqlCommand(script, con))
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                Menu Menu = new Menu();
                                Menu.id_menu = reader.GetInt32(0);
                                Menu.titre_menu = reader.GetString(1);
                                Menu.date_menu = reader.GetDateTime(2);
                                Menu.tarif_menu = reader.GetDouble(3);
                                Menu.photo1_menu = reader.GetString(4);

                                MENU.Add(Menu);
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
