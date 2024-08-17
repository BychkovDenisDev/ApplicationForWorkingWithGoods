using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Helpers {
	static class FileHelper {
		/// <summary>
		/// Получить тип файла
		/// </summary>
		/// <param name="path"></param> путь до файла
		public static string GetFileType(string path) {
			string extension = Path.GetExtension(path);
			return extension;
		}
		/// <summary>
		/// Проверить, что файл существует
		/// </summary>
		/// <param name="path"></param> путь до файла
		public static bool CheckFile(string path) {
			if (String.IsNullOrWhiteSpace(path) || !File.Exists(path)) {
				throw new Exception(String.Format("Такого файла \"{0}\", не существует!", path));
			}
			return true;
		}
	}
}
