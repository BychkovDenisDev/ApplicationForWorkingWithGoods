using ApplicationForWorkingWithGoods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Navigation {
	public class NavMenu {

		private readonly Action _actionSelect;
		private readonly Action _update;
		private readonly Action _find;
		private readonly Action _insertDataInProducts;
		private readonly Action _insertDataInClients;
		private readonly Action _insertDataInPurchase;

		public NavMenu(IFileWorker fileWorker) {
			_actionSelect = fileWorker.SelectProductInPurchase;
			_update = fileWorker.UpdateDataClient;
			_find = fileWorker.FindGoldPerson;
			_insertDataInProducts = fileWorker.InsertDataInProductsWorksheet;
			_insertDataInClients = fileWorker.InsertDataInClientsWorksheet;
			_insertDataInPurchase = fileWorker.InsertDataInPurchaseWorksheett;
		}

		/// <summary>
		/// Меню навигации
		/// </summary>
		public void Menu() {
			while (true) {
				Console.WriteLine("Выберите команду действия: ");
				Console.WriteLine("[1] Вывести информацию по заказам клиентов");
				Console.WriteLine("[2] Изменить контактное лица клиента");
				Console.WriteLine("[3] Вывести информацию по золотому клиенту");
				Console.WriteLine("[4] Добавить новые данные в файл");
				Console.WriteLine("[5] Завершить работу");
				Console.Write("Строка для ввода: ");
				string cmd = Console.ReadLine();

				switch (cmd) {
					case "1":
						_actionSelect();
						break;
					case "2":
						_update();
						break;
					case "3":
						_find();
						break;
					case "4":
						Console.WriteLine("Укажите имя листа, где необходимо добавить новые данные:");
						Console.WriteLine("1. Товары");
						Console.WriteLine("2. Клиенты");
						Console.WriteLine("3. Заявки");
						Console.Write("Строка для ввода: ");
						string cmdIsertData = Console.ReadLine();
						if (cmdIsertData == "1") {
							_insertDataInProducts();
						} else if (cmdIsertData == "2") {
							_insertDataInClients();
						} else if (cmdIsertData == "3") {
							_insertDataInPurchase();
						}
						break;
					case "5":
						return;
					default:
						Console.WriteLine("Такой команды нет, попробуйте снова");
						break;
				}
			}
		}
	}
}
