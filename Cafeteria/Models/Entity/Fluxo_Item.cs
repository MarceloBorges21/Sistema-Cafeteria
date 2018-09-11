using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cafeteria.Models.Entity
{
    public class Fluxo_Item
    {
        public int Id { get; set; }
        public int Id_FluxoVenda{ get; set; }
        public int Id_Produto { get; set; }
		public int Quantidade { get; set; }

	}
}