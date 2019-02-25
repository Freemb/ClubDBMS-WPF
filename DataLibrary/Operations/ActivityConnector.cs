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
					//da.FillSchema(dt,SchemaType.Source);
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
		public List<ActivityModel> Load(bool all)
		{
			using (SqlConnection conn = new SqlConnection(ConnString()))
			{
				using (SqlDataAdapter da = new SqlDataAdapter("dbo.spLoadActivityPrices", conn))
				{
					da.SelectCommand.CommandType = CommandType.StoredProcedure;
					da.SelectCommand.Parameters.AddWithValue("@Prices", all);
					
					//da.FillSchema(dt,SchemaType.Source);
					dt.TableName = "Activities";
					da.Fill(dt);
					#region ConvertToModels
					List<ActivityModel> output = dt.AsEnumerable().Select(row => new ActivityModel()
					{
						ActivityID = row.Field<int>("ActivityId"),
						ActivityName = row.Field<string>("Activity"),
						ActivityType = row.Field<string>("Type"),
						SubActivity = row.Field<string>("SubActivity"),
						SubActivityID = row.Field<int>("SubActivityId"),
						Price = row.Field<decimal>("Price"),
						IsWEBH = row.Field<bool>("WkBH")

					}).ToList();
					#endregion
					return output;
				}
			}

		}
	}
}
