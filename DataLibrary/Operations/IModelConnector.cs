using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLibrary.Operations
{
	public interface IModelConnector<T>
	{
		
		Exception Ex { get; set; }
		SqlException Sqlex { get; set; }
		int Update(T model);
		int Insert(T model);
		int Delete(T model);
        List<T> Load(string input, bool all);
    }
}
