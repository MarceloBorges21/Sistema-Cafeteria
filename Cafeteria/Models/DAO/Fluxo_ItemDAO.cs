using Cafeteria.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Cafeteria.Models.DAO
{
	public class Fluxo_ItemDAO
	{
		      
		string conexao = WebConfigurationManager.ConnectionStrings["BancoCafeteria"].ConnectionString;

		public List<Fluxo_Item> Listar()
		{
			string consulta = "Select * FROM Fluxo_Item";

			using (var conn = new SqlConnection(conexao))
			{
				var cmd = new SqlCommand(consulta, conn);
				List<Fluxo_Item> dados = new List<Fluxo_Item>();
				Fluxo_Item p = null;
				try
				{
					conn.Open();
					using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						while (reader.Read())
						{
							p = new Fluxo_Item();
							p.Id = (int)reader["Id"];
							p.Id_FluxoVenda = (int)reader["Id_FluxoVenda"];
                            p.Id_Produto = (int)reader["Id_Produto"];
							p.Quantidade = (int)reader["Quantidade"];
							dados.Add(p);
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
				return dados;
			}
		}

        //Salvando pelo Objeto do formulario
        public void SalvarPeloObjeto(Fluxo_Item u)
		{
			using (var conn = new SqlConnection(conexao))
			{

				string sql = @"INSERT INTO 
                                Fluxo_Item 
                                    (Id_Produto,Quantidade) 
                                VALUES 
                                    (@Id_Produto,@Quantidade)";
				SqlCommand cmd = new SqlCommand(sql, conn);

				cmd.Parameters.AddWithValue("@Id_Produto", u.Id_Produto);
				cmd.Parameters.AddWithValue("@Quantidade", u.Quantidade);

				try
				{
					conn.Open();
					cmd.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					throw e;
				}
			}


		}
		
		//Delete
		public void Excluir(int Id)
		{
			using (var conn = new SqlConnection(conexao))
			{
				string sql = @"DELETE FROM Fluxo_Item                                
                              Where Id=@Id";

				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.Parameters.AddWithValue("@Id", Id);

				try
				{
					conn.Open();
					cmd.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					throw e;
				}
			}
		}
	}
}
