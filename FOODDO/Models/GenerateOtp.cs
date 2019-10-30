using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODDO.Models
{
    public class GenerateOtp
    {
       protected Int64 OtpId { get; set; }
       public string OtpCode { get; set; }
        public string OtpType { get; set; }
        public int hubid { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Deleted { get; set; }
        public static List<GenerateOtp> List { get; set; }
        public System.Int64 Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.OtpId == 0)
                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO GenerateOtp VALUES (@OtpCode,@OtpType,@HubId,@CreationDate,@Deleted);select SCOPE_IDENTITY();", Obj.Con);
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("UPDATE GenerateOtp SET OtpCode=@OtpCode,OtpType=@OtpType,HubId=@HubId,CreationDate=@CreationDate,Deleted=@Deleted where OtpId=@OtpId", Obj.Con);
                    cmd.Parameters.AddWithValue("@OtpId", this.OtpId);
                }
                cmd.Parameters.AddWithValue("@OtpCode", this.OtpCode);
                cmd.Parameters.AddWithValue("@OtpType", this.OtpType);
                cmd.Parameters.AddWithValue("@HubId", this.hubid);
                cmd.Parameters.AddWithValue("@CreationDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Deleted", this.Deleted);
                if (this.OtpId == 0)
                {
                    this.OtpId = System.Convert.ToInt64(cmd.ExecuteScalar());
                    if (this.OtpId > 0)
                        GenerateOtp.List.Insert(0, this);
                }
                else
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        GenerateOtp.List.RemoveAll(x => x.OtpId == this.OtpId);
                        if (this.Deleted == false)
                            GenerateOtp.List.Insert(0, this);
                    }
                }
            }
            catch (System.Exception e) { this.OtpId = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.OtpId;
        }

        public List<GenerateOtp> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            System.Collections.Generic.List<GenerateOtp> ListTmp = new System.Collections.Generic.List<GenerateOtp>();
            GenerateOtp ObjTmp = null;
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM GenerateOtp WHERE Deleted=0 ";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp = new GenerateOtp
                    {
                        OtpId = SDR.GetInt64(0),
                        OtpCode = SDR.GetString(1),
                        OtpType = SDR.GetString(2),
                        hubid = SDR.GetInt32(3),
                        CreationDate = SDR.GetDateTime(4),
                    };
                    ListTmp.Add(ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { cmd.Dispose(); SDR.Close(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return (ListTmp);
        }


    }
}