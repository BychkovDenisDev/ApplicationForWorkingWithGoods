using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Interfaces {
	public interface IProducts {
		/// <summary>
		/// Код товара
		/// </summary>
		int CodeProduct { get; }
		/// <summary>
		/// Наименование
		/// </summary>
		string Product { get; }
		/// <summary>
		/// Ед. измерения
		/// </summary>
		string UnitsMeasurements { get; }
		/// <summary>
		/// Цена товара за единицу
		/// </summary>
		double Price { get; }
	}
}
