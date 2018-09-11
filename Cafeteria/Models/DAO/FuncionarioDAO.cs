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
	public class FuncionarioDAO
    {
        
		string conexao = WebConfigurationManager.ConnectionStrings["BancoCafeteria"].ConnectionString;

        public List<Funcionario> Listar()
        {
            string consulta = "Select * FROM Funcionario ORDER BY Nome";

            using (var conn = new SqlConnection(conexao))
            {
                var cmd = new SqlCommand(consulta, conn);
                List<Funcionario> dados = new List<Funcionario>();
                Funcionario p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            p = new Funcionario();
                            p.Id = (int)reader["Id"];
                            p.Nome = reader["Nome"].ToString();
                            p.Endereco = reader["Endereco"].ToString();
                            p.Login = reader["Login"].ToString();
                            p.Senha = reader["Senha"].ToString();
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
        public List<Funcionario> Get(int Id)
        {
            string consulta = "Select * FROM Funcionario Where Id=" + Id;

            using (var conn = new SqlConnection(conexao))
            {
                var cmd = new SqlCommand(consulta, conn);
                List<Funcionario> dados = new List<Funcionario>();
                Funcionario p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            p = new Funcionario();
                            p.Id = (int)reader["Id"];
                            p.Nome = reader["Nome"].ToString();
                            p.Endereco = reader["Endereco"].ToString();
                            p.Login = reader["Login"].ToString();
                            p.Senha = reader["Senha"].ToString();
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
        public void SalvarPeloObjeto(Funcionario u)
        {
            using (var conn = new SqlConnection(conexao))
            {

                string sql = @"INSERT INTO 
                                Funcionario 
                                    (Nome,Endereco,Login,Senha) 
                                VALUES 
                                    (@Nome,@Endereco,@Login,@Senha)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Nome", u.Nome);
                cmd.Parameters.AddWithValue("@Endereco", u.Endereco);
                cmd.Parameters.AddWithValue("@Login", u.Login);
                cmd.Parameters.AddWithValue("@Senha", u.Senha);

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
        public void Salvar(string Nome, string Endereco, string Login,string Senha)
        {
            using (var conn = new SqlConnection(conexao))
            {
                string sql = @"INSERT INTO 
                                Funcionario 
                                    (Nome,Endereco,Login,Senha) 
                                VALUES 
                                    (@Nome,@Endereco,@Login,@Senha)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Nome", Nome);
                cmd.Parameters.AddWithValue("@Endereco", Endereco);
                cmd.Parameters.AddWithValue("@Login", Login);
                cmd.Parameters.AddWithValue("@Senha", Senha);

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
        public void Editar (int Id, string Endereco, string Login, string Senha)
        {
            using (var conn = new SqlConnection(conexao))
            {
                string sql = @"UPDATE Funcionario 
                                    SET                                 
                                Endereco=@Endereco,
                                Login=@Login,
                                Senha=@Senha
                              Where Id=@Id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@Endereco", Endereco);
                cmd.Parameters.AddWithValue("@Login", Login);
                cmd.Parameters.AddWithValue("@Senha", Senha);

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
                string sql = @"DELETE FROM Funcionario                                
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
