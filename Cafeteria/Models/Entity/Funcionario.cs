using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.Models.Entity
{
	public class Funcionario
	{
		public int Id { get; set; }
		public string Nome { get; set; }
		public string Endereco { get; set; }
		public string Login { get; set; }
		public string Senha { get; set; }

	}
}