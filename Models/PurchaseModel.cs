using ApplicationForWorkingWithGoods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Models {
	public class PurchaseModel : IPurchase {
		/// <summary>
		/// Код заявки
		/// </summary>
		public int CodePurchase { get; private set; }
		/// <summary>
		/// Код товара
		/// </summary>
		public int CodeProduct { get; private set; }
		/// <summary>
		/// Код клиента
		/// </summary>
		public int IdClient { get; private set; }
		/// <summary>
		/// Номер заявки
		/// </summary>
		public int NumberPurchase { get; private set; }
		/// <summary>
		/// Требуемое количество
		/// </summary>
		public int ProductQuantity { get; private set; }
		/// <summary>
		/// Дата размещения
		/// </summary>
		public DateTime DatePlacing { get; private set; }
		/// <summary>
		/// Модель товаров
		/// </summary>
		public ProductsModel Products { get; set; }
		/// <summary>
		/// Модель клиентов
		/// </summary>
		public ClientsModel Clients { get; set; }

		public PurchaseModel(int codePurchase, int codeProduct, int idClient, int numberPurchase, int productQuantity, DateTime datePlacing) {
			CodePurchase = codePurchase;
			CodeProduct = codeProduct;
			IdClient = idClient;
			NumberPurchase = numberPurchase;
			ProductQuantity = productQuantity;
			DatePlacing = datePlacing;
		}
	}
}
