using ApplicationForWorkingWithGoods.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Interfaces {
	public interface IPurchase {
		/// <summary>
		/// Код заявки
		/// </summary>
		int CodePurchase { get; }
		/// <summary>
		/// Код товара
		/// </summary>
		int CodeProduct { get; }
		/// <summary>
		/// Код клиента
		/// </summary>
		int IdClient { get; }
		/// <summary>
		/// Номер заявки
		/// </summary>
		int NumberPurchase { get; }
		/// <summary>
		/// Требуемое количество
		/// </summary>
		int ProductQuantity { get; }
		/// <summary>
		/// Требуемое количество
		/// </summary>
		DateTime DatePlacing { get; }
		/// <summary>
		/// Модель товаров
		/// </summary>
		ProductsModel Products { get; }
		/// <summary>
		/// Модель клиентов
		/// </summary>
		ClientsModel Clients { get; }
	}
}
