using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.Models.Entity
{
	public class Fluxo_Venda
	{
		public int Id { get; set; }
		public string Data_Venda { get; set; }
		public int Id_Funcionario { get; set; }
        public string NomeFuncionario { get; set; }
        public string status { get; set; }
		public decimal Valor_Total { get; set; }
		public decimal Desconto { get; set; }
		public decimal Valor_Conta { get; set; }
        public int Mesa { get; set; }
        public string Forma_Pagamento { get; set; }
	}
}