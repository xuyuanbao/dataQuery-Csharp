using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Data.OracleClient;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Text;

namespace MyDB
{
    public class Database : IDbExecute
    {
        public DataSet dataSet;
        public DbDataReader dataReader;

        private DBStatusENum _dbStatus;
        private DbConfig _DbConfig = new DbConfig();
        private DbExecute<SqlConnection, SqlCommand, SqlDataAdapter, SqlDataReader> SqlExecute;
        private DbExecute<SqlCeConnection, SqlCeCommand, SqlCeDataAdapter, SqlCeDataReader> SqlCeExecute;
        private DbExecute<MySqlConnection, MySqlCommand, MySqlDataAdapter, MySqlDataReader> MySqlExecute;
        private DbExecute<OracleConnection, OracleCommand, OracleDataAdapter, OracleDataReader> OracleExecute;

        public DBStatusENum DBStatus
        {
            get { return _dbStatus;}
            set { _dbStatus=value ;}
        }

        public DbConfig Config
        {
            get { return _DbConfig;}
            set
            {
                _DbConfig = value;
            }
        }

        public  bool InitDB()
        {
            try
            {
                if (Config.LoadSettings())
                {
                    DBStatus = DBStatusENum.Initialized;
                    if (Open())
                    {
                        DBStatus = DBStatusENum.Opening;
                        return true;
                    }
                }
                else
                {
                    DBStatus = DBStatusENum.NotInitialed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MyDB.DbConfigManager ConfigManager = new MyDB.DbConfigManager();
                if (ConfigManager.ShowDialog() == DialogResult.OK)
                {
                    if (InitDB() == false)
                    {
                        MessageBox.Show("数据库连接失败！");
                    }
                }
                ConfigManager.Dispose();
            }
            return false;
        }

        public bool Open()
        {
            if (DBStatus == DBStatusENum.Opening)
                return true;

            bool bSuccessOpened = false;
            try
            {
                switch (Config.ServerType)
                {
                    case DbServerType.SqlServer:
                        if (SqlExecute == null) SqlExecute = new DbExecute<SqlConnection, SqlCommand, SqlDataAdapter, SqlDataReader>();
                        SqlExecute.dbConnection.ConnectionString = Config.ConnectionString;
                        SqlExecute.dbConnection.Open();
                        bSuccessOpened = true;
                        break;
                    case DbServerType.SqlServerCe:
                        if (SqlCeExecute == null) SqlCeExecute = new DbExecute<SqlCeConnection, SqlCeCommand, SqlCeDataAdapter, SqlCeDataReader>();
                        SqlCeExecute.dbConnection.ConnectionString = Config.ConnectionString;
                        SqlCeExecute.dbConnection.Open();
                        bSuccessOpened = true;
                        break;
                    case DbServerType.MySql:
                        if (MySqlExecute == null) MySqlExecute = new DbExecute<MySqlConnection, MySqlCommand, MySqlDataAdapter, MySqlDataReader>();
                        MySqlExecute.dbConnection.ConnectionString = Config.ConnectionString;
                        MySqlExecute.dbConnection.Open();
                        bSuccessOpened = true;
                        break;
                    case DbServerType.Oracle:
                        if (OracleExecute == null) OracleExecute = new DbExecute<OracleConnection, OracleCommand, OracleDataAdapter, OracleDataReader>();
                        OracleExecute.dbConnection.ConnectionString = Config.ConnectionString;
                        OracleExecute.dbConnection.Open();
                        bSuccessOpened = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
              
            }
            finally
            {

            }
            return bSuccessOpened;
        }

        public void Close()
        {
            try
            {
                switch (Config.ServerType)
                {
                    case DbServerType.SqlServer:
                        if (SqlExecute != null)
                        {
                            if (SqlExecute.dbConnection.State == ConnectionState.Open) SqlExecute.dbConnection.Close();
                        }
                        break;
                    case DbServerType.SqlServerCe:
                        if (SqlCeExecute != null)
                        {
                            if (SqlCeExecute.dbConnection.State == ConnectionState.Open) SqlCeExecute.dbConnection.Close();
                        }
                        break;
                    case DbServerType.MySql:
                        if (MySqlExecute != null)
                        {
                            if (MySqlExecute.dbConnection.State == ConnectionState.Open) MySqlExecute.dbConnection.Close();
                        }
                        break;
                    case DbServerType.Oracle:
                        if (OracleExecute != null)
                        {
                            if (OracleExecute.dbConnection.State == ConnectionState.Open) OracleExecute.dbConnection.Close();
                        }
                        break;
                    default:
                        break;
                }
                DBStatus = DBStatusENum.Closed;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {

            }
        }

        public bool ExecuteReader(string sqlText)
        {
            if (DBStatus != DBStatusENum.Opening)
                return false;
            bool bSuccessExecuted = false;
            try
            {
                switch (Config.ServerType)
                {
                    case DbServerType.SqlServer:
                        if (SqlExecute == null) SqlExecute = new DbExecute<SqlConnection, SqlCommand, SqlDataAdapter, SqlDataReader>(Config.ConnectionString);
                        bSuccessExecuted = SqlExecute.ExecuteReader(sqlText);
                        if (bSuccessExecuted) dataReader = SqlExecute.dbDataReader;
                        break;
                    case DbServerType.SqlServerCe:
                        if (SqlCeExecute == null) SqlCeExecute = new DbExecute<SqlCeConnection, SqlCeCommand, SqlCeDataAdapter, SqlCeDataReader>(Config.ConnectionString);
                        bSuccessExecuted = SqlCeExecute.ExecuteReader(sqlText);
                        if (bSuccessExecuted) dataReader = SqlCeExecute.dbDataReader;
                        break;
                    case DbServerType.MySql:
                        if (MySqlExecute == null) MySqlExecute = new DbExecute<MySqlConnection, MySqlCommand, MySqlDataAdapter, MySqlDataReader>(Config.ConnectionString);
                        bSuccessExecuted = MySqlExecute.ExecuteReader(sqlText);
                        if (bSuccessExecuted) dataReader = MySqlExecute.dbDataReader;
                        break;
                    case DbServerType.Oracle:
                        if (OracleExecute == null) OracleExecute = new DbExecute<OracleConnection, OracleCommand, OracleDataAdapter, OracleDataReader>(Config.ConnectionString);
                        bSuccessExecuted = OracleExecute.ExecuteReader(sqlText);
                        if (bSuccessExecuted) dataReader = OracleExecute.dbDataReader;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {

            }
            return bSuccessExecuted;
        }

        public bool ExecuteDataSet(string sqlText)
        {
            if (DBStatus != DBStatusENum.Opening)
                return false;
            bool bSuccessExecuted = false;
            try
            {
                switch (Config.ServerType)
                {
                    case DbServerType.SqlServer:
                        if (SqlExecute == null) SqlExecute = new DbExecute<SqlConnection, SqlCommand, SqlDataAdapter, SqlDataReader>(Config.ConnectionString);
                        bSuccessExecuted = SqlExecute.ExecuteDataSet(sqlText);
                        if (bSuccessExecuted) dataSet = SqlExecute.dataSet;
                        break;
                    case DbServerType.SqlServerCe:
                        if (SqlCeExecute == null) SqlCeExecute = new DbExecute<SqlCeConnection, SqlCeCommand, SqlCeDataAdapter, SqlCeDataReader>(Config.ConnectionString);
                        bSuccessExecuted = SqlCeExecute.ExecuteDataSet(sqlText);
                        if (bSuccessExecuted) dataSet = SqlCeExecute.dataSet;
                        break;
                    case DbServerType.MySql:
                        if (MySqlExecute == null) MySqlExecute = new DbExecute<MySqlConnection, MySqlCommand, MySqlDataAdapter, MySqlDataReader>(Config.ConnectionString);
                        bSuccessExecuted = MySqlExecute.ExecuteDataSet(sqlText);
                        if (bSuccessExecuted) dataSet = MySqlExecute.dataSet;
                        break;
                    case DbServerType.Oracle:
                        if (OracleExecute == null) OracleExecute = new DbExecute<OracleConnection, OracleCommand, OracleDataAdapter, OracleDataReader>(Config.ConnectionString);
                        bSuccessExecuted = OracleExecute.ExecuteDataSet(sqlText);
                        if (bSuccessExecuted) dataSet = OracleExecute.dataSet;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {

            }
            return bSuccessExecuted;
        }

        public int ExecuteNonQuery(string sqlText)
        {
            if (DBStatus != DBStatusENum.Opening)
                return -1;
            int iRecordsAffected = -1;
            try
            {
                switch (Config.ServerType)
                {
                    case DbServerType.SqlServer:
                        if (SqlExecute == null) SqlExecute = new DbExecute<SqlConnection, SqlCommand, SqlDataAdapter, SqlDataReader>(Config.ConnectionString);
                        iRecordsAffected = SqlExecute.ExecuteNonQuery(sqlText);
                        break;
                    case DbServerType.SqlServerCe:
                        if (SqlCeExecute == null) SqlCeExecute = new DbExecute<SqlCeConnection, SqlCeCommand, SqlCeDataAdapter, SqlCeDataReader>(Config.ConnectionString);
                        iRecordsAffected = SqlCeExecute.ExecuteNonQuery(sqlText);
                        break;
                    case DbServerType.MySql:
                        if (MySqlExecute == null) MySqlExecute = new DbExecute<MySqlConnection, MySqlCommand, MySqlDataAdapter, MySqlDataReader>(Config.ConnectionString);
                        iRecordsAffected = MySqlExecute.ExecuteNonQuery(sqlText);
                        break;
                    case DbServerType.Oracle:
                        if (OracleExecute == null) OracleExecute = new DbExecute<OracleConnection, OracleCommand, OracleDataAdapter, OracleDataReader>(Config.ConnectionString);
                        iRecordsAffected = OracleExecute.ExecuteNonQuery(sqlText);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            finally
            {

            }
            return iRecordsAffected;
        }

    }
}
