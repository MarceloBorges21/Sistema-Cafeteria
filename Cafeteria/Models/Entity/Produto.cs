using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.Models.Entity
{
	public class Produto
	{
		public int Id { get; set; }
		public string Descricao { get; set; }
		public string Valor { get; set; }
        public string Data { get; set; }
        public int Quantidade { get; set; }
        public decimal Total { get; set; }

    }
}