using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Models {
	public class ColumnNameModel {
		public string Name { get; private set; }
		public string Liter { get; private set; }

		public ColumnNameModel(string name, string liter) {
			Name = name;
			Liter = liter;
		}
	}
}
