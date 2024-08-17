using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationForWorkingWithGoods.Interfaces;
using ApplicationForWorkingWithGoods.Models;
using ApplicationForWorkingWithGoods.Helpers;
using ClosedXML.Excel;
using System.Globalization;
using ApplicationForWorkingWithGoods.Navigation;

namespace ApplicationForWorkingWithGoods {
	public class ExcelWorker : IFileWorker {
		/// <summary>
		/// Рабочая книга 
		/// </summary>
		private XLWorkbook _workbook;
		/// <summary>
		/// Имя листа товары (откуда будем читать данные) 
		/// </summary>
		private IXLWorksheet _productsWorksheet;
		/// <summary>
		/// Имя листа клиенты (откуда будем читать данные) 
		/// </summary>
		private IXLWorksheet _clientsWorksheet;
		/// <summary>
		/// Имя листа заказы (откуда будем читать данные) 
		/// </summary>
		private IXLWorksheet _purchaseWorksheet;

		public ExcelWorker(XLWorkbook workbook, IXLWorksheet productsWorksheet, IXLWorksheet clientsWorksheet, IXLWorksheet purchaseWorksheet) {
			_workbook = workbook;
			_productsWorksheet = productsWorksheet;
			_clientsWorksheet = clientsWorksheet;
			_purchaseWorksheet = purchaseWorksheet;
		}

		/// <summary>
		/// По наименованию товара вывести информацию о клиентах, заказавших этот товар, 
		/// с указанием информации по количеству товара, цене и дате заказа.
		/// </summary>
		public void SelectProductInPurchase() {
			Console.Write("Введите наименование товара: ");
			string writeNameProduct = Console.ReadLine().ToLower(); //Переводим в нижний регистр

			bool isProductFound = false;
			var productsRows = _productsWorksheet.RowsUsed().Skip(1);

			foreach (var productRow in productsRows) {
				if (productRow.Cell("B").Value.ToString().ToLower() == writeNameProduct) {
					ProductsModel product = new ProductsModel
					(
						int.Parse(productRow.Cell("A").Value.ToString()),
						productRow.Cell("B").Value.ToString(),
						productRow.Cell("C").Value.ToString(),
						double.Parse(productRow.Cell("D").Value.ToString())
					);

					List<PurchaseModel> purchases = new List<PurchaseModel>();

					var purchaseRows = _purchaseWorksheet.RowsUsed().Skip(1);
					foreach (var purchaseRow in purchaseRows) {
						if (purchaseRow.Cell("B").Value.ToString() == product.CodeProduct.ToString()) {
							PurchaseModel purchase = new PurchaseModel
							(
								int.Parse(purchaseRow.Cell("A").Value.ToString()),
								int.Parse(purchaseRow.Cell("B").Value.ToString()),
								int.Parse(purchaseRow.Cell("C").Value.ToString()),
								int.Parse(purchaseRow.Cell("D").Value.ToString()),
								int.Parse(purchaseRow.Cell("E").Value.ToString()),
								DateTime.ParseExact(purchaseRow.Cell("F").Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture)
							);
							purchase.Products = product;
							purchases.Add(purchase);
						}
					}

					foreach (var purchase in purchases) {
						var clientsRows = _clientsWorksheet.RowsUsed().Skip(1);
						foreach (var clientRow in clientsRows) {
							if (clientRow.Cell("A").Value.ToString() == purchase.IdClient.ToString()) {
								ClientsModel client = new ClientsModel
								(
									int.Parse(clientRow.Cell("A").Value.ToString()),
									clientRow.Cell("B").Value.ToString(),
									clientRow.Cell("C").Value.ToString(),
									clientRow.Cell("D").Value.ToString()
								);
								purchase.Clients = client;
								break;
							}
						}
					}

					Console.WriteLine($"\nКоличество заказов товара {product.Product}: {purchases.Count}\n");

					foreach (var purchase in purchases) {
						Console.WriteLine($"Клиент: {purchase.Clients?.Client ?? "данные о клиенте не найдены в файле"}");
						Console.WriteLine($"Количество товара: {purchase.ProductQuantity} {purchase.Products.UnitsMeasurements}");
						Console.WriteLine($"Цена за {purchase.Products.UnitsMeasurements.ToLower()}: {purchase.Products.Price}");
						Console.WriteLine($"Итог: {purchase.ProductQuantity * purchase.Products.Price}");
						Console.WriteLine($"Дата заказа: {purchase.DatePlacing.ToShortDateString()}\n");
					}

					isProductFound = true;
					break;
				}
			}

			if (!isProductFound)
				Console.WriteLine("\nТовара с подобным названием в таблице нет\n");
		}

		/// <summary>
		/// Запрос на изменение контактного лица клиента с указанием параметров
		/// </summary>
		public void UpdateDataClient() {
			Console.Write("Введите наименование организации, где Вы хотите заменить контактное лицо: ");
			var newOrgName = Console.ReadLine().ToLower();
			Console.Write("Введите новое ФИО контактного лица: ");
			var newPerson = Console.ReadLine();
			bool check = false;

			var clientsRows = _clientsWorksheet.RowsUsed().Skip(1);
			foreach (var clientRow in clientsRows) {
				if (clientRow.Cell("B").Value.ToString().ToUpper().Contains(newOrgName)) {
					if (clientRow.Cell("B").Value.ToString().ToUpper() != newOrgName) {
						Console.WriteLine($"В файле есть организация с похожим наименование: {clientRow.Cell("B").Value}");
					} else {
						check = true;
					}

					if (check) {
						Console.WriteLine($"\nСтарое контактное лицо {clientRow.Cell("B").Value}: {clientRow.Cell("D").Value}");
						clientRow.Cell("D").Value = newPerson;
						Console.WriteLine($"Новое контактное лицо {clientRow.Cell("B").Value}: {clientRow.Cell("D").Value}");
						_workbook.Save();
						Console.WriteLine("Изменения сохранены\n");
						break;
					}
				}
			}

			if (!check) {
				Console.WriteLine("\nОрганизация с данным наименованием не найдена в таблице\n");
			}
		}

		/// <summary>
		/// Запрос на определение золотого клиента
		/// </summary>
		public void FindGoldPerson() {
			Console.WriteLine("Поиск клиента с наибольшим числом заказов за указанный период");
			int year;
			int month;
			bool correctYear = false;
			bool correctMonth = false;

			Console.Write("Введите год: ", false);
			correctYear = int.TryParse(Console.ReadLine(), out year);
			Console.Write("Введите месяц: ", false);
			correctMonth = int.TryParse(Console.ReadLine(), out month);

			var purchaseClientCount = new Dictionary<int, int>();
			var ordersRows = _purchaseWorksheet.RowsUsed().Skip(1);

			foreach (var orderRow in ordersRows) {
				DateTime date = DateTime.ParseExact(orderRow.Cell("F").Value.ToString(), "dd.MM.yyyy H:mm:ss", CultureInfo.InvariantCulture);

				if (date.Year == year && date.Month == month) {
					int clientId = int.Parse(orderRow.Cell("C").Value.ToString());
					if (purchaseClientCount.ContainsKey(clientId)) {
						purchaseClientCount[clientId]++;
					} else {
						purchaseClientCount.Add(clientId, 1);
					}
				}
			}

			if (purchaseClientCount.Count == 0) {
				Console.WriteLine("\nПо заданным критериям ничего не найдено!\n");
				return;
			}

			int maxPurchaseCount = purchaseClientCount.Max(x => x.Value);
			List<int> IdMaxPurchaseCount = purchaseClientCount.Where(x => x.Value == maxPurchaseCount).Select(x => x.Key).ToList();
			List<ClientsModel> clientsMaxPurchase = new List<ClientsModel>();

			var clientsRows = _clientsWorksheet.RowsUsed().Skip(1);

			foreach (var clientRow in clientsRows) {
				foreach (var id in IdMaxPurchaseCount) {
					if (int.Parse(clientRow.Cell("A").Value.ToString()) == id) {
						clientsMaxPurchase.Add(new ClientsModel
						(
							int.Parse(clientRow.Cell("A").Value.ToString()),
							clientRow.Cell("B").Value.ToString(),
							clientRow.Cell("C").Value.ToString(),
							clientRow.Cell("D").Value.ToString()
						));
					}
				}
			}

			if (clientsMaxPurchase.Count != 0) {
				Console.WriteLine($"\nМаксимальное количество заказов: {maxPurchaseCount}");
				Console.WriteLine($"Клиентов с максимальным числом заказов: {clientsMaxPurchase.Count}\n");

				foreach (var client in clientsMaxPurchase) {
					Console.WriteLine($"Наименование организации: {client.NameOrganization}");
					Console.WriteLine($"Адрес организации: {client.AdressOrganization}");
					Console.WriteLine($"Контактное лицо: {client.Client}\n");
				}
			} else
				Console.WriteLine("\nНе удалось найти клиента с максимальным числом заказов\n");
		}

		/// <summary>
		/// Добавление новых значений в лист Товары
		/// </summary>
		public void InsertDataInProductsWorksheet() {
			Console.Write("Введите наименование товара: ");
			var newProductName = Console.ReadLine().ToLower();
			Console.Write("Введите единицу измерения: ");
			var newUnitsMeasurements = Console.ReadLine();
			Console.Write("Введите цену товара: ");
			var newPrice = Console.ReadLine();
			var newCode = HelperMethods.GetCodeRandom();
			var firstEmptyProductsRow = _productsWorksheet.LastRowUsed().RowBelow();

			firstEmptyProductsRow.Cell("A").Value = newCode;
			firstEmptyProductsRow.Cell("B").Value = newProductName;
			firstEmptyProductsRow.Cell("C").Value = newUnitsMeasurements;
			firstEmptyProductsRow.Cell("D").Value = newPrice;
			_workbook.Save();
			Console.WriteLine("Изменения сохранены\n");
		}

		/// <summary>
		/// Добавление новых значений в лист Клиенты
		/// </summary>
		public void InsertDataInClientsWorksheet() {
			Console.Write("Введите наименование организации: ");
			var newNameOrganization = Console.ReadLine().ToLower();
			Console.Write("Введите адресс организации: ");
			var newAdressOrganization = Console.ReadLine();
			Console.Write("Введите контактное лицо: ");
			var newClient = Console.ReadLine();
			var newIdClient = HelperMethods.GetCodeRandom();
			var firstEmptyclientsRow = _clientsWorksheet.LastRowUsed().RowBelow();

			firstEmptyclientsRow.Cell("A").Value = newIdClient;
			firstEmptyclientsRow.Cell("B").Value = newNameOrganization;
			firstEmptyclientsRow.Cell("C").Value = newAdressOrganization;
			firstEmptyclientsRow.Cell("D").Value = newClient;
			_workbook.Save();
			Console.WriteLine("Изменения сохранены\n");
		}
		/// <summary>
		/// Добавление новых значений в лист Заказы
		/// </summary>
		public void InsertDataInPurchaseWorksheett() {
			Console.Write("Введите код товара: ");
			var сodeProduct = Console.ReadLine().ToLower();
			Console.Write("Введите код клиента: ");
			var idClient = Console.ReadLine();
			Console.Write("Введите требуемое количество товара: ");
			var newProductQuantity = Console.ReadLine();
			var newCodePurchase = HelperMethods.GetCodeRandom();
			var newNumberPurchase = HelperMethods.GetCodeRandom();
			DateTime newDatePlacing = DateTime.Now;

			bool checkIdClient = false;
			bool checkCodeProduct = false;

			var firstEmptyclientsRow = _clientsWorksheet.LastRowUsed().RowBelow();
			var clientsRows = _clientsWorksheet.RowsUsed().Skip(1);
			var productsRows = _productsWorksheet.RowsUsed().Skip(1);

			foreach (var clientRow in clientsRows) {
				if (clientRow.Cell("C").Value.ToString().Contains(idClient)) {
					if (clientRow.Cell("C").Value.ToString().ToUpper() != idClient) {
						Console.WriteLine($"Указанный код клиента не найдет во вкладке Клиенты: {idClient}");
						break;
					} else {
						checkIdClient = true;
					}

					if (checkIdClient) {
						firstEmptyclientsRow.Cell("C").Value = idClient;
					}
				}
			}

			foreach (var productRow in productsRows) {
				if (productRow.Cell("B").Value.ToString().Contains(idClient)) {
					if (productRow.Cell("B").Value.ToString().ToUpper() != сodeProduct) {
						Console.WriteLine($"Указанный код товара не найдет во вкладке Товары: {сodeProduct}");
						break;
					} else {
						checkCodeProduct = true;
					}

					if (checkCodeProduct) {
						firstEmptyclientsRow.Cell("B").Value = сodeProduct;
					}
				}
			}

			firstEmptyclientsRow.Cell("A").Value = newCodePurchase;
			firstEmptyclientsRow.Cell("D").Value = newNumberPurchase;
			firstEmptyclientsRow.Cell("E").Value = newProductQuantity;
			firstEmptyclientsRow.Cell("F").Value = newDatePlacing.ToShortDateString();
			_workbook.Save();
			Console.WriteLine("Изменения сохранены\n");
		}
	}
}
