using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnections
{
    public interface IDBShell
    {
        void SetConnectionTypeLocalMicro();
        void SetConnectionTypeLocalOracle();
        void SetConnectionTypeLocalNOSQL();
        void SetConnectionTypeCloudMicro();
        void SetConnectionTypeCloudAma();
        void SetConnectionTypeCloudNoSql();
        System.Data.DataTable UpsertTable(System.Data.DataTable table);
        System.Data.DataTable RemoveRow(System.Data.DataRow row);
        System.Data.DataTable FillTable(System.Data.DataTable table);
        System.Data.DataSet UpsertDataSet(System.Data.DataSet DS);
        System.Data.DataSet FillDataSet(System.Data.DataSet DS);
        System.Data.DataTable CallStoredProc(String proc);
        void SetConfigurationLocation(String location);
        String GetConfigurationLocation();
    }
}
