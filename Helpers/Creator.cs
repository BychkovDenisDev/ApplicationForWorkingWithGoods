using ApplicationForWorkingWithGoods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Helpers {
	static class Creator {

		/// <summary>
		/// тип документа
		/// </summary>
		private const string FileType = ".xlsx";
		/// <summary>
		/// Прочитать файл
		/// </summary>
		/// <param name="fileType"></param> тип файла
		public static IFileReader Create(string fileType) {

			if (fileType == FileType) {
				return new ExcelReader();
			} else {
				return null;
			}
		}
	}
}
