using ApplicationForWorkingWithGoods.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Models {
	public class ClientsModel : IClients {
		/// <summary>
		/// Код клиента
		/// </summary>
		public int IdClient { get; private set; }
		/// <summary>
		/// Наименование организации
		/// </summary>
		public string NameOrganization { get; private set; }
		/// <summary>
		/// Адрес
		/// </summary>
		public string AdressOrganization { get; private set; }
		/// <summary>
		/// Контактное лицо (ФИО)
		/// </summary>
		public string Client { get; private set; }

		public ClientsModel(int idClient, string nameOrganization, string adressOrganization, string client) {
			IdClient = idClient;
			NameOrganization = nameOrganization;
			AdressOrganization = adressOrganization;
			Client = client;
		}
	}
}
