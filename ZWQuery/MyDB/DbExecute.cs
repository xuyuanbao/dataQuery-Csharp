using System;
using System.Data;
using System.Data.Common ;

namespace MyDB
{
    public class DbExecute<TDbConnection, TDbCommand, TDbDataAdapter,TDbDataReader> : IDbExecute
        where TDbConnection : DbConnection, new()
        where TDbCommand : DbCommand, new()
        where TDbDataAdapter : DbDataAdapter, new()
        where TDbDataReader : DbDataReader
    {
        public TDbConnection dbConnection;
        public TDbCommand dbCommand;
        public TDbDataAdapter dbDataAdapter;
        public TDbDataReader dbDataReader;
        public DataSet dataSet;
        
        public DbExecute()
        {
            dbConnection = new TDbConnection();
            dbCommand = new TDbCommand();
            dbCommand.Connection = dbConnection;
            dbDataAdapter = new TDbDataAdapter();
        }

        public DbExecute(string connectionString)
        {
            dbConnection = new TDbConnection();
            dbConnection.ConnectionString = connectionString;
            dbCommand = new TDbCommand();
            dbCommand.Connection = dbConnection;
            dbDataAdapter = new TDbDataAdapter();
        }

        ~DbExecute()
        {
            if (dataSet != null)
            {
                dataSet.Reset();
            }
            if (dbDataReader != null)
            {
                if (dbDataReader.IsClosed == false)
                {
                    //if(dbDataReader!=null) 
                        //dbDataReader.Close();
                }
            }
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
                dbDataReader = (TDbDataReader)dbCommand.ExecuteReader();
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
