using Cafeteria.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace Cafeteria.Models.DAO
{
    public class LoginDAO
    {
        public string Logar(string login,string Senha)
        {
            string retorno = "";
            string conexao = WebConfigurationManager.ConnectionStrings["BancoCafeteria"].ConnectionString;


            string consulta = "Select * FROM Funcionario Where Login='" + login +"'and Senha='"+Senha+"'";

            using (var conn = new SqlConnection(conexao))
            {
                var cmd = new SqlCommand(consulta, conn);
                List<LoginSenha> dados = new List<LoginSenha>();
                LoginSenha p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            p = new LoginSenha();
                            p.Id = (int)reader["Id"];
                            p.login = reader["Login"].ToString();
                            p.Senha = reader["Senha"].ToString();
                            HttpContext.Current.Session["login"] = reader["Nome"].ToString();
                            HttpContext.Current.Session["Id_Func"] = reader["Id"].ToString();
                            dados.Add(p);

                            retorno = "Entrou";
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    conn.Close();
                }
                return retorno;
            }
        }

		public string Sair(string login, string Senha)
		{
			string retorno = "";
			string conexao = WebConfigurationManager.ConnectionStrings["BancoCafeteria"].ConnectionString;


			string consulta = "Select * FROM Funcionario Where Login='0'and Senha='0'";

			using (var conn = new SqlConnection(conexao))
			{
				var cmd = new SqlCommand(consulta, conn);
				List<LoginSenha> dados = new List<LoginSenha>();
				LoginSenha p = null;
				try
				{
					conn.Open();
					using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						while (reader.Read())
						{
							p = new LoginSenha();
							p.Id = (int)reader["Id"];
							p.login = reader["Login"].ToString();
							p.Senha = reader["Senha"].ToString();
							HttpContext.Current.Session["login"] = reader["Nome"].ToString();
							HttpContext.Current.Session["Id_Func"] = reader["Id"].ToString();
							dados.Add(p);

							retorno = "Entrou";
						}
					}
				}
				catch (Exception ex)
				{
					throw new Exception(ex.Message);
				}
				finally
				{
					conn.Close();
				}
				return retorno;
			}
		}
	}
}