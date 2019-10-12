namespace FOODDO.Models
{
    public class Customer
    {
        public System.Int64 CID { get; set; }
        public string Name { get; set; }
        public System.DateTime Birthday { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public System.DateTime Signup_Date { get; set; }
        public System.DateTime Update_Date { get; set; }
        public bool Deleted { get; set; }
        public static System.Collections.Generic.List<Customer> List { get; set; }

        public Customer()
        {
            this.Name = "";
            this.Birthday = System.DateTime.Now;
            this.Email = "";
            this.Mobile = "";
            this.Password = "";
            this.Signup_Date = System.DateTime.Now;
            this.Update_Date = System.DateTime.Now;
        }

        public System.Int64 Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.CID == 0)
                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO CUSTOMER (Name,Birthday,Email,Mobile,Password,Signup_Date,Update_Date,Deleted) VALUES (@Name,@Birthday,@Email,@Mobile,@Password,@Signup_Date,@Update_Date,@Deleted);select SCOPE_IDENTITY();", Obj.Con);
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("UPDATE CUSTOMER SET Name=@Name,Birthday=@Birthday,Email=@Email,Mobile=@Mobile,Password=@Password,Signup_Date=@Signup_Date,Update_Date=@Update_Date,Deleted=@Deleted where CID=@CID", Obj.Con);
                    cmd.Parameters.AddWithValue("@CID", this.CID);
                }

                cmd.Parameters.AddWithValue("@Name", this.Name);
                cmd.Parameters.AddWithValue("@Birthday", this.Birthday);
                cmd.Parameters.AddWithValue("@Email", this.Email);
                cmd.Parameters.AddWithValue("@Mobile", this.Mobile);
                cmd.Parameters.AddWithValue("@Password", this.Password);
                cmd.Parameters.AddWithValue("@Signup_Date", this.Signup_Date);
                cmd.Parameters.AddWithValue("@Update_Date", System.DateTime.Now);
                cmd.Parameters.AddWithValue("@Deleted", this.Deleted);
                if (this.CID == 0)
                {
                    this.CID = System.Convert.ToInt64(cmd.ExecuteScalar());
                    if (this.CID > 0)
                        Customer.List.Insert(0, this);
                }
                else
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        Customer.List.RemoveAll(x => x.CID == this.CID);
                        if (this.Deleted == false)
                            Customer.List.Insert(0, this);
                    }
                }
            }
            catch (System.Exception e) { this.CID = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.CID;
        }

        public System.Collections.Generic.List<Customer> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            System.Collections.Generic.List<Customer> ListTmp = new System.Collections.Generic.List<Customer>();
            Customer ObjTmp = null;
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM CUSTOMER WHERE Deleted=0 ORDER BY CID DESC";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp = new Customer
                    {
                        CID = SDR.GetInt64(0),
                        Name = SDR.GetString(1),
                        Birthday = SDR.GetDateTime(2),
                        Email = SDR.GetString(3),
                        Mobile = SDR.GetString(4),
                        Password = SDR.GetString(5),
                        Signup_Date = SDR.GetDateTime(6),
                        Update_Date = SDR.GetDateTime(7),
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
