using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataLibrary.Operations
{
	public abstract  class CommonConnector
	{
		public Exception Ex { get; set; }
		public SqlException Sqlex { get; set; }
		protected DataTable dt = new DataTable();
		protected static string ConnString(string name = "ClubDBMS")
		{
			return ConfigurationManager.ConnectionStrings[name].ConnectionString;
		}
		
	}
}
