using ApplicationForWorkingWithGoods.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Models {
	public class ProductsModel : IProducts {
		/// <summary>
		/// Код товара
		/// </summary>
		public int CodeProduct { get; private set; }
		/// <summary>
		/// Наименование
		/// </summary>
		public string Product { get; private set; }
		/// <summary>
		/// Ед. измерения
		/// </summary>
		public string UnitsMeasurements { get; private set; }
		/// <summary>
		/// Цена товара за единицу
		/// </summary>
		public double Price { get; private set; }

		public ProductsModel(int codeProduct, string product, string unitsMeasurements, double price) {
			CodeProduct = codeProduct;
			Product = product;
			UnitsMeasurements = unitsMeasurements;
			Price = price;
		}
	}
}
