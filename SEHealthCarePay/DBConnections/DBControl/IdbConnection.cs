using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBConnections.DBControl
{
    public interface IDbConnection
    {

        void SetConfigurationLocation(string location);
        string GetConfigurationLocation();
        void SetServer(string server);
        void SetUser(string user);
        void SetPassword(string password);

        void SetDatabase(string database);

        void SetSchema(System.Data.DataSet dataSet);
        void SetConnectionInfo(string server, string user, string password, string database, System.Data.DataSet dataSet);
        System.Data.DataSet GetSchema();
        Boolean GetUsingDB();
        void SetUsingDB(Boolean value);
        Boolean Connect(Boolean allowSet);
        void DisConnect();
        string GetServer();
        string GetUser();
        string GetPassword();

        string GetDatabase();
 
        System.Data.DataTable SendQuery(string Query);
        Boolean SendNonQuery(string NoNQuery);
        Boolean CheckForDataBase();
        Boolean CreateDataBase();
        Boolean DeleteDataBase();

        Boolean CheckAndCorrectSchema();

        Boolean CheckForTable(string tableName);
        Boolean DeleteTable(string tableName);

        Boolean CreateTable(System.Data.DataTable table);
        Boolean CheckAndFixTable(System.Data.DataTable table);

        System.Data.DataTable UpsertTable(System.Data.DataTable table);
        System.Data.DataTable FillTable(System.Data.DataTable table);
        System.Data.DataSet UpsertDataSet(System.Data.DataSet DS);
        System.Data.DataSet FillDataSet(System.Data.DataSet DS);

        System.Data.DataTable RemoveRow(System.Data.DataRow row);
        System.Data.DataTable CallStoredProc(string proc);
        Boolean CheckForStoredProc(string procname);
        Boolean CheckAndFixStoredProc(string procname, string procStatment);
    }
}
