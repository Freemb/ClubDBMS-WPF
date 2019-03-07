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
        public int Delete(DeliveryModel model)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnString()))
                {
                    using (var cmd = new SqlCommand("dbo.spDeleteDelivery", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", model.ID);
                        conn.Open();
                        int RecordsAffected = cmd.ExecuteNonQuery();
                        this.Ex = null;
                        return RecordsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Ex = ex;
                return 0;
            }
        }

        public int Insert(DeliveryModel model)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString()))
                {

                    SqlCommand cmd = new SqlCommand("dbo.spInsertDelivery", connection) { CommandType = CommandType.StoredProcedure };
                    //Add after the stored procedure is made
                    //cmd.Parameters.AddWithValue();

                    cmd.Parameters.Add(new SqlParameter
                    { ParameterName = "@ID", Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Int });
                    connection.Open();
                    cmd.ExecuteScalar();
                    return cmd.Parameters["@ID"].Value != DBNull.Value ? Convert.ToInt32(cmd.Parameters["@ID"].Value) : 0;
                }
            }
            catch (Exception ex)
            {
                this.Ex = ex;
                return 0;

            }
        }

        public int Update(DeliveryModel model)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString()))
                {

                    SqlCommand cmd = new SqlCommand("dbo.spUpdateVisit", connection){CommandType = CommandType.StoredProcedure};
                    cmd.Parameters.AddWithValue("@DeliveryDate", model.EntryTime.Date);
                    cmd.Parameters.AddWithValue("@Entry", model.EntryTime);
                    cmd.Parameters.AddWithValue("@VReg", model.VReg);
                    cmd.Parameters.AddWithValue("@Company", model.Company);
                    cmd.Parameters.AddWithValue("@Make", model.Make);
                    cmd.Parameters.AddWithValue("@Colour", model.Colour);
                    cmd.Parameters.AddWithValue("@Driver", model.DriverName);
                    cmd.Parameters.AddWithValue("@Location", model.Location);
                    cmd.Parameters.AddWithValue("@Exit", model.ExitTime);
                    cmd.Parameters.AddWithValue("@Description", model.Description);
                    cmd.Parameters.AddWithValue("@ID", model.ID);
                    connection.Open();
                    int RecordsAffected = cmd.ExecuteNonQuery();
                    return RecordsAffected;
                }
            }
            catch (Exception ex)
            {
                this.Ex = ex;
                return 0;

            }
        }

        public List<DeliveryModel> Load(string input, bool all)
        {
            using (SqlConnection con = new SqlConnection(ConnString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("dbo.spLoadDeliveries", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //passing in a null for input gives a default date of today's date in stored procedure
                if (!all)
                {
                    da.SelectCommand.Parameters.AddWithValue("@DeliveryDate", Convert.ToDateTime(input));
                }
                else
                {
                    da.SelectCommand.Parameters.AddWithValue("@DeliveryDate", DBNull.Value);
                }
                da.FillSchema(dt, SchemaType.Source);
                dt.TableName = "Deliveries";
                da.Fill(dt);
                #region ConvertToModels
                List<DeliveryModel> output = dt.AsEnumerable().Select(row => new DeliveryModel(
                row.Field<int>("ID"),
                row.IsNull("EntryTime") ? DateTime.Now : row.Field<DateTime>("EntryTime"),
                row.IsNull("ExitTime") ? DateTime.Now : row.Field<DateTime>("ExitTime"),
                row.Field<string>("VReg"),
                row.Field<string>("Company"),
                row.Field<string>("Make"),
                row.Field<string>("Colour"),
                row.Field<string>("Location"),
                row.Field<string>("DriverName"),
                row.Field<string>("Description"))).ToList();
                #endregion
                return output;

            }
        }
    }
}
