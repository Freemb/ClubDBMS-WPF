using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataLibrary.Operations
{
    public class EventConnector : CommonConnector, IModelConnector<EventSpecModel>
    {
        public int Delete(EventSpecModel model)
        {
            throw new NotImplementedException();
        }

        public int Insert(EventSpecModel model)
        {
            throw new NotImplementedException();
        }

        public List<EventSpecModel> Load(string input, bool all)
        {
            using (SqlConnection conn = new SqlConnection(ConnString()))
            {

                SqlDataAdapter da = new SqlDataAdapter("spLoadEvents", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (!all)
                {
                    da.SelectCommand.Parameters.AddWithValue("@EventName", input);
                }
                else { da.SelectCommand.Parameters.AddWithValue("@EventName", DBNull.Value); }

                da.Fill(dt);
            }

            
            List<EventSpecModel> output = new List<EventSpecModel>();
            List<DataRow> temp = dt.AsEnumerable().ToList();
            int? ID = 0;
            
            for (int i=0; i < temp.Count(); i++)
            {
                EventPriceModel modelprices = new EventPriceModel()
                {
                    EventSpecID = temp[i].Field<int?>("ID"),
                    Group = temp[i].Field<string>("Group"),
                    Cost = temp[i].IsNull("Cost") ? 0 : temp[i].Field<decimal?>("Cost")
                };

                //Data is grouped according to integer ID from database sorting, O(n) behaviour
                if (ID != temp[i].Field<int?>("ID"))
                {
                    ID = temp[i].Field<int?>("ID");
                    EventSpecModel model = new EventSpecModel()
                    {
                        EventID = temp[i].Field<int>("EventID"),
                        ID = temp[i].Field<int?>("ID"),
                        EventDate = temp[i].Field<DateTime?>("EventDate"),
                        Location = temp[i].Field<string>("Location"),
                        MaxLimit = temp[i].Field<short?>("MaxLimit")
                    };
                    model.PriceList.Add(modelprices);
                    output.Add(model);
                }

                else { output.Last().PriceList.Add(modelprices); }
                    
            }
               return output;

            
        }

        public int Update(EventSpecModel model)
        {
            throw new NotImplementedException();
        }
    }
}
