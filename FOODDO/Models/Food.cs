namespace FOODDO.Models
{
    public class Food
    {
        public System.Int64 FID { get; set; }
        public string Food_Image { get; set; }
        public string Food_Name { get; set; }
        public double CostPrice { get; set; }
        public double Price { get; set; }// selling price 
        public string Qty { get; set; }
        public string Description { get; set; }
        public int Category_ID { get; set; }
        public int MID { get; set; }
        public bool Status { get; set; }
        public string Rating { get; set; }
        public int Create_By { get; set; }
        public System.DateTime Create_Date { get; set; }
        public int Update_By { get; set; }
        public System.DateTime Update_Date { get; set; }
        public bool Deleted { get; set; }
        public string FoodType { get; set; }
        public int AdminAprovalStatus { get; set; }// when food is created by mess ids
        public string MealsType { get; set; }
        public static System.Collections.Generic.List<Food> List { get; set; }
        public Food()
        {
            this.Food_Image = "";
            this.Food_Name = "";
            this.Qty = "";
            this.Description = "";
            this.Rating = "";
            this.Create_Date = System.DateTime.Now;
            this.Update_Date = System.DateTime.Now;
        }

        public System.Int64 Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.FID == 0)
                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO FOOD (Food_Image,Food_Name,SellPrice,Qty,Description,Category_ID,MID,Status,Rating,Create_By,Create_Date,Update_By,Update_Date,Deleted,FoodType,CostPrice,AdminAprovalStatus,MealsType) VALUES (@Food_Image,@Food_Name,@SellPrice,@Qty,@Description,@Category_ID,@MID,@Status,@Rating,@Create_By,@Create_Date,@Update_By,@Update_Date,@Deleted,@FoodType,@CostPrice,@AdminAprovalStatus,@MealsType);select SCOPE_IDENTITY();", Obj.Con);
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("UPDATE FOOD SET Food_Image=@Food_Image,Food_Name=@Food_Name,SellPrice=@SellPrice,Qty=@Qty,Description=@Description,Category_ID=@Category_ID,MID=@MID,Status=@Status,Rating=@Rating,Create_By=@Create_By,Create_Date=@Create_Date,Update_By=@Update_By,Update_Date=@Update_Date,Deleted=@Deleted,FoodType=@FoodType,CostPrice=@CostPrice,AdminAprovalStatus=@AdminAprovalStatus,MealsType=@MealsType where FID=@FID", Obj.Con);
                    cmd.Parameters.AddWithValue("@FID", this.FID);
                }
                cmd.Parameters.AddWithValue("@Food_Image", this.Food_Image);
                cmd.Parameters.AddWithValue("@Food_Name", this.Food_Name);
                cmd.Parameters.AddWithValue("@SellPrice", this.Price);
                cmd.Parameters.AddWithValue("@Qty", this.Qty);
                cmd.Parameters.AddWithValue("@Description", this.Description);
                cmd.Parameters.AddWithValue("@Category_ID", this.Category_ID);
                cmd.Parameters.AddWithValue("@MID", this.MID);
                cmd.Parameters.AddWithValue("@Status", this.Status);
                if (this.Rating == null)
                {
                    cmd.Parameters.AddWithValue("@Rating", System.DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Rating", this.Rating);
                }
                cmd.Parameters.AddWithValue("@Create_By", this.Create_By);
                cmd.Parameters.AddWithValue("@Create_Date", this.Create_Date);
                cmd.Parameters.AddWithValue("@Update_By", this.Update_By);
                cmd.Parameters.AddWithValue("@Update_Date", System.DateTime.Now);
                cmd.Parameters.AddWithValue("@Deleted", this.Deleted);
                cmd.Parameters.AddWithValue("@FoodType", this.FoodType);
                cmd.Parameters.AddWithValue("@CostPrice", this.CostPrice);
                cmd.Parameters.AddWithValue("@AdminAprovalStatus", this.AdminAprovalStatus);
                cmd.Parameters.AddWithValue("@MealsType", this.MealsType);
                if (this.FID == 0)
                {
                    this.FID = System.Convert.ToInt64(cmd.ExecuteScalar());
                    if (this.FID > 0)
                        Food.List.Insert(0, this);
                }
                else
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        Food.List.RemoveAll(x => x.FID == this.FID);
                        if (this.Deleted == false)
                            Food.List.Insert(0, this);
                    }
                }
            }
            catch (System.Exception e) { this.FID = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.FID;
        }

        public System.Collections.Generic.List<Food> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            System.Collections.Generic.List<Food> ListTmp = new System.Collections.Generic.List<Food>();
            Food ObjTmp = null;
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM FOOD WHERE Deleted=0 ORDER BY FID DESC";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp = new Food
                    {
                        FID = SDR.GetInt64(0),
                        Food_Image = SDR.GetString(1),
                        Food_Name = SDR.GetString(2),
                        Price = SDR.GetDouble(3),
                        Qty = SDR.GetString(4),
                        Description = SDR.GetString(5),
                        Category_ID = SDR.GetInt32(6),
                        MID = SDR.GetInt32(7),
                        Status = SDR.GetBoolean(8),
                        Rating =SDR.IsDBNull(9)?"0": SDR.GetString(9),
                        Create_By = SDR.GetInt32(10),
                        Create_Date = SDR.GetDateTime(11),
                        Update_By = SDR.GetInt32(12),
                        Update_Date = SDR.GetDateTime(13),
                        FoodType=SDR.IsDBNull(15)?"":SDR.GetString(15),
                        CostPrice=SDR.IsDBNull(16)?SDR.GetDouble(3):SDR.GetDouble(16),
                        AdminAprovalStatus=SDR.IsDBNull(17)?1:SDR.GetInt32(17),
                        MealsType=SDR.IsDBNull(18)?"All":SDR.GetString(18)
                        
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
