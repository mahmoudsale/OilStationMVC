using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data; 
using System.Data.Common;
using System.Data.OleDb;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Configuration;

namespace OilStationMVC.DataBase
{
    public class GenDB
    {
        public SqlConnection scnnNW;
        protected string PatchString = "";
        public string IntegratedSecur;
        public string DbLoginUser;
        public string DbLoginPassword;
        public string ServerName;
        public string DBName;
        string ConStr = "";




        public GenDB()
        {
            //string constrds = "Data Source=" + dt.Rows[0]["Server"] + ";Initial Catalog=" + dt.Rows[0]["ConfigDB"] + ";UID=" + dt.Rows[0]["UserId"] + "; Pwd=" + dt.Rows[0]["Password"] + "";
            //string constrds = "Data Source=DESKTOP-Q1PSSKP;Initial Catalog=Accountant;UID=sa; Pwd=123456";
        string constrds =     ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //"Data Source=SQL5006.site4now.net;Initial Catalog=DB_A657C1_Accountant;User Id=DB_A657C1_Accountant_admin;Password=YOUR_DB_PASSWORD
            //string constrds = "Server=DESKTOP-Q1PSSKP;Database=OilStationCore;Trusted_Connection=True;MultipleActiveResultSets=true";
            //string constrds = "Data Source=SQL5049.site4now.net;Initial Catalog=DB_A61213_OilStationCore;User Id=DB_A61213_OilStationCore_admin;Password=ms230289_m;";
            string SQL_CONNECTION_STRING = constrds;
            ConStr = constrds;

            scnnNW = new SqlConnection(SQL_CONNECTION_STRING);
            PatchString = "";
            ServerName = scnnNW.DataSource;
            DBName = scnnNW.Database;

        }

        #region Old
        public GenDB(string ConnStr)
        {
            string SQL_CONNECTION_STRING = ConnStr;
            //System.Configuration.ConfigurationManager.AppSettings["connectionstr"].ToString();

            scnnNW = new SqlConnection(SQL_CONNECTION_STRING);
            PatchString = "";
        }

        public void StartPatch()
        {
            PatchString = "";
        }

        public GenDB(bool useTools)
        {
            //this constructor don't initial the connection 
        }




        ~GenDB()
        {
            try
            {
                if (scnnNW.State == ConnectionState.Open)
                    scnnNW.Close();
            }
            catch (Exception exp)
            {

            }
            finally { }

        }


        List<string> lstStrSql;
        public void AddToPatch(string str)
        {
            if (lstStrSql == null)
                lstStrSql = new List<string>();

            lstStrSql.Add(str);
        }

        public void ClearPatch()
        {
            lstStrSql = null;
        }

        public async Task<ActionResult> Apply()
        {
            scnnNW.Close();
            scnnNW.Open();
            //scnnNW.Close();
            SqlTransaction transaction;

            // Start a local transaction.
            transaction = scnnNW.BeginTransaction("xTransaction");



            SqlCommand scmd = new SqlCommand();
            scmd.Connection = scnnNW;
            scmd.Transaction = transaction;

            try
            {
                for (int i = 0; i < lstStrSql.Count; i++)
                {
                    if (lstStrSql[i].Length > 5)
                    {
                        scmd.CommandText = lstStrSql[i];
                     await   scmd.ExecuteNonQueryAsync();
                    }
                }
                transaction.Commit();
                ClearPatch();

                return new ActionResult { Success = true };



            }
            catch (Exception exp)
            { 
                try
                {
                    ClearPatch();

                    transaction.Rollback();
                    return new ActionResult { Success = false, Message = "Rollback" };
                }
                catch (Exception ex2)
                {
                    ClearPatch();

                    return new ActionResult { Success = false, Message = "Connection Closed" };
                }

            }
            finally
            {
                ClearPatch();

                scnnNW.Close();

            }

        }
        public void DeleteRecords(string TableName, string KeyFields, string KeyValue, string Condition)
        {
            if (Condition == "") Condition = " 1=1 ";
            string strSQL = "Delete From " + TableName + " where " + KeyFields + " ='" + KeyValue + "' and " + Condition;

            SqlCommand scmd = new SqlCommand();
            scmd.CommandText = strSQL;
            scmd.Connection = scnnNW;
            scnnNW.Open();
            scmd.ExecuteNonQuery();
            scnnNW.Close();



        }
        public void DeleteRecords(string TableName, string KeyFields, string KeyValue, string Condition, bool IsNumeric)
        {
            string strSQL = "";
            if (Condition == "") Condition = " 1=1 ";
            if (IsNumeric)
                strSQL = "Delete From " + TableName + " where " + KeyFields + " =" + CDbl(KeyValue) + " and " + Condition;
            else
                strSQL = "Delete From " + TableName + " where " + KeyFields + " ='" + KeyValue + "' and " + Condition;

            SqlCommand scmd = new SqlCommand();
            scmd.CommandText = strSQL;
            scmd.Connection = scnnNW;
            scnnNW.Open();
            scmd.ExecuteNonQuery();
            scnnNW.Close();



        }
        public SqlConnection GetConnection()
        {
            return scnnNW;
        }

        public async Task<int> ExecutSQLAsync(string strSQL)
        {
            SqlCommand scmd = new SqlCommand();
            scmd.CommandText = strSQL;
            scmd.Connection = scnnNW;
            scnnNW.Open();
            int ret = await scmd.ExecuteNonQueryAsync();
            scnnNW.Close();
            return ret;
        }
         
        public void ExecutSQLInTrans(string strSQL)
        {
            AddToPatch(strSQL);
        }

        public int ExecutSQL(string strSQL)
        {
            SqlCommand scmd = new SqlCommand();
            scmd.CommandText = strSQL;
            scmd.Connection = scnnNW;
            scnnNW.Open();
            int ret = scmd.ExecuteNonQuery();
            scnnNW.Close();
            return ret;
        }

        public int ExecutSQLWithParm(string strSQL, DbParameter[] @params = null)
        {

            SqlCommand scmd = new SqlCommand();
            scmd.CommandText = strSQL;
            scmd.Connection = scnnNW;
            scnnNW.Open();
            if (@params != null)
            {
                for (int i = 0; i < @params.Length; i++)
                {
                    scmd.Parameters.Add(@params[i]);
                }
            }
            int ret = scmd.ExecuteNonQuery();
            scnnNW.Close();
            return ret;

        }
        public async Task<int> ExecutSQLWithParmAsync(string strSQL, DbParameter[] @params = null)
        {

            SqlCommand scmd = new SqlCommand();
            scmd.CommandText = strSQL;
            scmd.Connection = scnnNW;
            scnnNW.Open();
            if (@params != null)
            {
                for (int i = 0; i < @params.Length; i++)
                {
                    scmd.Parameters.Add(@params[i]);
                }
            }
            int ret = await scmd.ExecuteNonQueryAsync();
            scnnNW.Close();
            return ret;

        }
        public int ExecutSQL(string strSQL, string d)
        {
            SqlCommand scmd = new SqlCommand();
            scmd.CommandText = strSQL;
            scmd.Connection = scnnNW;
            scnnNW.Open();
            int ret = CInt(scmd.ExecuteScalar());
            scnnNW.Close();
            return ret;
        }

        public string GetNewKey(string sfield, string stable)
        {
            return GetValue("select isnull(max(" + sfield + "),0) +1 as NewKey from " + stable);
        }
        public async Task<string> GetNewKeyAsync(string sfield, string stable)
        {
            return await GetValueAsync("select isnull(max(" + sfield + "),0) +1 as NewKey from " + stable);
        }

        public async Task<string> GetNewKeyAsync(string sfield, string stable, string sWhere)
        {
            return await GetValueAsync("select isnull(max(" + sfield + "),0) +1 as NewKey from " + stable + " where " + sWhere);
        }
         public string GetNewKey (string sfield, string stable, string sWhere)
        {
            return   GetValue ("select isnull(max(" + sfield + "),0) +1 as NewKey from " + stable + " where " + sWhere);
        }

        public void InsertRecords(string strSQL)
        {
            SqlCommand scmd = new SqlCommand();
            scmd.CommandText = strSQL;
            scmd.Connection = scnnNW;
            scnnNW.Open();
            scmd.ExecuteNonQuery();
            scnnNW.Close();

        }

        public string GetValue(string strSQL)
        {
            object obj = "";
            try
            {
                SqlCommand scmd = new SqlCommand();
                scmd.CommandText = strSQL;
                scmd.Connection = scnnNW;

                scnnNW.Open();
                obj = scmd.ExecuteScalar();
                scnnNW.Close();
                if (ReferenceEquals(obj, null))
                    return "";
                return obj.ToString();
            }
            catch (Exception ex)
            {
                scnnNW.Close();
            }
            return obj.ToString();

        }

        public async Task<string> GetValueAsync(string strSQL)
        {
            object obj = "";
            try
            {
                SqlCommand scmd = new SqlCommand();
                scmd.CommandText = strSQL;
                scmd.Connection = scnnNW;

                scnnNW.Open();
                obj = await scmd.ExecuteScalarAsync();
                scnnNW.Close();
                if (ReferenceEquals(obj, null))
                    return "";
                return obj.ToString();
            }
            catch (Exception ex)
            {
                scnnNW.Close();
            }
            return obj.ToString();

        }

        public string GetValue(string sReturndValue, string sTableName, string sWhere)
        {

            SqlCommand scmd = new SqlCommand();
            scmd.CommandText = "select " + sReturndValue + " from " + sTableName + " where " + sWhere;
            scmd.Connection = scnnNW;
            scnnNW.Open();
            object obj = scmd.ExecuteScalar();
            scnnNW.Close();
            if (ReferenceEquals(obj, null))
                return "";
            return obj.ToString();


        }
        public async Task<string> GetValueAsync(string sReturndValue, string sTableName, string sWhere)
        {

            SqlCommand scmd = new SqlCommand();
            scmd.CommandText = "select " + sReturndValue + " from " + sTableName + " where " + sWhere;
            scmd.Connection = scnnNW;
            scnnNW.Open();
            object obj = await scmd.ExecuteScalarAsync();
            scnnNW.Close();
            if (ReferenceEquals(obj, null))
                return "";
            return obj.ToString();


        }


        public DataSet GetDataSet(string Tablename, string swhere)
        {
            string strSQL = "SELECT * FROM " + Tablename + " where " + swhere + " ";

            SqlDataAdapter sda = new SqlDataAdapter(strSQL, scnnNW);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;


        }
        public DataSet GetDataSet(string sqlSelect)
        {


            SqlDataAdapter sda = new SqlDataAdapter(sqlSelect, scnnNW);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
        public  Task<DataSet> GetDataSetAsync(string sqlSelect)
        {
            return Task.Run(() =>
            {
                using (SqlDataAdapter sda = new SqlDataAdapter(sqlSelect, scnnNW))
                {
                    DataSet ds = new DataSet();
                    sda.Fill(ds);
                    return  ds;
                }
            });
        }
        public SqlDataReader GetDataReader(string Tablename, string swhere)
        {
            string strSQL = "SELECT * FROM " + Tablename + " where " + swhere;

            SqlCommand scmd = new SqlCommand();
            scmd.CommandText = strSQL;
            scmd.Connection = scnnNW;
            SqlDataReader dr;

            scnnNW.Open();


            dr = scmd.ExecuteReader();

            return dr;


        }

        #region Converters

        public double CDbl(string p)
        {
            if (p == "")
                return 0;
            else
            {
                double ret = Convert.ToDouble(p);
                return ret;
            }

        }
        public double CDbl(object p)
        {
            if (p == null)
                return 0;
            else
            {
                double ret = Convert.ToDouble(p);
                return ret;
            }

        }
        public decimal CDec(object p)
        {
            if (p == null)
                return 0;
            else
            {
                decimal ret = Convert.ToDecimal(p);
                return ret;
            }

        }
        public int CInt(string p)
        {
            if (p == "")
                return 0;
            else
            {
                int ret = Convert.ToInt32(p);
                return ret;
            }

        }
        public int CInt(object p)
        {
            if (p == null)
                return 0;
            else
            {
                int ret = Convert.ToInt32(p);
                return ret;
            }

        }


        #endregion
        #endregion

    }

}
