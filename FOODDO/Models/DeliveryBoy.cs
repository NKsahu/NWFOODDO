namespace FOODDO.Models
{
    public class DeliveryBoy
    {
        public int DBID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public System.DateTime Create_Date { get; set; }
        public int Create_By { get; set; }
        public System.DateTime Update_Date { get; set; }
        public int Update_By { get; set; }
        public bool Deleted { get; set; }
        public static System.Collections.Generic.List<DeliveryBoy> List { get; set; }

        public DeliveryBoy()
        {
            this.Name = "";
            this.Mobile = "";
            this.Password = "";
            this.Create_Date = System.DateTime.Now;
            this.Update_Date = System.DateTime.Now;
        }

        public int Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.DBID == 0)
                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO DELIVERYBOY (Name,Mobile,Password,Create_Date,Create_By,Update_Date,Update_By,Deleted) VALUES (@Name,@Mobile,@Password,@Create_Date,@Create_By,@Update_Date,@Update_By,@Deleted);select SCOPE_IDENTITY();", Obj.Con);
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("UPDATE DELIVERYBOY SET Name=@Name,Mobile=@Mobile,Password=@Password,Create_Date=@Create_Date,Create_By=@Create_By,Update_Date=@Update_Date,Update_By=@Update_By,Deleted=@Deleted where DBID=@DBID", Obj.Con);
                    cmd.Parameters.AddWithValue("@DBID", this.DBID);
                }

                cmd.Parameters.AddWithValue("@Name", this.Name);
                cmd.Parameters.AddWithValue("@Mobile", this.Mobile);
                cmd.Parameters.AddWithValue("@Password", this.Password);
                cmd.Parameters.AddWithValue("@Create_Date", this.Create_Date);
                cmd.Parameters.AddWithValue("@Create_By", this.Create_By);
                cmd.Parameters.AddWithValue("@Update_Date", System.DateTime.Now);
                cmd.Parameters.AddWithValue("@Update_By", this.Update_By);
                cmd.Parameters.AddWithValue("@Deleted", this.Deleted);
                if (this.DBID == 0)
                {
                    this.DBID = System.Convert.ToInt32(cmd.ExecuteScalar());
                    if (this.DBID > 0)
                        DeliveryBoy.List.Insert(0, this);
                }
                else
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        DeliveryBoy.List.RemoveAll(x => x.DBID == this.DBID);
                        if (this.Deleted == false)
                            DeliveryBoy.List.Insert(0, this);
                    }
                }
            }
            catch (System.Exception e) { this.DBID = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.DBID;
        }

        public System.Collections.Generic.List<DeliveryBoy> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            System.Collections.Generic.List<DeliveryBoy> ListTmp = new System.Collections.Generic.List<DeliveryBoy>();
            DeliveryBoy ObjTmp = null;
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM DELIVERYBOY WHERE Deleted=0 ORDER BY DBID DESC";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp = new DeliveryBoy
                    {
                        DBID = SDR.GetInt32(0),
                        Name = SDR.GetString(1),
                        Mobile = SDR.GetString(2),
                        Password = SDR.GetString(3),
                        Create_Date = SDR.GetDateTime(4),
                        Create_By = SDR.GetInt32(5),
                        Update_Date = SDR.GetDateTime(6),
                        Update_By = SDR.GetInt32(7),
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
