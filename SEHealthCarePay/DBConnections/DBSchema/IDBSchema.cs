using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnections.DBSchema
{
    public interface IDBSchema
    {

       DataSet GetSchema();

       DataSet GetSampleData();

        string[] getStoreProcs();
        string getStoredProcCmd(string procName);

    }
}
