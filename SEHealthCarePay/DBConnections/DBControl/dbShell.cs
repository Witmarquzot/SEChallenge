using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace DBConnections
{
    public class DBShell
    {
        /// <summary>
        ///  Makes a new DB Shell for connection to a Database
        /// </summary>
        /// <param name="XMLPath">Path to the configuration for the Database Connection</param>
        public DBShell(String XMLPath)
        {
            if (XMLPath.Length > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(XMLPath);
                String sqlType = doc.GetElementsByTagName("SQLTYPE").Item(0).InnerText;
                if (sqlType.Equals("LOCALMICROSOFT"))
                {
                    SetConnectionTypeLocalMicro();
                }
                else if (sqlType.Equals("LOCALORACLE"))
                {
                    SetConnectionTypeLocalOracle();
                }
                if(!conn.Equals(null) )
                {
                    conn.SetUser(doc.GetElementsByTagName("USER").Item(0).InnerText);
                    conn.SetPassword(doc.GetElementsByTagName("PASSWORD").Item(0).InnerText);
                    conn.SetServer(doc.GetElementsByTagName("SERVER").Item(0).InnerText);
                    conn.SetDatabase(doc.GetElementsByTagName("DATABASE").Item(0).InnerText);
                }
            }   
            
        }
        /// <summary>
        ///    DB Connection Interface the allows us to connec to multiple different types with one "View"
        /// </summary>
        IDbConnection conn = null;

        /// <summary>
        ///     Connects to a local Microsoft SQL instance and handles all Create / Delete / Update Funtions
        /// </summary>
        virtual public void SetConnectionTypeLocalMicro()
        {
            conn = new MicoSFTSql();
        }

        /// <summary>
        ///     Connects to a local MySql instance(Which owned by oracle) and handles all Create / Delete / Update Funtions
        /// </summary>
        virtual public void SetConnectionTypeLocalOracle()
        {
            conn = new OraclSQL();
        }

        /// <summary>
        ///     Connects to a future local NOSQL instance and handles all Create / Delete / Update Funtions
        /// </summary>
        virtual public void SetConnectionTypeLocalNOSQL()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///     Connects to a future cloud Microsoft SQL instance and handles all Create / Delete / Update Funtions
        /// </summary>
        virtual public void SetConnectionTypeCloudMicro()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///     Connects to a future cloud Amazon SQL instance and handles all Create / Delete / Update Funtions
        /// </summary>
        virtual public void SetConnectionTypeCloudAma()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Connects to a future cloud NoSQL instance and handles all Create / Delete / Update Funtions
        /// </summary>
        virtual public void SetConnectionTypeCloudNoSql()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Connects to defined Server, should not need as functions connect and disconnect
        /// </summary>
        /// <param name="useDB">Is this a DB Function or Server Function</param>
        /// <returns>True for success or Exception for failure</returns>
        virtual public bool Connect(Boolean useDB)
        {
                
            return conn.Connect(useDB);
        }
        /// <summary>
        ///   Sets flag to connecto to DB or Server
        /// </summary>
        /// <param name="value"></param>
        virtual public void SetUsingDB(Boolean value)
        {
            conn.SetUsingDB(value);
        }
        /// <summary>
        /// Are we connecting to the DB
        /// </summary>
        /// <returns>True if conencted to DB , false if connected to server</returns>
        virtual public Boolean GetUsingDB()
        {
            return conn.GetUsingDB(); ;
        }

        /// <summary>
        ///  Manually disconnects the server, should not need as functions connect and disconnect
        /// </summary>
        virtual public void DisConnect()
        {
             conn.DisConnect();
        }

        /// <summary>
        ///  Which DB are we trying to connect to, needed for IDBConn
        /// </summary>
        /// <returns>Database connecting to if defined</returns>
        virtual public string GetDatabase()
        {
            return conn.GetDatabase();
        }

        /// <summary>
        ///  Password we are using to connect, needed for IDBConn
        /// </summary>
        /// <returns>Password if defined</returns>
        virtual public string GetPassword()
        {
            return conn.GetPassword();
        }

        /// <summary>
        /// Server we are using to connect to, needed for IDBConn
        /// </summary>
        /// <returns>Server if defined</returns>
        virtual public string GetServer()
        {
            return conn.GetServer();
        }

        /// <summary>
        /// User we are connecting as, needed for IDBConn
        /// </summary>
        /// <returns>User if defined</returns>
        virtual public string GetUser()
        {
            return conn.GetUser();
        }


        /// <summary>
        ///     Sets all info for connection
        /// </summary>
        /// <param name="server"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="database"></param>
        /// <param name="dataSet"></param>
        virtual public void SetConnectionInfo(string server, string user, string password, string database, System.Data.DataSet dataSet)
        {
            conn.SetConnectionInfo(server, user, password, database, dataSet);
        }
        /// <summary>
        /// Schema of the Dataset we need to meet minummum
        /// </summary>
        /// <param name="dataSet"></param>
        virtual public void SetSchema(System.Data.DataSet dataSet)
        {
            conn.SetSchema(dataSet);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        virtual public void SetDatabase(string database)
        {
            conn.SetDatabase(database);

        }
        virtual public void SetPassword(string password)
        {
            conn.SetPassword(password);

        }

        virtual public void SetServer(string server)
        {
            conn.SetServer(server);
        }

        virtual public void SetUser(string user)
        {
            conn.SetUser(user);
   
        }
        virtual public System.Data.DataTable SendQuery(String Query)
        {
           return conn.SendQuery(Query);
        }
        virtual public Boolean SendNonQuery(String NoNQuery)
        {
            return conn.SendNonQuery(NoNQuery);
        }
        virtual public Boolean CheckForDataBase()
        {
            return conn.CheckForDataBase();
        }
        virtual public Boolean CreateDataBase()
        {
            return conn.CreateDataBase();
        }
        virtual public Boolean DeleteDataBase()
        {
            return conn.DeleteDataBase();
        }

        virtual public Boolean CheckAndCorrectSchema()
        {
            return conn.CheckAndCorrectSchema();
        }

        virtual public Boolean CheckForTable(String tableName)
        {
            return conn.CheckForTable(tableName);
        }
        virtual public Boolean DeleteTable(String tableName)
        {
            return conn.DeleteTable(tableName);
        }

        virtual public Boolean CreateTable(System.Data.DataTable table)
        {
            return conn.CreateTable(table);
        }
        virtual public Boolean CheckAndFixTable(System.Data.DataTable table)
        {
            return conn.CheckAndFixTable(table);
        }
        virtual public System.Data.DataTable UpsertTable(System.Data.DataTable table)
        {
            return conn.UpsertTable(table);
        }

        virtual public System.Data.DataTable FillTable(System.Data.DataTable table)
        {
            return conn.FillTable(table);
        }

        virtual public System.Data.DataSet UpsertDataSet(System.Data.DataSet DS)
        {
            return conn.UpsertDataSet(DS);
        }

        virtual public System.Data.DataSet FillDataSet(System.Data.DataSet DS)
        {
            return conn.FillDataSet(DS);
        }

    }
}
