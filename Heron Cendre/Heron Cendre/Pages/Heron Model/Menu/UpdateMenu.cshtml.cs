using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace Heron_Cendre.Pages.Heron_Model.Menu
{
    public class UpdateMenuModel : PageModel
    {
        public string errorMesssage = "";
        public string successMessage = "";
        public Menu Menu = new Menu();
        public void OnGet()
        {
            try
            {
                string Id = Request.Query["Id"];
                string connectionString = "Data Source=.;Initial Catalog=HeronRestau;Integrated Security=True";
                using(SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string script = "select * from Menu where id_menu =" + Id;
                    using(SqlCommand cmd = new SqlCommand(script, con))
                    {
                        using(SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Menu.id_menu = reader.GetInt32(0);
                                Menu.titre_menu = reader.GetString(1);
                                Menu.date_menu = reader.GetDateTime(2);
                                Menu.tarif_menu = reader.GetDouble(3);
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
        public void OnPost()
        {
            string Id = Request.Form["Id"];
            Menu.titre_menu = Request.Form["Title"];
            Menu.tarif_menu = int.Parse(Request.Form["tarif"]);
            Menu.photo1_menu = Request.Form["photo"];
            if(Menu.titre_menu.Length == 0 || Menu.tarif_menu == null)
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
                    string script = "UPDATE Menu " +
                        "SET titre_menu=@titre_menu," +
                        " tarif_menu=@tarif_menu," +
                        "photo1_menu=@photo1_menu" +
                        " WHERE id_menu = @Id;";
                    using (SqlCommand cmd = new SqlCommand(script, con))
                    {
                        cmd.Parameters.AddWithValue("@titre_menu", Menu.titre_menu);
                        cmd.Parameters.AddWithValue("@tarif_menu", Menu.tarif_menu);
                        cmd.Parameters.AddWithValue("@photo1_menu", Menu.photo1_menu);
                        cmd.Parameters.AddWithValue("@Id", Id);
                        cmd.ExecuteNonQuery();
                    }
                }
                successMessage = "Modification effectuer avec succes";
                Menu.titre_menu = "";
                Menu.tarif_menu = null;
                Response.Redirect("IndexMenu");
            }
            catch (Exception ex)
            {
                errorMesssage = ex.Message;
            }
        }
    }
}
