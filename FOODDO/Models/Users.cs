namespace FOODDO.Models
{
    public class Users
    {
        public int UID { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public string User_Type { get; set; }
        public System.Int64 Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public System.Int64 Update_By { get; set; }
        public System.DateTime Update_Date { get; set; }
        public System.Int32 MESSID { get; set; }// mess id for filter oreder according to user
        public bool Deleted { get; set; }
        public static System.Collections.Generic.List<Users> List { get; set; }

        public Users()
        {
            this.User_Name = "";
            this.Password = "";
            this.User_Type = "";
            this.Create_Date = System.DateTime.Now;
            this.Update_Date = System.DateTime.Now;
            this.MESSID = 0;
        }

        public int Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.UID == 0)
                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO USERS (User_Name,Password,User_Type,Create_By,Create_Date,Update_By,Update_Date,Deleted,MESS_ID) VALUES (@User_Name,@Password,@User_Type,@Create_By,@Create_Date,@Update_By,@Update_Date,@Deleted,@MESS_ID);select SCOPE_IDENTITY();", Obj.Con);
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("UPDATE USERS SET User_Name=@User_Name,Password=@Password,User_Type=@User_Type,Create_By=@Create_By,Create_Date=@Create_Date,Update_By=@Update_By,Update_Date=@Update_Date,Deleted=@Deleted,MESS_ID=@MESS_ID where UID=@UID", Obj.Con);
                    cmd.Parameters.AddWithValue("@UID", this.UID);
                }
                cmd.Parameters.AddWithValue("@User_Name", this.User_Name);
                cmd.Parameters.AddWithValue("@Password", this.Password);
                cmd.Parameters.AddWithValue("@User_Type", this.User_Type);
                cmd.Parameters.AddWithValue("@Create_By", this.Create_By);
                cmd.Parameters.AddWithValue("@Create_Date", this.Create_Date);
                cmd.Parameters.AddWithValue("@Update_By", this.Update_By);
                cmd.Parameters.AddWithValue("@Update_Date", System.DateTime.Now);
                cmd.Parameters.AddWithValue("@Deleted", this.Deleted);
                cmd.Parameters.AddWithValue("@MESS_ID", this.MESSID);
                if (this.UID == 0)
                {
                    this.UID = System.Convert.ToInt32(cmd.ExecuteScalar());
                    if (this.UID > 0)
                        Users.List.Insert(0, this);
                }
                else
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        Users.List.RemoveAll(x => x.UID == this.UID);
                        if (this.Deleted == false)
                            Users.List.Insert(0, this);
                    }
                }
            }
            catch (System.Exception e) { this.UID = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.UID;
        }

        public System.Collections.Generic.List<Users> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            System.Collections.Generic.List<Users> ListTmp = new System.Collections.Generic.List<Users>();
            Users ObjTmp = null;
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM USERS WHERE Deleted=0 ORDER BY UID DESC";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp = new Users
                    {
                        UID = SDR.GetInt32(0),
                        User_Name = SDR.GetString(1),
                        Password = SDR.GetString(2),
                        User_Type = SDR.GetString(3),
                        Create_By = SDR.GetInt64(4),
                        Create_Date = SDR.GetDateTime(5),
                        Update_By = SDR.GetInt64(6),
                        Update_Date = SDR.GetDateTime(7),
                        MESSID = SDR.IsDBNull(9)? 0:SDR.GetInt32(9),
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
