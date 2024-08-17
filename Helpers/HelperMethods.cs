using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Helpers {
	static class HelperMethods {
		/// <summary>
		/// Создать рандомный номер от 0 до 1111
		/// </summary>
		public static int GetCodeRandom() {
			Random random = new Random();
			return random.Next(0, 1111);
		}
	}
}
