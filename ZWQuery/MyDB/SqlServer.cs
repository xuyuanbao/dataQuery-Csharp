using System;
using System.Data;
using System.Data.SqlClient;

namespace MyDB
{
    public class SqlServer : IDbExecute
    {
        public SqlConnection dbConnection;
        public SqlCommand dbCommand;
        public SqlDataAdapter dbDataAdapter;
        public SqlDataReader dbDataReader;
        public DataSet dataSet;

        public SqlServer()
        {
            dbConnection = new SqlConnection();
            dbCommand = new SqlCommand();
            dbCommand.Connection = dbConnection;
            dbDataAdapter = new SqlDataAdapter();
        }

        public SqlServer(string connectionString)
        {
            dbConnection = new SqlConnection(connectionString);
            dbCommand = new SqlCommand();
            dbCommand.Connection = dbConnection;
            dbDataAdapter = new SqlDataAdapter();
        }

        ~SqlServer()
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
