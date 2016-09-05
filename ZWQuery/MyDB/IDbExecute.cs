
namespace MyDB
{
    public interface IDbExecute
    {
        /// <summary>
        /// 对数据库连接执行select语句并生成一个对应数据库类型的DbDataReader,返回是否执行成功
        /// </summary>
        /// <param name="sqlText">select语句</param>
        /// <returns>是否执行成功</returns>
        bool ExecuteReader(string sqlText);

        /// <summary>
        /// 对数据库连接执行select语句并生成一个DataSet,返回是否执行成功
        /// </summary>
        /// <param name="sqlText">select语句</param>
        /// <returns>是否执行成功</returns>
        bool ExecuteDataSet(string sqlText);

        /// <summary>
        /// 对数据库连接执行insert,delete,update语句,返回受影响的行数
        /// </summary>
        /// <param name="sqlText">insert,delete,update语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(string sqlText); 
    }
}
