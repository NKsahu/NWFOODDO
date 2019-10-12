using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FOODDO.Models
{
    public class Routes
    {
        public int HubID { get; set; }
        public string HubName { get; set; }
        public string HubCode { get; set; }
        public string HubLatLng { get; set; }
        public string HubOwnerName { get; set; }
        public string MobileNo { get; set; }
        public string Mobile2 { get; set; }

        public List<Routes> RouteList()
        {
            
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            List<Routes> ListTmp = new List<Routes>();
            Routes ObjRoutes= null;
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM DL_Route";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjRoutes = new Routes
                    {
                        HubID = SDR.GetInt32(0),
                        HubName = SDR.GetString(1),
                        HubCode = SDR.GetString(2),
                        HubLatLng=SDR.IsDBNull(3)?"":SDR.GetString(3),
                        HubOwnerName= SDR.IsDBNull(4) ? "": SDR.GetString(4),
                        MobileNo= SDR.IsDBNull(5) ? "":SDR.GetString(5),
                        Mobile2= SDR.IsDBNull(6) ? "":SDR.GetString(6)
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

        public int  Insert()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            int RE = 0;
            try
            {
                cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO Dl_Route (RouteName,RouteCode,RouteLatLon,HubOwnerName,Mobile,Mobile2) VALUES (@RouteName,@RouteCode,@RouteLatLon,@HubOwnerName,@Mobile,@Mobile2);", Obj.Con);
                //cmd.Parameters.AddWithValue("@RID", this.RouteID);
                cmd.Parameters.AddWithValue("@RouteName", this.HubName);
                cmd.Parameters.AddWithValue("@RouteCode", this.HubCode);
                if (this.HubLatLng == null)
                {
                    cmd.Parameters.AddWithValue("@RouteLatLon", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RouteLatLon", this.HubLatLng);
                }
                
                cmd.Parameters.AddWithValue("@HubOwnerName", this.HubOwnerName);
                if (this.MobileNo == null)
                {
                    cmd.Parameters.AddWithValue("@Mobile", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Mobile", this.MobileNo);
                }
               
                if (this.Mobile2 == null)
                {
                    cmd.Parameters.AddWithValue("@Mobile2", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Mobile2", this.Mobile2);
                }
                
                RE = cmd.ExecuteNonQuery();
               
            }
            catch (System.Exception e) { RE = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return RE;
        }

        public int Update()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            int RE = 0;
            try
            {
                cmd = new System.Data.SqlClient.SqlCommand("UPDATE Dl_Route SET RouteName=@RouteName,RouteCode=@RouteCode,RouteLatLon=@RouteLatLon,HubOwnerName=@HubOwnerName,Mobile=@Mobile,Mobile2=@Mobile2 where RID=@RID ", Obj.Con);
                cmd.Parameters.AddWithValue("@RID", this.HubID);
                cmd.Parameters.AddWithValue("@RouteName", this.HubName);
                cmd.Parameters.AddWithValue("@RouteCode", this.HubCode);
                if (this.HubLatLng == null)
                {
                    cmd.Parameters.AddWithValue("@RouteLatLon", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@RouteLatLon", this.HubLatLng);
                }

                cmd.Parameters.AddWithValue("@HubOwnerName", this.HubOwnerName);
                if (this.MobileNo == null)
                {
                    cmd.Parameters.AddWithValue("@Mobile", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Mobile", this.MobileNo);
                }

                if (this.Mobile2 == null)
                {
                    cmd.Parameters.AddWithValue("@Mobile2", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Mobile2", this.Mobile2);
                }
                if (cmd.ExecuteNonQuery() > 0) {
                    RE = cmd.ExecuteNonQuery();
                }

            }
            catch (System.Exception e) { RE = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return RE;
        }

    }
}