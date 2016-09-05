using System;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MyDB
{
    public class MySql : IDbExecute
    {
        public MySqlConnection dbConnection;
        public MySqlCommand dbCommand;
        public MySqlDataAdapter dbDataAdapter;
        public MySqlDataReader dbDataReader;
        public DataSet dataSet;

        public MySql()
        {
            dbConnection = new MySqlConnection();
            dbCommand = new MySqlCommand();
            dbCommand.Connection = dbConnection;
            dbDataAdapter = new MySqlDataAdapter();
        }

        public MySql(string connectionString)
        {
            dbConnection = new MySqlConnection(connectionString);
            dbCommand = new MySqlCommand();
            dbCommand.Connection = dbConnection;
            dbDataAdapter = new MySqlDataAdapter();
        }

        ~MySql()
        {
            if (dataSet != null)
            {
                dataSet.Reset();
            }
            if (dbDataReader != null)
            {
                if (dbDataReader.IsClosed == false)
                {
                    dbDataReader.Close();
                }
            }
            //if (dbConnection.State != ConnectionState.Closed)
            //{
            //    dbConnection.Close();
            //}
        }

        public bool ExecuteReader(string sqlText)
        {
            bool bSuccessExecuted = false;
            try
            {
                if (dbDataReader != null)
                {
                    if (dbDataReader.IsClosed == false)
                    {
                        dbDataReader.Close();
                    }
                }
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                dbCommand.CommandText = sqlText;
                dbCommand.CommandType = CommandType.Text;
                dbDataReader = dbCommand.ExecuteReader();
                bSuccessExecuted = true;
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
            bool bSuccessExecuted = false;
            try
            {
                if (dbDataReader != null)
                {
                    if (dbDataReader.IsClosed == false)
                    {
                        dbDataReader.Close();
                    }
                }
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                if (dataSet == null)
                {
                    dataSet = new DataSet();
                }
                else 
                {
                    dataSet.Reset();
                }
                dbCommand.CommandText = sqlText;
                dbCommand.CommandType = CommandType.Text;
                dbDataAdapter.SelectCommand = dbCommand;
                dbDataAdapter.Fill(dataSet);
                bSuccessExecuted = true;
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
            int iRecordsAffected = -1;
            try
            {
                if (dbDataReader != null)
                {
                    if (dbDataReader.IsClosed == false)
                    {
                        dbDataReader.Close();
                    }
                }
                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }
                dbCommand.CommandText = sqlText;
                dbCommand.CommandType = CommandType.Text;
                iRecordsAffected = dbCommand.ExecuteNonQuery();
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
