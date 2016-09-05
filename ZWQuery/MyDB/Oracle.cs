using System;
using System.Data;
using System.Data.OracleClient;

namespace MyDB
{
    class Oracle : IDbExecute
    {
        public OracleConnection dbConnection;
        public OracleCommand dbCommand;
        public OracleDataAdapter dbDataAdapter;
        public OracleDataReader dbDataReader;
        public DataSet dataSet;

        public Oracle()
        {
            dbConnection = new OracleConnection();
            dbCommand = new OracleCommand();
            dbCommand.Connection = dbConnection;
            dbDataAdapter = new OracleDataAdapter();
        }

        public Oracle(string connectionString)
        {
            dbConnection = new OracleConnection(connectionString);
            dbCommand = new OracleCommand();
            dbCommand.Connection = dbConnection;
            dbDataAdapter = new OracleDataAdapter();
        }

        ~Oracle()
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
