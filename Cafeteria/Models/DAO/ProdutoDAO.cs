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
	public class ProdutoDAO
	{
		string conexao = WebConfigurationManager.ConnectionStrings["BancoCafeteria"].ConnectionString;

		public List<Produto> Listar()
		{
			string consulta = "Select * FROM Produto ORDER BY Descricao";

			using (var conn = new SqlConnection(conexao))
			{
				var cmd = new SqlCommand(consulta, conn);
				List<Produto> dados = new List<Produto>();
				Produto p = null;
				try
				{
					conn.Open();
					using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
					{
						while (reader.Read())
						{
							p = new Produto();
							p.Id = (int)reader["Id"];
							p.Descricao = reader["Descricao"].ToString().ToUpper();
							p.Valor = reader["Valor"].ToString();
                            p.Data = Convert.ToDateTime(reader["Data"]).ToString("dd/MM/yyyy");
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

        //listando pelo Id (para preencher o formulario para edição)
        public List<Produto> Get(int Id)
        {
            string consulta = "Select * FROM Produto Where Id=" + Id;

            using (var conn = new SqlConnection(conexao))
            {
                var cmd = new SqlCommand(consulta, conn);
                List<Produto> dados = new List<Produto>();
                Produto p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            p = new Produto();
                            p.Id = (int)reader["Id"];
                            p.Descricao = reader["Descricao"].ToString().ToUpper();
                            p.Valor =reader["Valor"].ToString();
                            p.Data = Convert.ToDateTime(reader["Data"]).ToString("dd/MM/yyyy");
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
        public void SalvarPeloObjeto(Produto u)
        {
            using (var conn = new SqlConnection(conexao))
            {

                string sql = @"INSERT INTO 
                                Produto 
                                    (Descricao,Valor,Data) 
                                VALUES 
                                    (@Descricao,@Valor,@Data)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Descricao", u.Descricao.ToUpper());
                cmd.Parameters.AddWithValue("@Valor", u.Valor);
                cmd.Parameters.AddWithValue("@Data", Convert.ToDateTime(u.Data).ToString("yyyy-MM-dd"));
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

        //Salvando pelos campos do formulario
        public void Salvar(string Descricao,  decimal Valor, DateTime Data)
        {
            using (var conn = new SqlConnection(conexao))
            {
                string sql = @"INSERT INTO 
                                Produto 
                                    (Descricao,Valor,Data) 
                                VALUES 
                                    (@Descricao,@Valor,@Data)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Descricao", Descricao);
                cmd.Parameters.AddWithValue("@Valor", Valor);
                cmd.Parameters.AddWithValue("@Data", Data);


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

        //Update
        public void Editar(int Id, string Valor)
        {
            using (var conn = new SqlConnection(conexao))
            {
                string sql = @"UPDATE Produto 
                                    SET                                            
                                Valor=@Valor                                       
                              Where Id=@Id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Valor",Valor);
               

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
        public void Excluir( int Id)
        {
            using (var conn = new SqlConnection(conexao))
            {
                string sql = @"DELETE FROM Produto                                
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
	
