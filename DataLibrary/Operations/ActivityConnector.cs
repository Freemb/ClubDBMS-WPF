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
	
	
		public bool Delete(ActivityModel model)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnString()))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.spDeleteActivity", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubActivityID", model.ActivityID);
                        cmd.Parameters.Add(new SqlParameter { Direction = ParameterDirection.Output, ParameterName = "@flag", SqlDbType = SqlDbType.Int });
                        con.Open();
                        cmd.ExecuteScalar();
                        this.Ex = null;
                        bool flag = cmd.Parameters["@flag"].Value != DBNull.Value ? Convert.ToBoolean(cmd.Parameters["@flag"].Value) : false;
                        return flag;
                    }
                }

            }
            catch(Exception ex)
            {
              this.Ex = ex;
              return false;
            }
        }  
        
    

		public int Insert(ActivityModel model)
		{
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString()))
                {

                    SqlCommand cmd = new SqlCommand("dbo.spInsertActivity", connection){CommandType = CommandType.StoredProcedure};
                    //Add after the stored procedure is made
                    //cmd.Parameters.AddWithValue();
                    
                    cmd.Parameters.Add(new SqlParameter
                    { ParameterName = "@SubActivityID", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int });
                    connection.Open();
                    cmd.ExecuteScalar();
                    return cmd.Parameters["@SubActivityID"].Value != DBNull.Value ? Convert.ToInt32(cmd.Parameters["@SubActivityID"].Value) : 0;
                }
            }
            catch (Exception ex)
            {
                this.Ex = ex;
                return 0;

            }
        }
        public int Update(ActivityModel model)
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

        public List<ActivityModel> Load(string input, bool all)
        {
            throw new NotImplementedException();
        }

       

       
    }
}
