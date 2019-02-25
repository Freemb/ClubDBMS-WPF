using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DataLibrary.Models;
using DataLibrary.Cache;

namespace DataLibrary.Operations
{
	public class ActivityConnector : CommonConnector, IModelConnector<ActivityModel>
	{
		
		public int Delete(ActivityModel model)
		{
			throw new NotImplementedException();
		}

		public int Insert(ActivityModel model)
		{
			throw new NotImplementedException();
		}

		public DataTable Load(string input)
		{
			using (SqlConnection conn = new SqlConnection(ConnString()))
			{
				using (SqlDataAdapter da = new SqlDataAdapter("dbo.spLoadActivityPrices", conn))
				{
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					if (input == null)
					{
						da.SelectCommand.Parameters.AddWithValue("@Prices", true);
					}
					else
					{
						da.SelectCommand.Parameters.AddWithValue("@Prices", false);
					}
					da.FillSchema(dt,SchemaType.Source);
					dt.TableName = "Activities";
					da.Fill(dt);
					return dt;
				}
			}
				
		}

		public int Update(ActivityModel model)
		{
			throw new NotImplementedException();
		}
		
	}
}
