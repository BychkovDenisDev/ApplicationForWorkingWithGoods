using ApplicationForWorkingWithGoods.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationForWorkingWithGoods.Models;
using ApplicationForWorkingWithGoods.Helpers;
using System.Text.RegularExpressions;
using ClosedXML.Excel;
using System.Globalization;
using ApplicationForWorkingWithGoods.Navigation;

namespace ApplicationForWorkingWithGoods {

	public class ExcelReader : IFileReader {
		/// <summary>
		/// Работа с файлом
		/// </summary>
		private IFileWorker _fileWorker;
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
		/// <summary>
		/// Меню навигации
		/// </summary>
		private NavMenu _navMenu;
		public IFileWorker FileWorker => _fileWorker;
		public NavMenu NavMenu => _navMenu;

		/// <summary>
		/// Получить тип файла
		/// </summary>
		/// <param name="filePath"></param> путь до файла
		public bool ReadFile(string filePath) {
			while (true) {
				try {
					_workbook = new XLWorkbook(filePath);
					break;
				} catch (Exception ex) {
					Console.WriteLine($"Ошибка при открытии файла: {ex.Message}");
					Console.WriteLine("Пожалуйста, введите путь к файлу заново: ");
					filePath = Console.ReadLine();
				}
			}
			_productsWorksheet = _workbook.Worksheet("Товары");
			_clientsWorksheet = _workbook.Worksheet("Клиенты");
			_purchaseWorksheet = _workbook.Worksheet("Заявки");
			if (_workbook == null || 
				_productsWorksheet == null ||
				_clientsWorksheet == null || 
				_purchaseWorksheet == null) {
				return false;
			}
			_fileWorker = new ExcelWorker(_workbook, _productsWorksheet, _clientsWorksheet, _purchaseWorksheet);
			_navMenu = new NavMenu(_fileWorker);
			return true;
		}
	}
}
