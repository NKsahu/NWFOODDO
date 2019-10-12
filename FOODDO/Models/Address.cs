namespace FOODDO.Models
{
    public class Address
    {
        public System.Int64 AddressId { get; set; }
        public System.Int64 CID { get; set; }
        public string Address1 { get; set; }
        public string Landmark { get; set; }
        public string Hub { get; set; }
        public string Type { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Description { get; set; }
        public System.DateTime Create_Date { get; set; }
        public System.DateTime Update_Date { get; set; }
        public bool Deleted { get; set; }
        public static System.Collections.Generic.List<Address> List { get; set; }

        public Address()
        {
            this.Address1 = "";
            this.Landmark = "";
            this.Hub = "";
            this.Type = "";
            this.Latitude = "";
            this.Longitude = "";
            this.Description = "";
            this.Create_Date = System.DateTime.Now;
            this.Update_Date = System.DateTime.Now;
        }

        public System.Int64 Insert()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            int RE = 0;
            try
            {
                cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO ADDRESS (CID,Address1,Landmark,Hub,Type,Latitude,Longitude,Description,Create_Date,Update_Date,Deleted) VALUES (@CID,@Address1,@Landmark,@Hub,@Type,@Latitude,@Longitude,@Description,@Create_Date,@Update_Date,@Deleted);", Obj.Con);
                cmd.Parameters.AddWithValue("@CID", this.CID);
                cmd.Parameters.AddWithValue("@Address1", this.Address1);
                cmd.Parameters.AddWithValue("@Landmark", this.Landmark);
                cmd.Parameters.AddWithValue("@Hub", this.Hub);
                cmd.Parameters.AddWithValue("@Type", this.Type);
                cmd.Parameters.AddWithValue("@Latitude", this.Latitude);
                cmd.Parameters.AddWithValue("@Longitude", this.Longitude);
                cmd.Parameters.AddWithValue("@Description", this.Description);
                cmd.Parameters.AddWithValue("@Create_Date", this.Create_Date);
                cmd.Parameters.AddWithValue("@Update_Date", this.Update_Date);
                cmd.Parameters.AddWithValue("@Deleted", 0);
                RE = cmd.ExecuteNonQuery();
                    if (RE > 0)
                        Address.List.Insert(0, this);
            }
            catch (System.Exception e) { RE = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return RE;
        }

        public System.Int64 Update()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            System.Int64 RE = 0;
            try
            {
                cmd = new System.Data.SqlClient.SqlCommand("UPDATE ADDRESS SET Address1=@Address1,Landmark=@Landmark,Hub=@Hub,Latitude=@Latitude,Longitude=@Longitude,Description=@Description,Create_Date=@Create_Date,Update_Date=@Update_Date,Deleted=@Deleted where CID=@CID AND Type=@Type", Obj.Con);
                cmd.Parameters.AddWithValue("@CID", this.CID);
                cmd.Parameters.AddWithValue("@Address1", this.Address1);
                cmd.Parameters.AddWithValue("@Landmark", this.Landmark);
                cmd.Parameters.AddWithValue("@Hub", this.Hub);
                cmd.Parameters.AddWithValue("@Type", this.Type);
                cmd.Parameters.AddWithValue("@Latitude", this.Latitude);
                cmd.Parameters.AddWithValue("@Longitude", this.Longitude);
                cmd.Parameters.AddWithValue("@Description", this.Description);
                cmd.Parameters.AddWithValue("@Create_Date", this.Create_Date);
                cmd.Parameters.AddWithValue("@Update_Date", System.DateTime.Now);
                cmd.Parameters.AddWithValue("@Deleted", this.Deleted);
                if (cmd.ExecuteNonQuery() > 0)
                    {
                    RE = this.AddressId;
                        Address.List.RemoveAll(x => x.CID == this.CID && x.Type.Equals(this.Type));
                        if (this.Deleted == false)
                            Address.List.Insert(0, this);
                    }
            }
            catch (System.Exception e) { RE = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return RE;
        }

        public System.Collections.Generic.List<Address> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            System.Collections.Generic.List<Address> ListTmp = new System.Collections.Generic.List<Address>();
            Address ObjTmp = null;
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM ADDRESS WHERE Deleted=0 ORDER BY Create_Date";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp = new Address
                    {
                        CID = SDR.GetInt64(0),
                        Address1 = SDR.GetString(1),
                        Landmark = SDR.GetString(2),
                        Hub = SDR.GetString(3),
                        Type = SDR.GetString(4),
                        Latitude = SDR.GetString(5),
                        Longitude = SDR.GetString(6),
                        Description = SDR.GetString(7),
                        Create_Date = SDR.GetDateTime(8),
                        Update_Date = SDR.GetDateTime(9),
                        AddressId=SDR.GetInt64(11)
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
