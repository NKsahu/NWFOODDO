namespace FOODDO.Models
{
    public class Ledger
    {
        public System.Int64 LID { get; set; }
        public System.Int64 CID { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
        public string Description { get; set; }
        public System.DateTime Entry_Date { get; set; }
        public bool Deleted { get; set; }
        public string LedgerType { get; set; }
        public static System.Collections.Generic.List<Ledger> List { get; set; }

        public Ledger()
        {
            this.Description = "";
            this.Entry_Date = System.DateTime.Now;
        }

        public System.Int64 Save()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            DBCon Obj = new DBCon();
            try
            {
                if (this.LID == 0)
                    cmd = new System.Data.SqlClient.SqlCommand("INSERT INTO LEDGER (CID,Debit,Credit,Description,Entry_Date,Deleted,LedgerType) VALUES (@CID,@Debit,@Credit,@Description,@Entry_Date,@Deleted,@LedgerType);select SCOPE_IDENTITY();", Obj.Con);
                else
                {
                    cmd = new System.Data.SqlClient.SqlCommand("UPDATE LEDGER SET CID=@CID,Debit=@Debit,Credit=@Credit,Description=@Description,Entry_Date=@Entry_Date,Deleted=@Deleted,@LedgerType=LedgerType where LID=@LID", Obj.Con);
                    cmd.Parameters.AddWithValue("@LID", this.LID);
                }

                cmd.Parameters.AddWithValue("@CID", this.CID);
                cmd.Parameters.AddWithValue("@Debit", this.Debit);
                cmd.Parameters.AddWithValue("@Credit", this.Credit);
                cmd.Parameters.AddWithValue("@Description", this.Description);
                cmd.Parameters.AddWithValue("@Entry_Date", this.Entry_Date);
                cmd.Parameters.AddWithValue("@Deleted", this.Deleted);
                if (this.LedgerType != null)
                    cmd.Parameters.AddWithValue("@LedgerType", this.LedgerType);
                else
                    cmd.Parameters.AddWithValue("@LedgerType", System.DBNull.Value);
                if (this.LID == 0)
                {
                    this.LID = System.Convert.ToInt64(cmd.ExecuteScalar());
                    if (this.LID > 0)
                        Ledger.List.Insert(0, this);
                }
                else
                {
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        Ledger.List.RemoveAll(x => x.LID == this.LID);
                        if (this.Deleted == false)
                            Ledger.List.Insert(0, this);
                    }
                }
            }
            catch (System.Exception e) { this.LID = 0; e.ToString(); }
            finally { cmd.Dispose(); Obj.Con.Close(); Obj.Con.Dispose(); Obj.Con = null; }
            return this.LID;
        }

        public System.Collections.Generic.List<Ledger> GetAll()
        {
            System.Data.SqlClient.SqlCommand cmd = null;
            System.Data.SqlClient.SqlDataReader SDR = null;
            System.Collections.Generic.List<Ledger> ListTmp = new System.Collections.Generic.List<Ledger>();
            Ledger ObjTmp = null;
            DBCon Obj = new DBCon();
            try
            {
                string Query = "SELECT * FROM LEDGER WHERE Deleted=0 ORDER BY LID DESC";
                cmd = new System.Data.SqlClient.SqlCommand(Query, Obj.Con);
                SDR = cmd.ExecuteReader();
                while (SDR.Read())
                {
                    ObjTmp = new Ledger
                    {
                        LID = SDR.GetInt64(0),
                        CID = SDR.GetInt64(1),
                        Debit = SDR.GetDouble(2),
                        Credit = SDR.GetDouble(3),
                        Description = SDR.GetString(4),
                        Entry_Date = SDR.GetDateTime(5),
                        LedgerType=SDR.IsDBNull(7)?"":SDR.GetString(7)
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
