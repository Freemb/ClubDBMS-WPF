using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Operations
{
	public class DeliveryConnector : CommonConnector, IModelConnector<DeliveryModel>
	{
		public DataTable Load(string input)
		{
			throw new NotImplementedException();
		}
		public int Delete(DeliveryModel model)
		{
			throw new NotImplementedException();
		}

		public int Insert(DeliveryModel model)
		{
			throw new NotImplementedException();
		}
		
		public int Update(DeliveryModel model)
		{
			throw new NotImplementedException();
		}
	}
}
