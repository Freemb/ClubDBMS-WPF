using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;


namespace DataLibrary.Operations
{

    public class MemberConnector: CommonConnector,IModelConnector<MemberModel>
	{
		public int Update(MemberModel member)
		{
			try
			{
				using (SqlConnection connection = new SqlConnection(ConnString()))
				{

					using (SqlCommand cmd = new SqlCommand("dbo.spUpdateMember", connection))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@Surname", member.Surname);
						cmd.Parameters.AddWithValue("@Title", member.Title);
						cmd.Parameters.AddWithValue("@Forename", member.Forename);
						cmd.Parameters.AddWithValue("@Gender", member.Gender);
						cmd.Parameters.AddWithValue("@Category", member.Category);
						cmd.Parameters.AddWithValue("@DOB", member.DateOfBirth);
						cmd.Parameters.AddWithValue("@Email", member.Email);
						cmd.Parameters.AddWithValue("@HomeTel", member.HomeTel);
						cmd.Parameters.AddWithValue("@MobileTel", member.MobileTel);
						cmd.Parameters.AddWithValue("@MemNo", member.MemNo);
						//cmd.Parameters.Add(new SqlParameter { ParameterName = "@flag", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Bit });
						connection.Open();
						int RowsAffected = cmd.ExecuteNonQuery();
						connection.Close();
						this.Ex = null;
						return RowsAffected;

					}
				}
			}
			catch (Exception ex)
			{
				this.Ex = ex;
				return 0;
			}
		}

		public int Insert(MemberModel member)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnString()))
				{

					using (SqlCommand cmd = new SqlCommand("dbo.spInsertMember", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@Surname", member.Surname);
						cmd.Parameters.AddWithValue("@Title", member.Title);
						cmd.Parameters.AddWithValue("@Forename", member.Forename);
						cmd.Parameters.AddWithValue("@Gender", member.Gender);
						cmd.Parameters.AddWithValue("@Category", member.Category);
						cmd.Parameters.AddWithValue("@DOB", member.DateOfBirth);
						cmd.Parameters.AddWithValue("@Email", member.Email);
						cmd.Parameters.AddWithValue("@HomeTel", member.HomeTel);
						cmd.Parameters.AddWithValue("@MobileTel", member.MobileTel);
						cmd.Parameters.AddWithValue("@MemNo", member.MemNo);
						//cmd.Parameters.Add(new SqlParameter { ParameterName = "@flag", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Bit });
						conn.Open();
						int RowsAffected = cmd.ExecuteNonQuery();
						conn.Close();
						this.Ex = null;
						return RowsAffected;

					}
				}
			}
			catch (Exception ex)
			{
					this.Ex = ex;
					return 0;
			}
		}

		public bool Delete(MemberModel member)
		{
			try
			{
				using (SqlConnection conn = new SqlConnection(ConnString()))
				{
					using (var cmd = new SqlCommand("dbo.spDeleteMember", conn))
					{
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.AddWithValue("@MemNo", member.MemNo);
                        cmd.Parameters.Add(new SqlParameter { Direction = ParameterDirection.Output, ParameterName = "@flag", SqlDbType = SqlDbType.Int });
                        conn.Open();
                        cmd.ExecuteScalar();
                        this.Ex = null;
                        bool flag = cmd.Parameters["@flag"].Value != DBNull.Value ? Convert.ToBoolean(cmd.Parameters["@flag"].Value) : false;
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

		public List<MemberModel> Load(string input = null, bool all = false)
		{
			using (SqlConnection con = new SqlConnection(ConnString()))
			{
				var da = new SqlDataAdapter("dbo.spMembers_GetbySurname", con);
				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				//passing in a null gives a default behaviour of returning top 25 records unfiltered
				if (!all || input != null)
				{
					da.SelectCommand.Parameters.AddWithValue("@Surname", input);
				}
				else
				{
					da.SelectCommand.Parameters.AddWithValue("@Surname", DBNull.Value);
				}
				//DataTable dt = new DataTable();
				da.FillSchema(dt, SchemaType.Source);
				dt.TableName = "Members";
				da.Fill(dt);
				#region ConvertToModels
				List<MemberModel> output = dt.AsEnumerable().Select(row => new MemberModel()
				{
					MemNo = row.Field<double>("MemNo"),
					Title = row.Field<string>("Title"),
					Forename = row.Field<string>("Forename"),
					Surname = row.Field<string>("Surname"),
					Category = row.Field<string>("Category"),
					Email = row.Field<string>("Email"),
					MobileTel = row.Field<string>("MobileTel"),
					HomeTel = row.Field<string>("HomeTel"),
					Gender = row.Field<string>("Gender"),
					DateOfBirth = row.IsNull("DOB") ? DateTime.MinValue : row.Field<DateTime>("DOB")
				}).ToList();
				#endregion
				return output;
			}
		}

    }
}
