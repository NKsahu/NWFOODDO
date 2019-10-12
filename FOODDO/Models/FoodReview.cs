namespace FOODDO.Models
{
    public class FoodReview
    {
        public System.Int64 FID { get; set; }
        public System.Int64 CID { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public System.DateTime Date_Time { get; set; }
        public bool Deleted { get; set; }
        public static System.Collections.Generic.List<FoodReview> List { get; set; }

        public FoodReview()
        {
            this.Comment = "";
            this.Date_Time = System.DateTime.Now;
        }

        public int Insert()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            int re = 0;
            try
            {
                cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO FOODREVIEW (FID,CID,Rating,Comment,Date_Time,Deleted) VALUES (@FID,@CID,@Rating,@Comment,@Date_Time,@Deleted);", Obj.Con);
                cmd.Parameters.AddWithValue("@FID", this.FID);
                cmd.Parameters.AddWithValue("@CID", this.CID);
                cmd.Parameters.AddWithValue("@Rating", this.Rating);
                cmd.Parameters.AddWithValue("@Comment", this.Comment);
                cmd.Parameters.AddWithValue("@Date_Time", this.Date_Time);
                cmd.Parameters.AddWithValue("@Deleted", this.Deleted);
                re = cmd.ExecuteNonQuery();
                if (re > 0)
                 FoodReview.List.Insert(0, this);
            }
            catch (System.Exception e) { re = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return re;
        }
        public System.Int64 Update()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                cmd = new System.Data.SqlClient.SqlCommand("UPDATE FOODREVIEW SET Rating=@Rating,Comment=@Comment,Date_Time=@Date_Time,Deleted=@Deleted where FID=@FID AND CID=@CID;", Obj.Con);
                cmd.Parameters.AddWithValue("@FID", this.FID);
                cmd.Parameters.AddWithValue("@CID", this.CID);
                cmd.Parameters.AddWithValue("@Rating", this.Rating);
                cmd.Parameters.AddWithValue("@Comment", this.Comment);
                cmd.Parameters.AddWithValue("@Date_Time", System.DateTime.Now);
                cmd.Parameters.AddWithValue("@Deleted", this.Deleted);
                
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        FoodReview.List.RemoveAll(x => x.FID == this.FID && x.CID==this.CID);
                        if (this.Deleted == false)
                            FoodReview.List.Insert(0, this);
                    }
            }
            catch (System.Exception e) { this.FID = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.FID;
        }


        public System.Collections.Generic.List<FoodReview> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            System.Collections.Generic.List<FoodReview> ListTmp = new System.Collections.Generic.List<FoodReview>();
            FoodReview ObjTmp = null;
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM FOODREVIEW WHERE Deleted=0 ORDER BY Date_Time DESC";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp = new FoodReview
                    {
                        FID = SDR.GetInt64(0),
                        CID = SDR.GetInt64(1),
                        Rating = SDR.GetInt32(2),
                        Comment = SDR.GetString(3),
                        Date_Time = SDR.GetDateTime(4),
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
