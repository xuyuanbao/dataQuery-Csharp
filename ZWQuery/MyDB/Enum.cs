
namespace MyDB
{
    /// <summary>
    /// 描述数据库的类型
    /// </summary>
    public enum DbServerType { SqlServer, SqlServerCe, MySql, Oracle };
    public enum DBStatusENum { NotInitialed, Initialized, Opening, Closed, WithError};
}