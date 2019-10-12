using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;

namespace FOODDO.Models
{
    public class TifinRackMaster
    {
        public System.Int64 TifinRackId;
        public System.Int64 TifinId;
        public string TifinRackStatus;
       public static ConcurrentDictionary<Int64, TifinRackMaster> List = new ConcurrentDictionary<Int64, TifinRackMaster>();
        public TifinRackMaster()
        {
            TifinRackStatus = "";
        }
        public System.Int64 Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.TifinRackId == 0)
                {
                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO TifinRackMaster (TifinID,TifinRackStatus) VALUES (@TifinID,@TifinRackStatus);select SCOPE_IDENTITY();", Obj.Con);
                }
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("UPDATE TifinRackMaster SET TifinRackStatus=@TifinRackStatus where TifinRackId=@TifinRackId", Obj.Con);
                    cmd.Parameters.AddWithValue("@TifinRackId", this.TifinRackId);
                }
                cmd.Parameters.AddWithValue("@TifinID", this.TifinId);
                cmd.Parameters.AddWithValue("@TifinRackStatus", this.TifinRackStatus);
                if (this.TifinRackId == 0)
                {
                    this.TifinRackId = System.Convert.ToInt64(cmd.ExecuteScalar());
                    if (this.TifinRackId > 0)
                        TifinRackMaster.List.TryAdd(this.TifinRackId, this);
                }
                else
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        
                        TifinRackMaster.List.TryUpdate(this.TifinRackId, this,this);
                    }
                }

            }
            catch (System.Exception e)
            {
                this.TifinRackId = 0;
                e.ToString();
            }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.TifinRackId;
        }


        public ConcurrentDictionary<Int64, TifinRackMaster> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            ConcurrentDictionary<Int64, TifinRackMaster> ListTmp = new ConcurrentDictionary<Int64, TifinRackMaster>();

            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM TifinRackMaster";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    TifinRackMaster ObjTmp = new TifinRackMaster();
                    ObjTmp.TifinRackId = SDR.GetInt64(0);
                    ObjTmp.TifinId = SDR.GetInt64(1);
                    ObjTmp.TifinRackStatus = SDR.IsDBNull(2) ? "UnUsed" : SDR.GetString(2).Replace(" ","");
                    ListTmp.TryAdd(ObjTmp.TifinRackId, ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { cmd.Dispose(); SDR.Close(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return (ListTmp);
        }


    }
}