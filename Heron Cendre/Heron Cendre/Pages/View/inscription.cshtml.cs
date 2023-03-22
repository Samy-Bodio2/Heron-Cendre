using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Heron_Cendre.Pages.View.Classe;
using System.Data.SqlClient;
using Heron_Cendre.Pages.Heron_Model.Menu;

namespace Heron_Cendre.Pages.View
{
    public class inscriptionModel : PageModel
    {
		public string errorMesssage = "";
		public string successMessage = "";
        public string log;
        public string pwd;

		Client clt = new Client();
		public void OnGet()
        {
		}
        public void OnPost() 
        {
            clt.nomComplet = Request.Form["name"];
            clt.email = Request.Form["email"];
            clt.tel = int.Parse(Request.Form["phone"]);
            clt.login = Request.Form["log"];
            clt.pwd = Request.Form["pwd"];
            if (clt.nomComplet == "" || clt.email == "" || clt.tel == null || clt.login == "" || clt.pwd == "")
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
                    string script = "INSERT INTO client " +
                        "(nom_complet,Email,telephone_client,mot_de_passe,login) VALUES" +
                        "(@nom_complet,@Email,@telephone_client,@mot_de_passe,@login)";
                    Console.WriteLine("creation client");
                    using (SqlCommand cmd = new SqlCommand(script, con))
                    {
                        cmd.Parameters.AddWithValue("@nom_complet", clt.nomComplet);
                        cmd.Parameters.AddWithValue("@Email", clt.email);
						cmd.Parameters.AddWithValue("@telephone_client", clt.tel);
						cmd.Parameters.AddWithValue("@mot_de_passe", clt.pwd);
                        cmd.Parameters.AddWithValue("@login", clt.login);
						Console.WriteLine("client Reussi2");
                        cmd.ExecuteNonQuery();
					}
                }
				successMessage = "Ajout effectuer avec succes";
				Response.Redirect("Connexion");
                log = clt.login;
                pwd = clt.pwd;
			}
            catch (Exception ex)
            {
                errorMesssage = ex.Message;
            }
		}
    }

}
