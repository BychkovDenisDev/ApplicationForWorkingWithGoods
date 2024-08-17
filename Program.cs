using ApplicationForWorkingWithGoods.Interfaces;
using ApplicationForWorkingWithGoods.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods {
	class Program {
		static void Main(string[] args) {
			Console.Write("Введите путь до файла с данными: ");
			string path = Console.ReadLine();

			try {
				bool checkFile = Helpers.FileHelper.CheckFile(path);
				if (checkFile) {
					var file = Helpers.FileHelper.GetFileType(path);
					IFileReader fileReader = Helpers.Creator.Create(file);
					if (fileReader == null) {
						throw new Exception(String.Format("Неверный тип файла!", file));
					}
					var isFileRead = fileReader.ReadFile(path); 
					if (!isFileRead) {
						throw new Exception(String.Format("Некорректный формат Excel файл", file));
					}
					if (fileReader is ExcelReader) {
						var reader = fileReader as ExcelReader;
						reader.NavMenu.Menu();
					}
				}
			} catch (Exception ex) {
				Console.WriteLine(ex.Message);
				Console.ReadKey();
				return;
			}
			Console.ReadKey();
		}
	}
}
