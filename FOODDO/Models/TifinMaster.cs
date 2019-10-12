using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;

namespace FOODDO.Models
{
    public class TifinMaster
    {
        public System.Int64 TifinID { get; set; }
        public System.Int32 TifinType { get; set; }
        public string TifinColor {get;set;}
        public System.DateTime CreationDate { get; set; }
        public System.Int32 NumberOfTifin { get; set; }
        public string TifinStatus { get; set; }
        public string TifinAtLocation { get; set; }// MESS ,CENTER,HUBOWNER,CUSTOMER,DELIVERY-BOY
        public string TifinTakenBy { get; set; }// uid
        public System.DateTime UpdationDate { get; set; }
        public static ConcurrentDictionary<Int64,TifinMaster> List=new ConcurrentDictionary<Int64, TifinMaster>();
        public TifinMaster()
        {
            
            CreationDate = DateTime.Now;
        }
        public System.Int64 Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.TifinID == 0)
                {
                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO TifinMaster (TifinType,TifinColor,CreationDate,TifinStatus,TifinLocationName,TifinTakenById,UpdationDate) VALUES(@TifinType,@TifinColor,@CreationDate,@TifinStatus,@TifinLocationName,@TifinTakenById,@UpdationDate);select SCOPE_IDENTITY();", Obj.Con);
                }
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("UPDATE TifinMaster set TifinType=@TifinType,TifinColor=@TifinColor,TifinStatus=@TifinStatus,TifinLocationName=@TifinLocationName,TifinTakenById=@TifinTakenById,UpdationDate=@UpdationDate  WHERE TID=@TID", Obj.Con);
                    cmd.Parameters.AddWithValue("@TID", this.TifinID);
                }
                cmd.Parameters.AddWithValue("@TifinType", this.TifinType);
                cmd.Parameters.AddWithValue("@TifinColor", this.TifinColor);
                cmd.Parameters.AddWithValue("@CreationDate", this.CreationDate);
                cmd.Parameters.AddWithValue("@TifinStatus", this.TifinStatus);
                if(this.TifinAtLocation==null)
                cmd.Parameters.AddWithValue("@TifinLocationName", System.DBNull.Value );
                else cmd.Parameters.AddWithValue("@TifinLocationName", this.TifinAtLocation);
                if (this.TifinTakenBy == null)
                    cmd.Parameters.AddWithValue("@TifinTakenById", System.DBNull.Value);
                else cmd.Parameters.AddWithValue("@TifinTakenById", this.TifinTakenBy);
                cmd.Parameters.AddWithValue("@UpdationDate", System.DateTime.Now);
                if (this.TifinID == 0)
                {
                    this.TifinID = System.Convert.ToInt64(cmd.ExecuteScalar());
                    if (this.TifinID > 0)
                        TifinMaster.List.TryAdd(this.TifinID, this);
                }
                else
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        TifinMaster.List.TryUpdate(this.TifinID, this,this);
                    }
                }
                
            }
            catch (System.Exception e) {
                this.TifinID = 0;
                e.ToString();
            }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.TifinID;
        }

        public ConcurrentDictionary<Int64,TifinMaster> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            ConcurrentDictionary<Int64,TifinMaster> ListTmp = new ConcurrentDictionary<Int64, TifinMaster>();

            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM TifinMaster";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    TifinMaster ObjTmp = new TifinMaster();
                    ObjTmp.TifinID = SDR.GetInt64(0);
                    ObjTmp.TifinType = SDR.GetInt32(1);
                    ObjTmp.TifinColor = SDR.GetString(2);
                    ObjTmp.TifinStatus = SDR.IsDBNull(4) ? "UnUsed" : SDR.GetString(4);
                    ObjTmp.TifinAtLocation = SDR.IsDBNull(5) ? "CENTER" : SDR.GetString(5);
                    ObjTmp.TifinTakenBy = SDR.IsDBNull(6) ? "" : SDR.GetString(6);
                    ObjTmp.UpdationDate = SDR.IsDBNull(7) ? System.DateTime.Now : SDR.GetDateTime(7);
                    ListTmp.TryAdd(ObjTmp.TifinID,ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { cmd.Dispose(); SDR.Close(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return (ListTmp);
        }


    }
}