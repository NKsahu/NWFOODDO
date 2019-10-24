using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODDO.Models
{
    public class TifinSubmiteHub
    {
    public    int TifinInfoId { get; set; }
    public  int HubId { get; set; }
    public  string    Img { get; set; }
    public  string    PersonName { get; set; }
    public  string Mobile { get; set; }
    public string Remarks { get; set; }
    public System.DateTime CreationDate { get; set; }
    public int DeliveryBoyId { get; set; }
    public int OtpNumber { get; set; }

        public TifinSubmiteHub()
        {
            Img = "";
            PersonName = "";
            Mobile = "";
            Remarks = "";
            CreationDate = System.DateTime.Now;
        }
        public System.Int64 Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.TifinInfoId == 0) { 
                cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO TifinSubmitInfo  VALUES (@HubId,@Img,@PersonName,@Mobile,@Remarks,@CreationDate,@DeliveryBoyId,@OtpNumber);select SCOPE_IDENTITY();", Obj.Con);
                cmd.Parameters.AddWithValue("@CreationDate", System.DateTime.Now);
            }
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("UPDATE FOOD SET HubId=@HubId,Img=@Img,PersonName=@PersonName,Mobile=@Mobile,Remarks=@Remarks,DeliveryBoyId=@DeliveryBoyId,OtpNumber=@OtpNumber where TifinInfoId=@TifinInfoId", Obj.Con);
                    cmd.Parameters.AddWithValue("@TifinInfoId", this.TifinInfoId);
                }
                cmd.Parameters.AddWithValue("@HubId", this.HubId);
                cmd.Parameters.AddWithValue("@Img", this.Img);
                cmd.Parameters.AddWithValue("@PersonName", this.PersonName);
                cmd.Parameters.AddWithValue("@Mobile", this.Mobile);
                cmd.Parameters.AddWithValue("@Remarks", this.Remarks);
                cmd.Parameters.AddWithValue("@DeliveryBoyId", this.DeliveryBoyId);
                cmd.Parameters.AddWithValue("@OtpNumber", this.OtpNumber);
                if (this.TifinInfoId == 0)
                {
                    this.TifinInfoId = System.Convert.ToInt32(cmd.ExecuteScalar());
                }
                else
                {
                    if (cmd.ExecuteNonQuery() <= 0)
                    {
                        this.TifinInfoId = 0;
                    }
                }
            }
            catch (System.Exception e) { this.TifinInfoId = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.TifinInfoId;
        }
        public static List<TifinSubmiteHub> TifinInfoList()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            List<TifinSubmiteHub> ListTmp = new List<TifinSubmiteHub>();
            TifinSubmiteHub ObjRoutes = null;
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM TifinSubmitInfo";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjRoutes = new TifinSubmiteHub
                    {
                        TifinInfoId=SDR.GetInt32(0),
                       HubId=SDR.GetInt32(1),
                       Img=SDR.GetString(2),
                       PersonName=SDR.GetString(3),
                       Mobile=SDR.GetString(4),
                   Remarks=SDR.GetString(5),
                   CreationDate=SDR.GetDateTime(6)

                    };
                    ListTmp.Add(ObjRoutes);
                }
            }
            catch (System.Exception e)
            {
                e.ToString();
            }
            finally { cmd.Dispose(); SDR.Close(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }

            return ListTmp;
        }
    }
}