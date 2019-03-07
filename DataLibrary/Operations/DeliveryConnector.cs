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

        public List<DeliveryModel> Load(string input, bool all)
        {
            using (SqlConnection con = new SqlConnection(ConnString()))
            {
                SqlDataAdapter da = new SqlDataAdapter("dbo.spLoadDeliveries", con);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                //passing in a null gives a default date of today's date in stored procedure
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
