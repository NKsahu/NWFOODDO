using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;

namespace FOODDO.Models
{
    public class OrderItemCollection
    {
        public System.Int64 ItemCollectionId { get; set; }
        public System.Int64 OrderItemId { get; set; }
        public System.Int64 TifinRackId { get; set; }
        public System.Int64 FoodId { get; set; }
        public System.Int64 MessId { get; set; }
        public DateTime CreationDate {get;set;}
        public DateTime UpdationDate { get; set; }
        public int CreatedBy { get; set; }
        public static ConcurrentDictionary<Int64, OrderItemCollection> List = new ConcurrentDictionary<Int64, OrderItemCollection>();
        public OrderItemCollection()
        {
            CreationDate = System.DateTime.Now;
            UpdationDate = System.DateTime.Now;
        }
        public System.Int64 Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.ItemCollectionId == 0)
                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO OrderItemCollection  VALUES (@OrderItemId,@TifinRackId,@FoodId,@MessId,@CreationDate,@UpdationDate,@CreatedBy);select SCOPE_IDENTITY();", Obj.Con);
                    cmd.Parameters.AddWithValue("@OrderItemId", this.OrderItemId);
                    cmd.Parameters.AddWithValue("@TifinRackId", this.TifinRackId);
                    cmd.Parameters.AddWithValue("@FoodId", this.FoodId);
                    cmd.Parameters.AddWithValue("@MessId", this.MessId);
                    cmd.Parameters.AddWithValue("@CreationDate", this.CreationDate);
                    cmd.Parameters.AddWithValue("@UpdationDate", this.UpdationDate);
                    cmd.Parameters.AddWithValue("@CreatedBy", this.CreatedBy);
                if (this.ItemCollectionId == 0)
                {
                    this.ItemCollectionId = System.Convert.ToInt64(cmd.ExecuteScalar());
                    if (this.ItemCollectionId > 0)
                        OrderItemCollection.List.TryAdd(this.ItemCollectionId, this);
                }

            }
            catch (System.Exception e)
            {
                this.TifinRackId = 0;
                e.ToString();
            }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.ItemCollectionId;
        }



        public ConcurrentDictionary<Int64, OrderItemCollection> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            ConcurrentDictionary<Int64, OrderItemCollection> ListTmp = new ConcurrentDictionary<Int64, OrderItemCollection>();
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM OrderItemCollection";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    OrderItemCollection ObjTmp = new OrderItemCollection();
                    ObjTmp.ItemCollectionId = SDR.GetInt64(0);
                    ObjTmp.OrderItemId = SDR.GetInt64(1);
                    ObjTmp.TifinRackId = SDR.GetInt64(2);
                    ObjTmp.FoodId = SDR.GetInt64(3);
                    ObjTmp.MessId = SDR.GetInt64(4);
                    ObjTmp.CreationDate = SDR.GetDateTime(5);
                    ObjTmp.CreatedBy = SDR.GetInt32(7);
                    ListTmp.TryAdd(ObjTmp.TifinRackId, ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { cmd.Dispose(); SDR.Close(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return (ListTmp);
        }

    }
}