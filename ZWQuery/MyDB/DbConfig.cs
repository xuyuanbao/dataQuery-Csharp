using System;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Data.OracleClient ;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MyDB
{
    public class DbConfig
    {
        private DbServerType _ServerType = DbServerType.SqlServer;
        private string _Server = "";
        private string _Port = "";
        private bool _WindowsAuthentication = false;
        private string _UserId = "";
        private string _Password = "";
        private string _Database = "";
        private string _ConnectionString = "";

        private const string DB_CONFIG_FILE = "MyDB.xml";
        private const string MES_KEY = "[ZHUWEI]";
        private const string MES_IV = "[zhuwei]";

        public DbConfig()
        {

        }

        public DbServerType ServerType
        {
            get
            {
                return _ServerType;
            }
            set
            {
                _ServerType = value;
            }
        }

        public string Server
        {
            get
            {
                return _Server;
            }
            set
            {
                if (value != null)
                {
                    _Server = value.Trim();
                }
                else
                {
                    throw (new ArgumentNullException("Server"));
                }
            }
        }

        public string Port
        {
            get
            {
                return _Port;
            }
            set
            {
                if (value != null)
                {
                    _Port = value.Trim();
                }
                else
                {
                    throw (new ArgumentException("Port"));
                }
            }
        }

        public bool WindowsAuthentication
        {
            get
            {
                return _WindowsAuthentication;
            }
            set
            {
                _WindowsAuthentication = value;
            }
        }

        public string UserId
        {
            get
            {
                return _UserId;
            }
            set
            {
                if (value != null)
                {
                    _UserId = value.Trim();
                }
                else
                {
                    throw (new ArgumentNullException("UserId"));
                }
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {

                if (value != null)
                {
                    _Password = value;
                }
                else
                {
                    throw (new ArgumentNullException("Password"));
                }
            }
        }

        public string Database
        {
            get
            {
                return _Database;
            }
            set
            {
                if (value != null)
                {
                    _Database = value.Trim();
                }
                else
                {
                    throw (new ArgumentNullException("Database"));
                }
            }
        }

        public string ConnectionString
        {
            get
            {
                switch (ServerType)
                {
                    case DbServerType.SqlServer:
                        {
                            _ConnectionString = " Data Source=" + Server + "; Database=" + Database +
                                "; Integrated Security=" + WindowsAuthentication.ToString() +
                                "; User ID=" + UserId + ";Password=" + Password +
                                "; Persist Security Info=false;";
                            break;
                        }
                    case DbServerType.SqlServerCe:
                        {
                            _ConnectionString = " Data Source=" + Server +
                                "; Password=" + Password +
                                "; Persist Security Info=false;";
                            break;
                        }
                    case DbServerType.MySql:
                        {
                            _ConnectionString = " Data Source=" + Server + "; Database=" + Database +
                                "; User ID=" + UserId + ";Password=" + Password +
                                "; Persist Security Info=false;charset=utf8;default command timeout=30";
                            break;
                        }
                    case DbServerType.Oracle:
                        {
                            _ConnectionString = "";
                            break;
                        }
                    default:
                        {
                            _ConnectionString = "";
                            break;
                        }
                }
                return _ConnectionString;
            }
        }

        /// <summary>
        /// 验证数据库连接
        /// </summary>
        /// <returns></returns>
        public bool VerifyConnection()
        {
            bool bSuccessConnected = false;

            switch (ServerType)
            {
                case DbServerType.SqlServer:
                    {
                        try
                        {
                            using (SqlConnection con = new SqlConnection(ConnectionString))
                            {
                                con.Open();
                                con.Close();
                                bSuccessConnected = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw (new Exception(ex.Message));
                        }
                        break;
                    }
                case DbServerType.SqlServerCe:
                    {
                        try
                        {
                            using (SqlCeConnection con = new SqlCeConnection(ConnectionString))
                            {
                                con.Open();
                                con.Close();
                                bSuccessConnected = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw (new Exception(ex.Message));
                        }
                        break;
                    }
                case DbServerType.MySql:
                    {
                        try
                        {
                            using (MySqlConnection con = new MySqlConnection(ConnectionString))
                            {
                                con.Open();
                                con.Close();
                                bSuccessConnected = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw (new Exception(ex.Message));
                        }
                        break;
                    }
                case DbServerType.Oracle:
                    {
                        _ConnectionString = "";
                        break;
                    }
                default:
                    {
                        _ConnectionString = "";
                        break;
                    }
            }

            return bSuccessConnected;
        }

        /// <summary>
        /// 保存连接参数至配置文件MyDB.xml
        /// </summary>
        /// <returns></returns>
        public bool SaveSettings()
        {
            bool bSuccessSaved = false;
            try
            {
                string DbConfigFile = Directory.GetCurrentDirectory() + "\\" + DB_CONFIG_FILE;

                MyXml.XmlWrite(DbConfigFile, "DbConfig", "ServerType", ServerType.ToString());
                MyXml.XmlWrite(DbConfigFile, "DbConfig", "Server", Server);
                MyXml.XmlWrite(DbConfigFile, "DbConfig", "Port", Port);
                MyXml.XmlWrite(DbConfigFile, "DbConfig", "WindowsAuthentication", WindowsAuthentication.ToString());
                MyXml.XmlWrite(DbConfigFile, "DbConfig", "UserId", UserId);
                MyXml.XmlWrite(DbConfigFile, "DbConfig", "Password", DES.Encrypt(Password, MES_KEY, MES_IV));  //加密
                MyXml.XmlWrite(DbConfigFile, "DbConfig", "Database", Database);
                bSuccessSaved = true;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            return bSuccessSaved;
        }

        /// <summary>
        /// 从配置文件MyDB.xml加载连接参数
        /// </summary>
        /// <returns></returns>
        public bool LoadSettings()
        {
            bool bSuccessLoaded = false;
            try
            {
                string DbConfigFile = Directory.GetCurrentDirectory() + "\\" + DB_CONFIG_FILE;
                try
                {
                    ServerType = (DbServerType)Enum.Parse(typeof(DbServerType), MyXml.XmlRead(DbConfigFile, "DbConfig", "ServerType", DbServerType.SqlServer.ToString()), true);
                }
                catch
                {
                    ServerType = DbServerType.SqlServer;
                }
                Server = MyXml.XmlRead(DbConfigFile, "DbConfig", "Server", "localhost\\sqlexpress");
                Port = MyXml.XmlRead(DbConfigFile, "DbConfig", "Port", (ServerType == DbServerType.MySql) ? "3306" : "");
                WindowsAuthentication = bool.Parse(MyXml.XmlRead(DbConfigFile, "DbConfig", "WindowsAuthentication", false.ToString()));
                UserId = MyXml.XmlRead(DbConfigFile, "DbConfig", "UserId", "");
                Password = DES.Decrypt(MyXml.XmlRead(DbConfigFile, "DbConfig", "Password", ""), MES_KEY, MES_IV);  //解密
                Database = MyXml.XmlRead(DbConfigFile, "DbConfig", "Database", "");
                bSuccessLoaded = true;
            }
            catch (Exception ex)
            {
                throw (new Exception(ex.Message));
            }
            return bSuccessLoaded;
        }

    }
}
