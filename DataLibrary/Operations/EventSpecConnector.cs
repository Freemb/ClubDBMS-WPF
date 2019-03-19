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
    public class EventSpecConnector : CommonConnector, IModelConnector<EventSpecModel>
    {
        public bool Delete(EventSpecModel model)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnString()))
                {
                    using (var cmd = new SqlCommand("dbo.spDeleteEventSpec", conn))
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

        public int Insert(EventSpecModel model)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnString()))
                {

                    SqlCommand cmd = new SqlCommand("dbo.spUpsertEventSpec", connection) { CommandType = CommandType.StoredProcedure };
                    cmd.Parameters.AddWithValue("@EventDate", model.EventDate);
                    cmd.Parameters.AddWithValue("@Location", model.Location);
                    cmd.Parameters.AddWithValue("@MaxLimit", model.MaxLimit);
                    cmd.Parameters.AddWithValue("@EventsID", model.Event.ID);
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@ID", Direction = ParameterDirection.InputOutput, SqlDbType = SqlDbType.Int });

                    DataTable pricelist = new DataTable();
                    pricelist.Columns.Add(new DataColumn("ID", typeof(int)));
                    pricelist.Columns.Add(new DataColumn("Group", typeof(string)));
                    pricelist.Columns.Add(new DataColumn("Cost", typeof(decimal)));
                    foreach (EventPriceModel item in model.PriceList)
                    {
                        pricelist.Rows.Add(item.ID, item.Group, item.Cost);
                    }
                    cmd.Parameters.Add(new SqlParameter
                    {
                        SqlDbType = SqlDbType.Structured,
                        ParameterName = "@PriceList",
                        Value = pricelist,
                        TypeName = "dbo.EventPrices"
                    });
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
        
        public int Update(EventSpecModel model)
        {
           return Insert(model);
            //try
            //{
            //    using (SqlConnection conn = new SqlConnection(ConnString()))
            //    {
            //        SqlCommand cmd = new SqlCommand("dbo.spUpsertEventSpec", conn) { CommandType = CommandType.StoredProcedure };
            //        cmd.Parameters.AddWithValue("@ID", model.ID);
            //        cmd.Parameters.AddWithValue("@EventDate", model.EventDate);
            //        cmd.Parameters.AddWithValue("@Location", model.Location);
            //        cmd.Parameters.AddWithValue("@MaxLimit", model.MaxLimit);

            //        DataTable pricelist = new DataTable();
            //        pricelist.Columns.Add(new DataColumn("ID", typeof(int)));
            //        pricelist.Columns.Add(new DataColumn("Group", typeof(string)));
            //        pricelist.Columns.Add(new DataColumn("Cost", typeof(decimal)));
            //        foreach (EventPriceModel item in model.PriceList)
            //        {
            //            pricelist.Rows.Add(item.ID, item.Group, item.Cost);
            //        }

            //        cmd.Parameters.Add( new SqlParameter {SqlDbType = SqlDbType.Structured,ParameterName = "@PriceList",
            //                                               Value = pricelist, TypeName = "dbo.EventPrices"});
                    
            //        conn.Open();
            //        int RecordsAffected = cmd.ExecuteNonQuery();
            //        this.Ex = null;
            //        return RecordsAffected;
            //    }
            //}
            //catch(Exception ex)
            //{
            //    this.Ex = ex;
            //    return 0;
            //}
            
        }

        public List<EventSpecModel> Load(string input, bool all)
        {
            //Load from sql database into datatable
            using (SqlConnection conn = new SqlConnection(ConnString()))
            {

                SqlDataAdapter da = new SqlDataAdapter("spLoadEventSpecs", conn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (!all)
                {
                    da.SelectCommand.Parameters.AddWithValue("@EventID", Convert.ToInt32(input));
                }
                else { da.SelectCommand.Parameters.AddWithValue("@EventID", DBNull.Value); }

                da.Fill(dt);
            }

            //Convert datatable to List of models
            List<EventSpecModel> output = new List<EventSpecModel>();
            List<DataRow> tempList = dt.AsEnumerable().ToList();
            int? IDholder = 0; // initialise a comparison temp variable

            for (int i = 0; i < tempList.Count(); i++)
            {
                int? EventSpecID = tempList[i].Field<int?>("ID"); //same read value used more than twice so set to temp variable for EventSpec ID

                //If not null EventSpecID then there will be price details to convert to models
                if (EventSpecID != null)
                {
                    EventPriceModel modelprices = new EventPriceModel()
                    {
                        ID = EventSpecID,
                        Group = tempList[i].Field<string>("Group"),
                        Cost = tempList[i].IsNull("Cost") ? 0 : tempList[i].Field<decimal?>("Cost")
                    };

                    //The Data is grouped according to integer ID from database sorting, O(n) behaviour from iterating through list once and extract price models for each ID group
                    if (EventSpecID != IDholder)
                    {
                        IDholder = EventSpecID;
                        EventSpecModel model = new EventSpecModel()
                        {
                            ID = tempList[i].Field<int?>("ID"),
                            EventDate = tempList[i].Field<DateTime?>("EventDate"),
                            Location = tempList[i].Field<string>("Location"),
                            MaxLimit = tempList[i].Field<short?>("MaxLimit")
                        };
                        model.Event.ID = tempList[i].Field<int>("EventID");
                        model.Event.EventName = tempList[i].Field<string>("EventName");
                        model.Event.Type = tempList[i].Field<string>("EventType");
                        model.Event.Frequency = tempList[i].Field<string>("EventFreq");
                        model.Event.Mode = tempList[i].Field<string>("BookingMode");
                        model.PriceList.Add(modelprices);
                        output.Add(model);
                    }

                    else { output.Last().PriceList.Add(modelprices); }
                }
                //if EventSpecID is null, add the EventModel without specifications and move on
                else
                {
                    EventSpecModel model = new EventSpecModel();
                    model.Event.ID = tempList[i].Field<int>("EventID");
                    model.Event.EventName = tempList[i].Field<string>("EventName");
                    model.Event.Type = tempList[i].Field<string>("EventType");
                    model.Event.Frequency = tempList[i].Field<string>("EventFreq");
                    model.Event.Mode = tempList[i].Field<string>("BookingMode");
                    output.Add(model);

                }
            }
            return output;
        }
    }
}
