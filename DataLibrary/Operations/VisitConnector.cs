using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLibrary.Models;
using DataLibrary.Cache;
using System.Windows;

namespace DataLibrary.Operations
{
	public class VisitConnector : CommonConnector ,IModelConnector<VisitModel>
	{
	
		public bool Delete(VisitModel model)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnString()))
				{
					using (var cmd = new SqlCommand("dbo.spDeleteVisit", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@VisitID", model.ID);
                        cmd.Parameters.Add(new SqlParameter { Direction = ParameterDirection.Output, ParameterName = "@flag", SqlDbType = SqlDbType.Int });
                        conn.Open();
                        cmd.ExecuteScalar();
                        bool flag = cmd.Parameters["@flag"].Value != DBNull.Value ? Convert.ToBoolean(cmd.Parameters["@flag"].Value) : false;
                        this.Ex = null;
                        return flag;
                    }
				}
			}
			catch (Exception ex)
			{
				this.Ex = ex;
				return false;
			}
		}

		public int Insert(VisitModel model)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(ConnString()))
				{

					SqlCommand cmd = new SqlCommand("dbo.spUpsertVisit", connection)
					{
						CommandType = CommandType.StoredProcedure
					};
					cmd.Parameters.AddWithValue("@VisitDate", model.VisitDate);
					cmd.Parameters.AddWithValue("@MemNo", model.Member.MemNo);
					cmd.Parameters.AddWithValue("@GuestFore", model.Guest.Forename);
					cmd.Parameters.AddWithValue("@GuestSur", model.Guest.Surname);
					cmd.Parameters.AddWithValue("@Activity", model.Activity.ActivityName);
					cmd.Parameters.AddWithValue("@SubActivity", model.Activity.SubActivity);
					cmd.Parameters.AddWithValue("@SubActivityID", model.Activity.SubActivityID);
					cmd.Parameters.AddWithValue("@Amount", model.Amount);
					cmd.Parameters.AddWithValue("@PaidDate", model.PaidDate);
					cmd.Parameters.AddWithValue("@Paid", model.IsPaid);
					cmd.Parameters.AddWithValue("@Notes", model.Notes);
					cmd.Parameters.Add(new SqlParameter
										{ ParameterName = "@VisitID", Direction = ParameterDirection.InputOutput, SqlDbType = SqlDbType.Int, Value = model.ID });

					connection.Open();
					cmd.ExecuteScalar();
					
					return cmd.Parameters["@VisitID"].Value != DBNull.Value ? Convert.ToInt32(cmd.Parameters["@VisitID"].Value) : 0;
				}
			}
			catch (Exception ex)
			{
				this.Ex = ex;
                return 0;

			}
		}
				
		public int Update(VisitModel model)
		{
           return Insert(model);
			//try
			//{
			//	using (SqlConnection connection = new SqlConnection(ConnString()))
			//	{

			//		SqlCommand cmd = new SqlCommand("dbo.spUpdateVisit", connection)
			//		{
			//			CommandType = CommandType.StoredProcedure
			//		};
			//		cmd.Parameters.AddWithValue("@VisitDate", model.VisitDate);
			//		cmd.Parameters.AddWithValue("@MemNo", model.Member.MemNo);
			//		cmd.Parameters.AddWithValue("@GuestFore", model.Guest.Forename);
			//		cmd.Parameters.AddWithValue("@GuestSur", model.Guest.Surname);
			//		cmd.Parameters.AddWithValue("@Activity", model.Activity.ActivityName);
			//		cmd.Parameters.AddWithValue("@SubActivity", model.Activity.SubActivity);
			//		cmd.Parameters.AddWithValue("@SubActivityID", model.Activity.SubActivityID);
			//		cmd.Parameters.AddWithValue("@Amount", model.Amount);
			//		cmd.Parameters.AddWithValue("@PaidDate", model.PaidDate);
			//		cmd.Parameters.AddWithValue("@Paid", model.IsPaid);
			//		cmd.Parameters.AddWithValue("@Notes", model.Notes);
			//		cmd.Parameters.AddWithValue("@VisitID", model.ID);
			//		connection.Open();
			//		int RecordsAffected = cmd.ExecuteNonQuery();
			//		return RecordsAffected;
			//	}
			//}
			//catch (Exception ex)
			//{
			//	this.Ex = ex;
			//	return 0;

			//}
			
		}
		public List<VisitModel> Load(string input = null, bool all = false)
		{
			using (SqlConnection con = new SqlConnection(ConnString()))
			{
				SqlDataAdapter da = new SqlDataAdapter("dbo.spLoadVisits", con);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				//passing in a null gives a default date of today's date in stored procedure
				if (all)
				{
                    da.SelectCommand.Parameters.AddWithValue("@VisitDate", DBNull.Value);
                    da.SelectCommand.Parameters.AddWithValue("@All", all);
                }
				else if(input == null)
				{
					da.SelectCommand.Parameters.AddWithValue("@VisitDate", DBNull.Value);
                    da.SelectCommand.Parameters.AddWithValue("@All", false);
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@VisitDate", Convert.ToDateTime(input));
                    da.SelectCommand.Parameters.AddWithValue("@All", false);
                }
				da.FillSchema(dt, SchemaType.Source);
				dt.TableName = "Visitors";
				da.Fill(dt);
				#region ConvertToModels
				List<VisitModel> output = dt.AsEnumerable().Select(row => new VisitModel(
				row.Field<int>("VisitID"),
				row.IsNull("VisitDate") ? DateTime.Today.Date : row.Field<DateTime>("VisitDate"),
				row.Field<double>("MemNo"),
				row.Field<string>("Forename"),
				row.Field<string>("Surname"),
				row.Field<string>("Category"),
				row.Field<int?>("SubActivityId"),
				row.Field<string>("Activity"),
				row.Field<string>("SubActivity"),
                row.Field<decimal?>("Amount"),
				row.Field<DateTime?>("PaidDate"),
                row.IsNull("Paid") ? false : row.Field<bool>("Paid"),
				row.Field<string>("GuestFore"),
				row.Field<string>("GuestSur"))).ToList();
                #endregion
                return output;
                
            }
		}
	}
}
