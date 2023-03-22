using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Heron_Cendre.Pages.View.Classe;
using System.Data.SqlClient;
using Azure.Identity;

namespace Heron_Cendre.Pages.View
{
    public class ConnexionModel : PageModel
    {
		public List<Client> Clients = new List<Client>();
		public string? test { get; set; }
 		public void OnGet()
        {
		}
        public void OnPost() 
        {

			string login = Request.Form["login"];
			string pwd = Request.Form["pwd"];
			try
			{
				string connectionString = "Data Source=.;Initial Catalog=HeronRestau;Integrated Security=True;";
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					con.Open();
					string script = "SELECT * FROM client " +
						"WHERE mot_de_passe = @mot_de_passe and login = @login";
					using (SqlCommand cmd = new SqlCommand(script, con))
					{
						cmd.Parameters.AddWithValue("@mot_de_passe", pwd);
						cmd.Parameters.AddWithValue("@login", login);
						using (SqlDataReader reader = cmd.ExecuteReader())
						{
							while (reader.Read())
							{
								Client clt = new Client();
								clt.nomComplet = reader.GetString(1);
								clt.pwd = reader.GetString(4);
								clt.login = reader.GetString(5);
								Console.Write(clt.login);
                                HttpContext.Session.SetString("test", clt.login);
								HttpContext.Session.SetString("nomC", clt.nomComplet);
                                Clients.Add(clt);

								if (clt.pwd.Length != 0 && clt.login.Length != 0)
								{
									Response.Redirect("Chambre");
								}
                                
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
