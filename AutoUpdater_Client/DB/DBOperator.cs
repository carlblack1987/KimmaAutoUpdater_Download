using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;

namespace AutoUpdater_Client.DB
{
    class DBOperator
    {
        //数据库连接
        SQLiteConnection m_dbConnection;
        //要打开或者新建的sqlite数据库文件名
        string dbFileName = string.Empty;

        public DBOperator(string filename)
        {
            this.dbFileName = filename;
        }

        //创建一个空的数据库
        void createNewDatabase()
        {
            try
            {
                FileInfo file = new FileInfo(dbFileName);
                if(!file.Exists)
                    SQLiteConnection.CreateFile(dbFileName);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //创建一个连接到指定数据库
        void openConnection()
        {
            try
            {
                if (m_dbConnection == null)
                {
                    m_dbConnection = new SQLiteConnection("Data Source=" + dbFileName + ";Version=3;");
                    m_dbConnection.Open();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //关闭连接
        void closeConnection()
        {
            try
            {
                if (m_dbConnection != null)
                    m_dbConnection.Close();
                m_dbConnection = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //执行指定SQL
        public void executeSql(string sql)
        {
            try
            {
                openConnection();
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                closeConnection();
            }
        }

        //查询特定字段结果
        //参数说明：sql为需要执行的sql脚本，col为要查询的字段名
        public string getSqlResult(string sql, string col)
        {
            string result = string.Empty;
            try
            {
                openConnection();
                SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    result = reader[col].ToString();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                closeConnection();
            }
            return result;
        }

        //执行初试化数据库操作，该操作一般只有第一次部署客户端的时候需要执行
        public void initiateClientDB()
        {
            try
            {
                createNewDatabase();
                //版本信息表
                executeSql("DROP TABLE IF EXISTS T_VERSION_INFO");
                executeSql("CREATE TABLE T_VERSION_INFO (ID INT PRIMARY KEY, VERSION VARCHAR(20) NOT NULL, PREVIOUSVER VARCHAR(20), "
                + "VERSIONSEQ INT NOT NULL, UPDATETIME DATETIME, STATUS VARCHAR(20), ISINUSE INT)");
                executeSql("INSERT INTO T_VERSION_INFO VALUES(1, 'V1.0.0', '', 100, DATETIME('NOW'), 'SUCCESS', 1)");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
