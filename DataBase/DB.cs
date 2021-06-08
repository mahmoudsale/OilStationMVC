using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace OilStationMVC.DataBase
{
    public class DB : GenDB
    {
        public DB()
        {

        }

        public DB(string ConnStr) : base(ConnStr)
        {

        }

        public void CreateTable(string TableName, string sql)
        {
            try
            {
                if (!ISExist("INFORMATION_SCHEMA.tables", " TABLE_NAME = '" + TableName + "'"))
                {
                    ExecutSQLAsync(sql);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void CreateView(string ViewName, string sql)
        {
            try
            {
                if (!ISExist("INFORMATION_SCHEMA.tables", " TABLE_NAME = '" + ViewName + "'"))
                {
                    ExecutSQLAsync(sql);
                }
            }
            catch (Exception ex)
            {

            }
        }
        public void AlterView(string ViewName, string sql)
        {
            try
            {
                if (ISExist("INFORMATION_SCHEMA.VIEWS", " TABLE_NAME = '" + ViewName + "'"))
                {
                    ExecutSQLAsync(sql);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void AddUpdateToReportTable(string FormName, string ReportName, string ReportArabicName, string ViewName, string nModuleNo, string ReportEnglishName = null, string FirstSubReportName = null, string SecondtSubReportName = null, string ThirdSubReportName = null, string FormParameterKeys = null)
        {
            try
            {
                if (ISExist("ReportTable", " FormName= '" + FormName + "' and MainReportName='" + ReportName + "'"))
                {
                    string sql = " update ReportTable set MainReportName='" + ReportName + "' , FirstSubReportName='" + FirstSubReportName + "'";
                    sql += " , SecondtSubReportName='" + SecondtSubReportName + "' , ThirdSubReportName='" + ThirdSubReportName + "'";
                    sql += " ,ReportArabicName='" + ReportArabicName + "',ReportEnglishName='" + ReportEnglishName + "' ,ViewName='" + ViewName + "',FormParameterKeys='" + FormParameterKeys + "'";
                    sql += " ,nModuleNo=" + nModuleNo + "";
                    sql += " where FormName= '" + FormName + "' and MainReportName='" + ReportName + "'";
                    ExecutSQLAsync(sql);
                }
                else
                {
                    string sql = @" INSERT INTO [dbo].[ReportTable]
                               ([nBranchId]
                               ,[FormName]
                               ,[MainReportName]
                               ,[FirstSubReportName]
                               ,[SecondtSubReportName]
                               ,[ThirdSubReportName]
                               ,[ReportArabicName]
                               ,[ReportEnglishName]
                               ,[ViewName]
                               ,[nModuleNo]
                                ,FormParameterKeys)
                               VALUES(1,'" + FormName + "','" + ReportName + "' ,'" + FirstSubReportName + "','" + SecondtSubReportName + "'  ,'" + ThirdSubReportName + "','" + ReportArabicName + "'  ,'" + ReportEnglishName + "','" + ViewName + "' ,'" + nModuleNo + "','" + FormParameterKeys + "')";
                    ExecutSQLAsync(sql);
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void AddUpdateAnyTable(string TableName, string PrimaryKeyFilter, string SetColumnsString = null, string InsertSql = null)
        {
            try
            {
                if (ISExist(TableName, PrimaryKeyFilter))
                {
                    if (!string.IsNullOrEmpty(SetColumnsString))
                    {
                        string sql = " update " + TableName + " set " + SetColumnsString + "";
                        sql += " where " + PrimaryKeyFilter + "";
                        ExecutSQLAsync(sql);
                    }

                }
                else
                {
                    if (!string.IsNullOrEmpty(InsertSql))
                    {
                        string sql = @" INSERT INTO " + TableName + " " + InsertSql + "";


                        ExecutSQLAsync(sql);
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }
        public void AddUpdateToSearchMaster(string TableID, string TableName, string Conditions, string FormTextar, string GrdWidth, string CodeField, string NameField, string sInsertForm = null, string FormTexteng = null)
        {
            try
            {
                if (ISExist("SearchMaster", " TableID= '" + TableID + "' "))
                {
                    string sql = @" UPDATE [dbo].[SearchMaster]
                               SET TableID = '" + TableID + "' ,TableName = '" + TableName + "',   Conditions = '" + Conditions + "' , FormTextar = '" + FormTextar + "'   ,FormTexteng = '" + FormTexteng + "'";
                    sql += "  ,GrdWidth = " + GrdWidth + ",CodeField = '" + CodeField + "',NameField = '" + NameField + "',sInsertForm = '" + sInsertForm + "'";
                    sql += " WHERE TableID = '" + TableID + "'";

                    ExecutSQLAsync(sql);

                }
                else
                {
                    string sql = @" INSERT INTO [dbo].[SearchMaster]
                           ([TableID]
                           ,[TableName]
                           ,[Conditions]
                           ,[FormTextar]
                           ,[FormTexteng]
                           ,[GrdWidth]
                           ,[CodeField]
                           ,[NameField]
                           ,[sInsertForm])
                               VALUES('" + TableID + "','" + TableName + "','" + Conditions + "' ,'" + FormTextar + "','" + FormTexteng + "'  ,'" + GrdWidth + "','" + CodeField + "'  ,'" + NameField + "','" + sInsertForm + "' )";
                    ExecutSQLAsync(sql);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void AddUpdateToSearchDetails(string TableID, string ColId, string ColName, string ColHeaderAr, string ColHeaderEng, string ColWidth, int ColVisible)
        {
            try
            {
                if (ISExist("SearchDetails", " TableID= '" + TableID + "' and  ColId='" + ColId + "'"))
                {
                    string sql = @" UPDATE [dbo].[SearchDetails]
                               SET TableID = '" + TableID + "' ,ColId = '" + ColId + "',   ColName = '" + ColName + "' , ColHeaderAr = '" + ColHeaderAr + "'   ,ColHeaderEng = '" + ColHeaderEng + "'";
                    sql += "  ,ColWidth = " + ColWidth + ",ColVisible = " + ColVisible + "";
                    sql += " WHERE TableID = '" + TableID + "' and  ColId='" + ColId + "'";

                    ExecutSQLAsync(sql);

                }
                else
                {
                    string sql = @" INSERT INTO [dbo].[SearchDetails]
           ([TableID]
           ,[ColId]
           ,[ColName]
           ,[ColHeaderAr]
           ,[ColHeaderEng]
           ,[ColWidth]
           ,[ColVisible])
                               VALUES('" + TableID + "','" + ColId + "','" + ColName + "' ,'" + ColHeaderAr + "','" + ColHeaderEng + "'  ,'" + ColWidth + "','" + ColVisible + "'  )";
                    ExecutSQLAsync(sql);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void AlterTable(string TableName, string FiledName = null, string DataType = null, string DefaultValue = null, string sqlAlter = null)
        {
            try
            {
                if (ISExist("INFORMATION_SCHEMA.tables", " TABLE_NAME = '" + TableName + "'"))
                {
                    if (!string.IsNullOrEmpty(FiledName))
                    {
                        if (TestField(TableName, FiledName) == false)
                        {
                            ExecutSQLAsync("Alter table " + TableName + " add " + FiledName + " " + DataType);
                            if (!string.IsNullOrEmpty(DefaultValue))
                                ExecutSQLAsync("update " + TableName + " set " + FiledName + "=" + DefaultValue);
                        }
                    }

                    if (!string.IsNullOrEmpty(sqlAlter))
                    {
                        ExecutSQLAsync(sqlAlter);
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void AlterTablePK(string TableName, string PkConstraint, bool AddOrRemove, string PKColumns = null)
        {
            string sql = @"SELECT     X.NAME AS INDEXNAME,
                               COL_NAME(IC.OBJECT_ID,IC.COLUMN_ID) AS COLUMNNAME
                    FROM       SYS.INDEXES  X 
                    INNER JOIN SYS.INDEX_COLUMNS  IC 
                            ON X.OBJECT_ID = IC.OBJECT_ID
                           AND X.INDEX_ID = IC.INDEX_ID ";
            if (AddOrRemove)
            {
                if (!ISExistWithSelectQuery(sql, " X.IS_PRIMARY_KEY = 1 AND      OBJECT_NAME(IC.OBJECT_ID)='" + TableName + "'"))
                {
                    string sqlstr = "alter table " + TableName + " ADD CONSTRAINT " + PkConstraint + " PRIMARY KEY " + PKColumns + "";
                    ExecutSQLAsync(sqlstr);
                }
            }
            else
            {
                if (ISExistWithSelectQuery(sql, " X.IS_PRIMARY_KEY = 1 AND      OBJECT_NAME(IC.OBJECT_ID)='" + TableName + "'"))
                {
                    string sqlstr = "alter table " + TableName + " drop constraint " + PkConstraint + " ";
                    ExecutSQLAsync(sqlstr);
                }
            }

        }
        public void AddToMenu(string sMenuName, string sMenuNameEng, string nParent, bool nForm, string nModuleId, bool nHide, string FormName, string FormType, string FormParameter, string RelatedConfigration, string TableName, string TableId = null)
        {
            try
            {
                if (!ISExist("menu", " FormName = '" + FormName + "' and FormParameter='" + FormParameter + "'"))
                {
                    DB db = new DB();
                    db.NewFields();
                    db.Table = "menu";
                    db.AddFieldstr("Nid", db.CInt(db.GetNewKey("nId", "menu")).ToString());
                    db.AddFieldstr("nBranchId", "1");
                    db.AddFieldstr("sMenuName", sMenuName);
                    db.AddFieldstr("sMenuNameEng", sMenuNameEng);
                    db.AddFieldstr("nParent", nParent);
                    db.AddFieldstr("nForm", nForm.ToString());
                    db.AddFieldstr("nModuleId", nModuleId);
                    db.AddFieldstr("nHide", nHide.ToString());
                    db.AddFieldstr("FormName", FormName);
                    db.AddFieldstr("FormType", FormType);
                    db.AddFieldstr("FormParameter", FormParameter);
                    db.AddFieldstr("RelatedConfigration", RelatedConfigration);
                    db.AddFieldstr("TableName", TableName);
                    if (!string.IsNullOrEmpty(TableId))
                    {
                        db.AddFieldstr("TableId", TableId);
                    }
                    db.ExecInsert();


                }
            }
            catch (Exception ex)
            {

                //throw;
            }
        }

        public DB(bool usetool)
            : base(usetool)
        {

        }
        private Hashtable FieldsWithValue;

        private string _table;
        private string _sCondition;


        public string Table
        {
            get
            {
                return _table;
            }
            set
            {
                _table = value;
            }
        }

        public string sCondition
        {
            get
            {
                return _sCondition;
            }
            set
            {
                _sCondition = value;
            }
        }

        public void CreateField(string Table, string Field, string ttype, string ValueToUpdate)
        {
            try
            {

                if (TestField(Table, Field) == false)
                {
                    ExecutSQL("Alter table " + Table + " add " + Field + " " + ttype);
                    ExecutSQL("update " + Table + " set " + Field + "=" + ValueToUpdate);
                }

            }
            catch (Exception e)
            {


            }
        }
        object synObj = new object();
        public DataTable GetDataTable(string sql)
        {
            IDbDataAdapter DA;
            IDbCommand cmd;
            if (sql == "") return new DataTable();
            DataSet ds = new DataSet();
            lock (synObj)
            {
                cmd = new SqlCommand("", this.GetConnection() as SqlConnection);
                DA = new SqlDataAdapter("", this.GetConnection() as SqlConnection);
                cmd.CommandText = sql;
                DA.SelectCommand = cmd;
                if (this.GetConnection().State != ConnectionState.Open) this.GetConnection().Open();

                DA.SelectCommand = cmd;
                DA.Fill(ds);

                this.GetConnection().Close();
            }
            return DA.FillSchema(ds, SchemaType.Source)[0];



        }
        public Task<DataTable> GetDataTableAsync(string sql)
        { 
            SqlCommand cmd;
            if (sql == "")
            {
                return Task.Run(() =>
                {
                    return new DataTable(); 
                });
            } 
            DataSet ds = new DataSet();
            lock (synObj)
            {
                return Task.Run(() =>
                { 
                    using (SqlDataAdapter DA = new SqlDataAdapter("", this.GetConnection() as SqlConnection))
                    {
                        cmd = new SqlCommand("", this.GetConnection() as SqlConnection); 
                        cmd.CommandText = sql;
                        DA.SelectCommand = cmd;
                        if (this.GetConnection().State != ConnectionState.Open) this.GetConnection().Open(); 
                        DA.SelectCommand = cmd;
                        DA.Fill(ds); 
                        this.GetConnection().Close();
                        return DA.FillSchema(ds, SchemaType.Source)[0];
                    }
                }); 
            } 
        }

        //public DataTable GetDataTable2(string sql)
        //{
        //    //    IDbDataAdapter DA;
        //    //    IDbCommand cmd;
        //    if (sql == "") return new DataTable();
        //    DataSet ds = new DataSet();

        //    lock (synObj)
        //    {

        //        //cmd = new SqlCommand("", this.GetConnection() as SqlConnection);
        //        //DA = new SqlDataAdapter("", this.GetConnection() as SqlConnection);
        //        cmd.CommandText = sql;

        //        if (this.GetConnection().State != ConnectionState.Open) this.GetConnection().Open();

        //        DA.SelectCommand = cmd;
        //        DA.Fill(ds);

        //        this.GetConnection().Close();
        //    }

        //    return DA.FillSchema(ds, SchemaType.Source)[0];
        //}
        public Boolean ISExist(string TableName, string Criteria)
        {
            DataTable dt = new DataTable();

            lock (synObj)
            {
                string SQL = " SELECT COUNT(*) FROM " + TableName;
                if (!String.IsNullOrEmpty(Criteria))
                {
                    if (Criteria.ToUpper().Contains("WHERE")) SQL += Criteria;
                    else SQL += " WHERE " + Criteria;
                }
                dt = GetDataTable(SQL);
            }

            if (dt.Rows.Count > 0 && Convert.ToInt32(dt.Rows[0][0]) > 0) return true;
            else return false;
        }
        public Boolean ISExistWithSelectQuery(string SqlQuery, string Criteria)
        {
            DataTable dt = new DataTable();

            lock (synObj)
            {
                string SQL = SqlQuery;
                if (!String.IsNullOrEmpty(Criteria))
                {
                    if (Criteria.ToUpper().Contains("WHERE")) SQL += Criteria;
                    else SQL += " WHERE " + Criteria;
                }
                dt = GetDataTable(SQL);
            }

            if (dt.Rows.Count > 0) return true;
            else return false;
        }

        public bool TestField(string Table, string Field)
        {
            try
            {
                System.Data.DataSet ds = GetDataSet("select Top 1 " + Field + " from " + Table);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public void NewFields()
        {
            if (FieldsWithValue != null)
                FieldsWithValue.Clear();

        }
        public void ExecuteInsertDatatable(DataTable tbInsertTarget, string tableName)
        {


            try
            {
                lock (synObj)
                {
                    //Khairy 03-2013


                    if (this.GetConnection().State != ConnectionState.Open) this.GetConnection().Open();
                    //if (WithTransaction) cmd.Transaction = _transaction;

                    SqlBulkCopy bulkCopy = new SqlBulkCopy(this.GetConnection().ConnectionString, SqlBulkCopyOptions.TableLock)
                    {
                        DestinationTableName = tableName,
                        BatchSize = 100000,
                        BulkCopyTimeout = 360
                    };
                    bulkCopy.WriteToServer(tbInsertTarget);

                    this.GetConnection().Close();
                }

            }
            catch (Exception ex)
            {

            }
        }
        public void RemoveField(string FieldName)
        {
            FieldsWithValue.Remove(FieldName);
        }
        public void AddField(string Filedname, string FieldValue)
        {
            if (FieldsWithValue == null)
                FieldsWithValue = new Hashtable();
            if (FieldValue == "False" | FieldValue == "false")
                FieldValue = "0";
            if (FieldValue == "True" | FieldValue == "true")
                FieldValue = "1";
            if (FieldValue == "")
                FieldValue = "0";

            FieldsWithValue.Add(Filedname, FieldValue);

        }
        public void ExecTruncate()
        {
            string sTruncate = "truncate table " + Table;
            ExecutSQLAsync(sTruncate);
        }
        public void AddFieldstr(string Filedname, string FieldValue)
        {
            if (FieldsWithValue == null)
                FieldsWithValue = new Hashtable();


            FieldsWithValue.Add(Filedname, "'" + FieldValue + "'");

        }

        public void AddFieldimg(string Filedname, string FieldValue)
        {
            if (FieldsWithValue == null)
                FieldsWithValue = new Hashtable();


            FieldsWithValue.Add(Filedname, "convert(VARBINARY(max), '" + FieldValue + "') ");

        }

        public void AddFieldG(string Filedname, string FieldValue)
        {
            if (Filedname.StartsWith("n"))
                AddField(Filedname, FieldValue);
            else
                AddFieldstr(Filedname, FieldValue);

        }


        public string GetInsertStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Insert Into " + Table + "(");
            foreach (DictionaryEntry entry in FieldsWithValue)
            {
                sb.Append(entry.Key.ToString() + ",");
            }
            sb = sb.Remove(sb.Length - 1, 1);

            sb.Append(") Values (");
            foreach (DictionaryEntry entry in FieldsWithValue)
            {
                sb.Append(entry.Value.ToString() + ",");
            }

            sb = sb.Remove(sb.Length - 1, 1);
            sb.Append(")");
            return sb.ToString();
        }


        public int ExecInsert()
        {
            return ExecutSQL(GetInsertStr());
        }
        public async Task<int> ExecInsertAsync()
        {
            return await ExecutSQLAsync(GetInsertStr());
        }
        public int ExecInsertAndREturnIdentityRowNo(string Query)
        {
            return ExecutSQL(Query, "");
        }


        public void ExecInsertInTrans()
        {
            AddToPatch(GetInsertStr());
        }

        public void ExecUpdateInTrans()
        {
            AddToPatch(GetUpdateStr());

        }


        public void ExecDeleteInTrans()
        {
            string sDel = "Delete from " + Table + " where " + sCondition;
            AddToPatch(sDel);
        }

        public string GetUpdateStr()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Update " + Table + " Set ");
            foreach (DictionaryEntry entry in FieldsWithValue)
            {
                sb.Append(entry.Key.ToString() + "=");
                sb.Append(entry.Value.ToString() + ",");

            }

            sb = sb.Remove(sb.Length - 1, 1);

            sb.Append(" Where " + sCondition);

            return sb.ToString();
        }
        public int ExecUpdate()
        {
            return ExecutSQL(GetUpdateStr());
        }

        public void ExecDelete()
        {
            string sDel = "Delete from " + Table + " where " + sCondition;
            ExecutSQL(sDel);
        }
        public async Task<int> ExecUpdateAsync()
        {
            return await ExecutSQLAsync(GetUpdateStr());
        }

        public async Task<int> ExecDeleteAsync()
        {
            string sDel = "Delete from " + Table + " where " + sCondition;
            return await ExecutSQLAsync(sDel);
        }



        public static string CnvToHijri(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public static bool chkhijri(string p)
        {
            throw new NotImplementedException();
        }

        public static DateTime CnvToGreg(string p)
        {
            throw new NotImplementedException();
        }

        //internal string GetNewKey(string KeyField, string TableName)
        //{
        //    throw new NotImplementedException();
        //}


    }
}
