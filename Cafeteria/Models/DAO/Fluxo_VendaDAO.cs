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
	public class Fluxo_VendaDAO
    {

        string conexao = WebConfigurationManager.ConnectionStrings["BancoCafeteria"].ConnectionString;

        //Update Status
        public void EditarStatus(int Id_Venda, string FormaPagamento)
        {
            using (var conn = new SqlConnection(conexao))
            {
                string sql = @"UPDATE Fluxo_Venda 
                                    SET 
                                Forma_Pagamento=@Forma_Pagamento,
                                status='F'                             
                              Where Id=@Id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", Id_Venda);              
                cmd.Parameters.AddWithValue("@Forma_Pagamento", FormaPagamento);

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

        //Abre uma nova venda com campos vazio
        public void AbrirVenda(int mesa)
        {
            using (var conn = new SqlConnection(conexao))
            {

                string sql = @"INSERT INTO 
                                Fluxo_Venda 
                                    (Data_Venda,Id_Funcionario,status,Valor_Total,Desconto,Valor_Conta,Mesa) 
                                VALUES 
                                    (GETDATE(),@Funcionario,'A','0','0','0',@mesa)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Funcionario", HttpContext.Current.Session["Id_Func"]);
                cmd.Parameters.AddWithValue("@mesa", mesa);

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

        //salva campos do model de adicionar produto
        public void SalvarItens( int Id_Venda,int Id_Produto, int Quantidade)
        {
            using (var conn = new SqlConnection(conexao))
            {
                string sql = @"INSERT INTO 
                                Fluxo_Item 
                                   (Id_FluxoVenda,Id_Produto,Quantidade)
                                VALUES 
                                    (@Venda,@Produto,@Quantidade)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Venda", Id_Venda);
                cmd.Parameters.AddWithValue("@Produto", Id_Produto);
                cmd.Parameters.AddWithValue("@Quantidade", Quantidade);

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

        //listar com JOIN
        public List<Fluxo_Venda> Get()
        {
            string consulta = @"select
                                v.Id
                               ,v.Data_Venda                               
                               ,v.status
                               ,v.Valor_Total
                               ,v.Desconto
                               ,v.Valor_Conta							   
                               ,f.Nome NomeFuncionario
                               ,isnull(v.Mesa,0) Mesa
                                ,v.Forma_Pagamento
                                FROM Fluxo_Venda v
                                JOIN Funcionario f on (f.Id = v.Id_Funcionario)
                                ORDER BY v.Id DESC";


            using (var conn = new SqlConnection(conexao))
            {
                var cmd = new SqlCommand(consulta, conn);
                List<Fluxo_Venda> dados = new List<Fluxo_Venda>();
                Fluxo_Venda p = null;
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            p = new Fluxo_Venda();
                            p.Id = (int)reader["Id"];
                            
                            p.Data_Venda = Convert.ToDateTime(reader["Data_Venda"]).ToString("dd/MM/yyyy");
                            p.NomeFuncionario = reader["NomeFuncionario"].ToString();
                            p.status = reader["status"].ToString();
                            p.Valor_Total = (decimal)reader["Valor_Total"];
                            p.Desconto = Convert.ToDecimal(reader["Desconto"]);
                            p.Valor_Conta = (decimal)reader["Valor_Conta"];
                            p.Mesa = (int)reader["Mesa"];
                            p.Forma_Pagamento = reader["Forma_Pagamento"].ToString();
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

        //listar comanda com oque foi inserido
        public List<Produto> CarregaTodaComanda(int Id_Venda)
        {
            string consulta = @"select
                                v.Id,
                                p.Descricao,
                                p.Valor,
                                fi.Quantidade,
                                (p.Valor * Quantidade) as Total
                                from Fluxo_Item fi
                                join Fluxo_Venda v on (v.Id = fi.Id_FluxoVenda)
                                join Produto p on (p.Id = fi.Id_Produto)
                                where v.Id= " + Id_Venda;

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
                            p.Descricao = reader["Descricao"].ToString();
                            p.Valor = reader["Valor"].ToString();
                            p.Quantidade = (int)reader["Quantidade"];
                            p.Total = (decimal)reader["Total"];

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

        //Salvando pelos campos do formulario
        public void Salvar(DateTime Data_Venda, int Id_Funcionario, string status, decimal Valor_Total, decimal Desconto, decimal Valor_Conta)
        {
            using (var conn = new SqlConnection(conexao))
            {
                string sql = @"INSERT INTO 
                                Fluxo_Venda 
                                   (Data_Venda,Id_Funcionario,status,Valor_Total,Desconto,Valor_Conta) 
                                VALUES 
                                    (@Data_Venda,@Id_Funcionario,@status,@Valor_Total,@Desconto,@Valor_Conta)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Data_Venda", Data_Venda);
                cmd.Parameters.AddWithValue("@Id_Funcionario", Id_Funcionario);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@Valor_Total", Valor_Total);
                cmd.Parameters.AddWithValue("@Desconto", Desconto);
                cmd.Parameters.AddWithValue("@Valor_Conta", Valor_Conta);

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
                string sql = @"DELETE FROM Fluxo_Venda                                
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

        //PegaValorTotal
        public decimal PegaValorTotal(int Id_Venda)
        {
            decimal total = 0;

            string consulta = @"Select
                                   IsNull(sum(p.Valor * Quantidade),0) as Total
                                from Fluxo_Item fi
                                join Fluxo_Venda v on (v.Id = fi.Id_FluxoVenda)
                                join Produto p on (p.Id = fi.Id_Produto)
                                where v.Id = " + Id_Venda;

            using (var conn = new SqlConnection(conexao))
            {
                var cmd = new SqlCommand(consulta, conn);   
                try
                {
                    conn.Open();
                    using (var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                    {
                        while (reader.Read())
                        {
                            total = Convert.ToDecimal(reader["Total"]);
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
                return total;
            }
        }

        //salvaTotal
        public void SalvarTotalConta(int Id_Venda, decimal valor)
        {
            using (var conn = new SqlConnection(conexao))
            {
                string sql = @"UPDATE Fluxo_Venda SET  Valor_Total=@Valor where Id =@Id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@Id", Id_Venda);
                cmd.Parameters.AddWithValue("@Valor", valor);
                

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