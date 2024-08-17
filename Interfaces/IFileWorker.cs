using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Interfaces {
	public interface IFileWorker {
		/// <summary>
		/// По наименованию товара вывести информацию о клиентах, заказавших этот товар, 
		/// с указанием информации по количеству товара, цене и дате заказа.
		/// </summary>
		void SelectProductInPurchase();
		/// <summary>
		/// Запрос на изменение контактного лица клиента с указанием параметров
		/// </summary>
		void UpdateDataClient();
		/// <summary>
		/// Запрос на определение золотого клиента
		/// </summary>
		void FindGoldPerson();
		/// <summary>
		/// Добавление новых значений в лист Товары
		/// </summary>
		void InsertDataInProductsWorksheet();
		/// <summary>
		/// Добавление новых значений в лист Клиенты
		/// </summary>
		void InsertDataInClientsWorksheet();
		/// <summary>
		/// Добавление новых значений в лист Заказы
		/// </summary>
		void InsertDataInPurchaseWorksheett();
	}
}
