using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODDO.Models.COMMON
{
    public class Offers
    {
        public int TitileId { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public double FromAmount { get; set; }
        public double ToAmount { get; set; }
        public double Bonus { get; set; }
        public static List<Offers> List = new List<Offers>();
        public System.Int64 Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.TitileId == 0)
                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO Offer VALUES (@Title,@description,@FromAmount,@ToAmount,@Bonus);select SCOPE_IDENTITY();", Obj.Con);
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("UPDATE Offer SET Title=@Title,description=@description,FromAmount=@FromAmount,ToAmount=@ToAmount,Bonus=@Bonus where OfferID=@OfferID", Obj.Con);
                    cmd.Parameters.AddWithValue("@OfferID", this.TitileId);
                }
                cmd.Parameters.AddWithValue("@Title", this.Title);
                cmd.Parameters.AddWithValue("@description", this.Discription);
                cmd.Parameters.AddWithValue("@FromAmount", this.FromAmount);
                cmd.Parameters.AddWithValue("@ToAmount", this.ToAmount);
                cmd.Parameters.AddWithValue("@Bonus", this.Bonus);
               
                if (this.TitileId == 0)
                {
                    this.TitileId = System.Convert.ToInt32(cmd.ExecuteScalar());
                    if (this.TitileId > 0)
                        Offers.List.Insert(0, this);
                }
                else
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        Offers.List.RemoveAll(x => x.TitileId == this.TitileId);
                        Offers.List.Insert(0, this);
                    }
                }
            }
            catch (System.Exception e) { this.TitileId = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.TitileId;
        }

        public System.Collections.Generic.List<Offers> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            System.Collections.Generic.List<Offers> ListTmp = new System.Collections.Generic.List<Offers>();

            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM Offer";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    Offers ObjTmp = new Offers();
                    ObjTmp.TitileId = System.Int32.Parse(SDR["OfferID"].ToString());
                    ObjTmp.Title = SDR["Title"].ToString();
                    ObjTmp.Discription = SDR["description"].ToString();
                    ObjTmp.FromAmount = int.Parse(SDR["FromAmount"].ToString());
                    ObjTmp.ToAmount = int.Parse(SDR["ToAmount"].ToString());
                    ObjTmp.Bonus = int.Parse(SDR["Bonus"].ToString());
                    ListTmp.Add(ObjTmp);
                }
            }
            catch (System.Exception e) { e.ToString(); }
            finally { cmd.Dispose(); SDR.Close(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return (ListTmp);
        }
    }
}