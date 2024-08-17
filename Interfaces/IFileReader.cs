using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationForWorkingWithGoods.Interfaces {
	public interface IFileReader {
		bool ReadFile(string path);
	}
}
