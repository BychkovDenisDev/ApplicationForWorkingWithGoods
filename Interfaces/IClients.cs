using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Interfaces {
	public interface IClients {
		/// <summary>
		/// Код клиента
		/// </summary>
		int IdClient { get; }
		/// <summary>
		/// Наименование организации
		/// </summary>
		string NameOrganization { get; }
		/// <summary>
		/// Адрес
		/// </summary>
		string AdressOrganization { get; }
		/// <summary>
		/// Контактное лицо (ФИО)
		/// </summary>
		string Client { get; }
	}
}
