using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Operations
{
    public class EventConnector : CommonConnector, IModelConnector<EventModel>
    {
        public bool Delete(EventModel model)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnString()))
                {
                    using (var cmd = new SqlCommand("dbo.spDeleteEvent", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", model.ID);
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

        public int Insert(EventModel model)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString()))
                {

                    SqlCommand cmd = new SqlCommand("dbo.spUpsertEvent", connection)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@EventName", model.EventName);
                    cmd.Parameters.AddWithValue("@Frequency", model.Frequency);
                    cmd.Parameters.AddWithValue("@Mode", model.Mode);
                    cmd.Parameters.AddWithValue("@Type", model.Type);
                    cmd.Parameters.Add(new SqlParameter
                    { ParameterName = "@ID", Direction = ParameterDirection.InputOutput, SqlDbType = SqlDbType.Int, Value = model.ID });

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

        public int Update(EventModel model)
        {
            return Insert(model);
            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(ConnString()))
            //    {

            //        using (SqlCommand cmd = new SqlCommand("dbo.spUpdateEvent", connection) { CommandType = CommandType.StoredProcedure })
            //        {
            //            cmd.Parameters.AddWithValue("@EventName", model.EventName);
            //            cmd.Parameters.AddWithValue("@Frequency", model.Frequency);
            //            cmd.Parameters.AddWithValue("@Mode", model.Mode);
            //            cmd.Parameters.AddWithValue("@ID", model.ID);
            //            cmd.Parameters.AddWithValue("@Type", model.Type);

            //            connection.Open();
            //            int RowsAffected = cmd.ExecuteNonQuery();
            //            connection.Close();
            //            this.Ex = null;
            //            return RowsAffected;

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    this.Ex = ex;
            //    return 0;
            //}
        }
        public List<EventModel> Load(string input, bool all)
        {
            using (SqlConnection conn = new SqlConnection(ConnString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("dbo.spLoadEvents", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dt);
                #region Convert to Models
                List<EventModel> output = dt.AsEnumerable().Select(row => new EventModel
                {
                    ID = row.Field<int>("ID"),
                    EventName = row.Field<string>("EventName"),
                    Type = row.Field<string>("EventType"),
                    Frequency = row.Field<string>("EventFreq"),
                    Mode = row.Field<string>("BookingMode")
                }).ToList();
                #endregion
                return output;
            }
        }
    }
}
