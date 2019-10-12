namespace FOODDO.Models
{
    public class Mess
    {
        public int MID { get; set; }
        public string Image { get; set; }
        public string Mess_Name { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string Owner_Name { get; set; }
        public string Bank_Name { get; set; }
        public string Account_No { get; set; }
        public string IFSC { get; set; }
        public string Aadhar_No { get; set; }
        public string Pan_No { get; set; }
        public int Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public int Update_By { get; set; }
        public System.DateTime Update_Date { get; set; }
        public bool Deleted { get; set; }
        public string Mobile2 { get; set; }
        public int NumberOfTifin { get; set; }
        public string MessType { get; set; }
        public static System.Collections.Generic.List<Mess> List { get; set; }

        public System.Collections.Generic.List<Food> FoodList { get; set; }

        public Mess()
        {
            this.Image = "";
            this.Mess_Name = "";
            this.Address = "";
            this.Mobile = "";
            this.Password = "";
            this.Owner_Name = "";
            this.Bank_Name = "";
            this.Account_No = "";
            this.IFSC = "";
            this.Aadhar_No = "";
            this.Pan_No = "";
            this.Create_Date = System.DateTime.Now;
            this.Update_Date = System.DateTime.Now;
            this.Mobile2 = "";
        }

        public int Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.MID == 0)
                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO MESS (Image,Mess_Name,Address,Mobile,Password,Owner_Name,Bank_Name,Account_No,IFSC,Aadhar_No,Pan_No,Create_By,Create_Date,Update_By,Update_Date,Deleted,Mobile2,NoOfTifin,MessType) VALUES (@Image,@Mess_Name,@Address,@Mobile,@Password,@Owner_Name,@Bank_Name,@Account_No,@IFSC,@Aadhar_No,@Pan_No,@Create_By,@Create_Date,@Update_By,@Update_Date,@Deleted,@Mobile2,@NoOfTifin,@MessType);select SCOPE_IDENTITY();", Obj.Con);
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("UPDATE MESS SET Image=@Image,Mess_Name=@Mess_Name,Address=@Address,Mobile=@Mobile,Password=@Password,Owner_Name=@Owner_Name,Bank_Name=@Bank_Name,Account_No=@Account_No,IFSC=@IFSC,Aadhar_No=@Aadhar_No,Pan_No=@Pan_No,Create_By=@Create_By,Create_Date=@Create_Date,Update_By=@Update_By,Update_Date=@Update_Date,Deleted=@Deleted,Mobile2=@Mobile2,NoOfTifin=@NoOfTifin,MessType=@MessType where MID=@MID", Obj.Con);
                    cmd.Parameters.AddWithValue("@MID", this.MID);
                }
                cmd.Parameters.AddWithValue("@Image", this.Image);
                cmd.Parameters.AddWithValue("@Mess_Name", this.Mess_Name);
                cmd.Parameters.AddWithValue("@Address", this.Address);
                cmd.Parameters.AddWithValue("@Mobile", this.Mobile);
                cmd.Parameters.AddWithValue("@Password", this.Password);
                cmd.Parameters.AddWithValue("@Owner_Name", this.Owner_Name);
                cmd.Parameters.AddWithValue("@Bank_Name", this.Bank_Name);
                cmd.Parameters.AddWithValue("@Account_No", this.Account_No);
                cmd.Parameters.AddWithValue("@IFSC", this.IFSC);
                cmd.Parameters.AddWithValue("@Aadhar_No", this.Aadhar_No);
                cmd.Parameters.AddWithValue("@Pan_No", this.Pan_No);
                cmd.Parameters.AddWithValue("@Create_By", this.Create_By);
                cmd.Parameters.AddWithValue("@Create_Date", this.Create_Date);
                cmd.Parameters.AddWithValue("@Update_By", this.Update_By);
                cmd.Parameters.AddWithValue("@Update_Date", System.DateTime.Now);
                cmd.Parameters.AddWithValue("@Deleted", this.Deleted);
                if (this.Mobile2 == null)
                {
                    cmd.Parameters.AddWithValue("@Mobile2", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Mobile2", this.Mobile2);
                }
                cmd.Parameters.AddWithValue("@NoOfTifin", this.NumberOfTifin);
                cmd.Parameters.AddWithValue("@MessType", this.MessType);
                if (this.MID == 0)
                {
                    this.MID = System.Convert.ToInt32(cmd.ExecuteScalar());
                    if (this.MID > 0)
                        Mess.List.Insert(0, this);
                }
                else
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        Mess.List.RemoveAll(x => x.MID == this.MID);
                        if (this.Deleted == false)
                            Mess.List.Insert(0, this);
                    }
                }
            }
            catch (System.Exception e) { this.MID = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.MID;
        }

        public System.Collections.Generic.List<Mess> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            System.Collections.Generic.List<Mess> ListTmp = new System.Collections.Generic.List<Mess>();
            Mess ObjTmp = null;
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM MESS WHERE Deleted=0 ORDER BY MID DESC";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp = new Mess
                    {
                        MID = SDR.GetInt32(0),
                        Image = SDR.GetString(1),
                        Mess_Name = SDR.GetString(2),
                        Address = SDR.GetString(3),
                        Mobile = SDR.GetString(4),
                        Password = SDR.GetString(5),
                        Owner_Name = SDR.GetString(6),
                        Bank_Name = SDR.GetString(7),
                        Account_No = SDR.GetString(8),
                        IFSC = SDR.GetString(9),
                        Aadhar_No = SDR.GetString(10),
                        Pan_No = SDR.GetString(11),
                        Create_By = SDR.GetInt32(12),
                        Create_Date = SDR.GetDateTime(13),
                        Update_By = SDR.GetInt32(14),
                        Update_Date = SDR.GetDateTime(15),
                        Mobile2=SDR.IsDBNull(17)?"":SDR.GetString(17),
                        NumberOfTifin=SDR.IsDBNull(18)?0:SDR.GetInt32(18),
                        MessType=SDR.IsDBNull(19)?"":SDR.GetString(19),
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
