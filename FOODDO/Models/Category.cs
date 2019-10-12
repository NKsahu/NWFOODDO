namespace FOODDO.Models
{
    public class Category
    {
        public int Category_ID { get; set; }
        public string Name { get; set; }
        public int Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public int Update_By { get; set; }
        public System.DateTime Update_Date { get; set; }
        public bool Deleted { get; set; }
        public static System.Collections.Generic.List<Category> List { get; set; }

        public Category()
        {
            this.Name = "";
            this.Create_Date = System.DateTime.Now;
            this.Update_Date = System.DateTime.Now;
        }

        public int Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.Category_ID == 0)
                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO CATEGORY (Name,Create_By,Create_Date,Update_By,Update_Date,Deleted) VALUES (@Name,@Create_By,@Create_Date,@Update_By,@Update_Date,@Deleted);select SCOPE_IDENTITY();", Obj.Con);
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("UPDATE CATEGORY SET Name=@Name,Create_By=@Create_By,Create_Date=@Create_Date,Update_By=@Update_By,Update_Date=@Update_Date,Deleted=@Deleted where Category_ID=@Category_ID", Obj.Con);
                    cmd.Parameters.AddWithValue("@Category_ID", this.Category_ID);
                }

                cmd.Parameters.AddWithValue("@Name", this.Name);
                cmd.Parameters.AddWithValue("@Create_By", this.Create_By);
                cmd.Parameters.AddWithValue("@Create_Date", this.Create_Date);
                cmd.Parameters.AddWithValue("@Update_By", this.Update_By);
                cmd.Parameters.AddWithValue("@Update_Date", System.DateTime.Now);
                cmd.Parameters.AddWithValue("@Deleted", this.Deleted);
                if (this.Category_ID == 0)
                {
                    this.Category_ID = System.Convert.ToInt32(cmd.ExecuteScalar());
                    if (this.Category_ID > 0)
                        Category.List.Insert(0, this);
                }
                else
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        Category.List.RemoveAll(x => x.Category_ID == this.Category_ID);
                        if (this.Deleted == false)
                            Category.List.Insert(0, this);
                    }
                }
            }
            catch (System.Exception e) { this.Category_ID = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.Category_ID;
        }

        public System.Collections.Generic.List<Category> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            System.Collections.Generic.List<Category> ListTmp = new System.Collections.Generic.List<Category>();
            Category ObjTmp = null;
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM CATEGORY WHERE Deleted=0 ORDER BY Category_ID DESC";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp = new Category
                    {
                        Category_ID = SDR.GetInt32(0),
                        Name = SDR.GetString(1),
                        Create_By = SDR.GetInt32(2),
                        Create_Date = SDR.GetDateTime(3),
                        Update_By = SDR.GetInt32(4),
                        Update_Date = SDR.GetDateTime(5),
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
