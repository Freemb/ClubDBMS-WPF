using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataLibrary.Operations
{
    public class EventBookingConnector : CommonConnector, IModelConnector<EventBookingModel>
    {
        public bool Delete(EventBookingModel model)
        {
            throw new NotImplementedException();
        }

        public int Insert(EventBookingModel model)
        {
            throw new NotImplementedException();
        }

        public List<EventBookingModel> Load(string input = null, bool all = true)
        {
            using (SqlConnection con = new SqlConnection(ConnString()))
            {
                using (SqlDataAdapter da = new SqlDataAdapter("dbo.spLoadBookings", con))
                {
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.SelectCommand.Parameters.AddWithValue("@eventspecid", Convert.ToInt32(input));
                    da.FillSchema(dt, SchemaType.Source);
                    dt.TableName = "Bookings";
                    da.Fill(dt);
                    List<EventBookingModel> output = dt.AsEnumerable().Select(row => new EventBookingModel(
                        row.Field<string>("BookingRef"),
                        row.Field<int>("EventSpec_ID"),
                        row.IsNull("BookingTime") ? DateTime.UtcNow : row.Field<DateTime>("BookingTime"),
                        row.Field<double>("MemNo"),
                        row.Field<string>("ContactNo"),
                        row.Field<byte>("TableNo"),
                        row.Field<string>("TablePos"),
                        row.Field<byte>("NumPeople"),
                        row.Field<string>("Requirements"),
                        row.Field<bool>("Confirmed"))).ToList();
                    return output;
                }
            }
        }

        public int Update(EventBookingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
