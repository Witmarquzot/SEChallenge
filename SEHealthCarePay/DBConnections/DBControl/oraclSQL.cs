using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DBConnections.DBControl
{

    public class OraclSQL :  IDbConnection
    {

        String serverName;
        String userName;
        String encryptedPass;
        String dbName;
        String configLocation;
        System.Data.DataSet dbDataSet;
        Boolean UsingDB = false;

        MySql.Data.MySqlClient.MySqlConnection conn;
        private string myConnectionString;
        public bool Connect(Boolean useDB)
        {
            if (String.IsNullOrWhiteSpace(GetServer()) && String.IsNullOrWhiteSpace(GetUser()) &&
                String.IsNullOrWhiteSpace(GetPassword()) && String.IsNullOrWhiteSpace(GetDatabase()))
            {
                throw new Exception(DBConstants.noInfoError);
            }
            if (String.IsNullOrWhiteSpace(GetServer()))
            {
                throw new Exception(DBConstants.noServerError);
            }
            if (String.IsNullOrWhiteSpace(GetUser()))
            {
                throw new Exception(DBConstants.noUserError);
            }
            if (String.IsNullOrWhiteSpace(GetPassword()))
            {
                throw new Exception(DBConstants.noPasswordError);
            }
            if (String.IsNullOrWhiteSpace(GetDatabase()))
            {
                throw new Exception(DBConstants.noDBerror);
            }

            if (useDB)
            {
                myConnectionString = "server=" + GetServer() + ";"
                + "uid=" + GetUser() + ";"
                + "pwd=" + GetPassword() + ";"
                + "database=" + GetDatabase() + "; Charset = latin1";
                SetUsingDB(true);
            }
            else
            {
                myConnectionString = "server=" + GetServer() + ";"
                + "uid=" + GetUser() + ";"
                + "pwd=" + GetPassword() + ";"
                + " Charset = latin1";
                SetUsingDB(false);
            }
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection
                {
                    ConnectionString = myConnectionString
                };
                conn.Open();
                return true;
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                SetUsingDB(false);
                DisConnect();
                switch (ex.Number)
                {
                    case 0:
                    {
                       throw new Exception(ex.Message);
                    }
                    case 1045:
                    {
                        throw new Exception("Invalid username/password");
                    }
                    default:
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        
        }
        public void DisConnect()
        {
            try
            {
                conn.Close();
            }
            finally
            {
                //we don't really care
            }
        }

        public void SetUsingDB(Boolean value)
        {
            UsingDB = value;
        }
        public Boolean GetUsingDB()
        {
            return UsingDB;
        }
        public string GetDatabase()
        {
            return dbName;
        }

        public string GetPassword()
        {
            return encryptedPass;
        }

        public string GetServer()
        {
            return serverName;
        }


        public string GetUser()
        {
            return userName;
        }


        public void SetConnectionInfo(string server, string user, string password, string database, System.Data.DataSet dataSet)
        {
            serverName = server;
            userName = user;
            encryptedPass = password;
            dbName = database;
            dbDataSet = dataSet;
        }

        public void SetDatabase(string database)
        {
            dbName = database;
        }
        public void SetSchema(System.Data.DataSet dataSet)
        {
            dbDataSet = dataSet;
        }
        public void SetPassword(string password)
        {
            encryptedPass = password;

        }

         public void SetServer(string server)
        {
            serverName = server;

        }

         public void SetUser(string user)
        {
            userName = user;

        }

        public System.Data.DataTable SendQuery(String Query)
        {
            DataTable results = new DataTable();
            if (Connect(true))
            {

                MySql.Data.MySqlClient.MySqlDataAdapter dataAdapter;
                try
                {

                    dataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(Query, conn);
                    dataAdapter.Fill(results);
                    return results;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    DisConnect();
                }
            }
            return results;
        }

        public object SendScalar(String ScalarQuery)
        {
            object rowsUpdated = 0;
            if (Connect(true))
            {
                MySql.Data.MySqlClient.MySqlTransaction transaction;
                transaction = conn.BeginTransaction();
                try
                {
                    MySql.Data.MySqlClient.MySqlCommand sqlCommand = new MySql.Data.MySqlClient.MySqlCommand(ScalarQuery, conn)
                    {
                        Transaction = transaction
                    };
                    rowsUpdated = sqlCommand.ExecuteScalar();
                    transaction.Commit();
                    return rowsUpdated;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
                finally
                {
                    DisConnect();
                }
            }
            return rowsUpdated;
        }
        public Boolean SendNonQuery(String NoNQuery)
        {
            if (Connect(true))
            {
                MySql.Data.MySqlClient.MySqlTransaction transaction;
                transaction = conn.BeginTransaction();
                try
                {
                    MySql.Data.MySqlClient.MySqlCommand sqlCommand = new MySql.Data.MySqlClient.MySqlCommand(NoNQuery, conn)
                    {
                        Transaction = transaction
                    };
                    sqlCommand.ExecuteNonQuery();
                    transaction.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw new Exception(e.Message);
                }
                finally
                {
                    DisConnect();
                }
            }
            return false;
        }
        public Boolean CreateDataBase()
        {
            if (Connect(false))
            {
                String command = "CREATE SCHEMA `" + GetDatabase() + "`;";
                try
                {
                    MySql.Data.MySqlClient.MySqlCommand sqlCommand = new MySql.Data.MySqlClient.MySqlCommand(command, conn);
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    DisConnect();
                }
                return true;
            }
            return false;
        }
        public Boolean DeleteDataBase()
        {
            if (Connect(false))
            {
                String command = "DROP SCHEMA `" + GetDatabase() + "`;";
                try
                {
                    MySql.Data.MySqlClient.MySqlCommand sqlCommand = new MySql.Data.MySqlClient.MySqlCommand(command, conn);
                    conn.CreateCommand();
                    sqlCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    DisConnect();
                }
                return true;
            }
            return false;
        }
        public Boolean CheckForDataBase()
        {
            if (Connect(false))
            {
                String command = "SELECT SCHEMA_NAME FROM INFORMATION_SCHEMA.SCHEMATA  WHERE SCHEMA_NAME like '" + GetDatabase() + "';";
                DataTable results = new DataTable();
                MySql.Data.MySqlClient.MySqlDataAdapter dataAdapter;
                try
                {

                    dataAdapter = new MySql.Data.MySqlClient.MySqlDataAdapter(command, conn);
                    dataAdapter.Fill(results);
                    return results.Rows.Count > 0;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    DisConnect();
                }
            }
            return false;

        }
        public Boolean CheckAndCorrectSchema()
        {
            SortedList<String, DataTable> tableList = new SortedList<String, DataTable>();
            foreach (DataTable t in dbDataSet.Tables)
            {
                tableList.Add(t.DisplayExpression, t);
            }
            foreach (KeyValuePair<String, DataTable> table in tableList)
            {
                DataTable t = table.Value;
                if (CheckForTable(t.TableName))
                {
                    CheckAndFixTable(t);
                }
                else
                {
                    CreateTable(t);
                    CheckTableConstraints(t);
                }
            }
            return true;
        }

        public Boolean CheckForTable(String tableName)
        {
            String command = "select TABLE_NAME from INFORMATION_SCHEMA.tables where TABLE_NAME LIKE '" 
                    + tableName + "' AND table_schema = '" + GetDatabase() + "';";
            return SendQuery(command).Rows.Count > 0;
        }

        public Boolean DeleteTable(String tableName)
        {
            String command = "DROP TABLE `" + tableName + "`;";
            return SendNonQuery(command);
        }

        public Boolean CheckAndFixTable(System.Data.DataTable table)
        {
            String command = "select * from " + GetDatabase() + "." + table.TableName + " LIMIT 1;";
            DataTable res = SendQuery(command);
            List<DataColumn> nColumns = new List<DataColumn>();
            foreach (DataColumn dc in table.Columns)
            {
                if (!res.Columns.Contains(dc.ColumnName))
                {
                    nColumns.Add(dc);
                }
            }
            if (nColumns.Count > 0)
            {
                foreach (DataColumn d in nColumns)
                {
                    command = "ALTER TABLE  `" + GetDatabase() + "`.`" + table.TableName + "` ADD ";
                    command += GetColumnCommand(d) + ";";
                    SendNonQuery(command);
                }
                return true;
            }
            else
            {
                return true;
            }
        }

        public Boolean CreateTable(System.Data.DataTable table)
        {
            if (table.Columns.Count > 0)
            {
                String command = "CREATE TABLE `" + GetDatabase() + "`.`" + table.TableName + "`(";
                String append ="";
                foreach (DataColumn dc in table.Columns)
                {
                    command += GetColumnCommand(dc);
                    if(dc.Unique)
                    {
                        append += ", UNIQUE INDEX `" + dc.ColumnName + "_UNIQUE` (`" + dc.ColumnName + "` ASC) VISIBLE"; 
                    }
                    if (table.Columns.IndexOf(dc).Equals(table.Columns.Count - 1))
                    {
                        command += append + " );";
                    }
                    else
                    {
                        command += ", ";
                    }

                }
                return SendNonQuery(command);
            }
            return true;
        }

        public Boolean CheckTableConstraints(System.Data.DataTable table)
        {
            String command;
            if (table.PrimaryKey.Length > 0)
            {
                for (int PK = 0; PK < table.PrimaryKey.Length; PK++)
                {
                    command = "Alter Table `" + GetDatabase() + "`.`" + table.TableName + "` ADD PRIMARY KEY ( `" + table.PrimaryKey[PK].ColumnName + "`);";
                    SendNonQuery(command);
                }
            }
            if (table.ParentRelations.Count > 0)
            {
                foreach (DataRelation relation in table.ParentRelations)
                {
                    for (int c = 0; c < relation.ChildColumns.Length; c++)
                    {
                        command = "Alter Table `" + GetDatabase() + "`.`" + relation.ChildTable.TableName + "` ADD CONSTRAINT FOREIGN KEY (`" + relation.ChildColumns[c].ColumnName + "`)";
                        command += " REFERENCES `" + GetDatabase() + "`.`" + relation.ParentTable.TableName + "` (`" + relation.ParentColumns[c].ColumnName + "`) ON DELETE CASCADE ;";
                        SendNonQuery(command);
                    }
                }

            }
            return true;

        }

        private String GetColumnCommand(DataColumn dc)
        {
            String command = "";
            command += "`" + dc.ColumnName + "` ";
            if (dc.DataType.Equals(System.Type.GetType("System.String")))
            {
                command += "NVARCHAR(" + dc.MaxLength + ")";
            }
            else if (dc.DataType.Equals(System.Type.GetType("System.Int32")))
            {
                command += "INT";
            }

            if (dc.AllowDBNull)
            {
                command += " NULL";
            }
            else
            {
                command += " NOT NULL ";
            }
            if (dc.AutoIncrement)
            {
                command += " AUTO_INCREMENT ";
            }
            return command;
        }
        public DataTable UpsertTable(DataTable table)
        {
            foreach (DataRow dataRow in table.Rows)
            {
                if (dataRow.RowState.Equals(DataRowState.Deleted))
                {
                    continue;
                }
                else if ((dataRow.RowState.Equals(DataRowState.Modified) && ((int)dataRow["id"]) > 0))
                {
                    string comand = "Update t set ";
                    foreach (DataColumn dc in table.Columns)
                    {
                        if (!dc.ColumnName.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                        {
                            if (dataRow[dc].Equals(DBNull.Value))
                            {
                                comand += " t." + dc.ColumnName + " = NULL ,";
                            }
                            else
                            {
                                comand += " t." + dc.ColumnName + " = " + (dc.DataType.Equals(System.Type.GetType("System.String")) ? "'" + ReplaceInvalid(((string)dataRow[dc])) + "'" : dataRow[dc]) + " ,";
                            }

                        }
                    }
                    if (comand.EndsWith(","))
                    {
                        comand = comand.Substring(0, comand.Length - 1);
                    }
                    comand += "from  `" + GetDatabase() + "`.`" + table.TableName + "` as t where t.id  = " + dataRow["id"] + ";";
                    SendQuery(comand);

                }
                else if ((dataRow.RowState.Equals(DataRowState.Added)) || ((int)dataRow["id"]) < 1)
                {
                    string comand = "insert  `" + GetDatabase() + "`.`" + table.TableName + "` (";
                    string select = " select ";
                    foreach (DataColumn dc in table.Columns)
                    {
                        if (!dc.ColumnName.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                        {
                            comand += dc.ColumnName + " ,";
                            if (dataRow[dc].Equals(DBNull.Value))
                            {
                                select += " NULL ,";
                            }
                            else
                            {
                                select += (dc.DataType.Equals(System.Type.GetType("System.String")) ? ("'" + ReplaceInvalid((string)dataRow[dc]) + "'") : dataRow[dc]) + " ,";
                            }
                        }
                    }
                    if (comand.EndsWith(","))
                    {
                        comand = comand.Substring(0, comand.Length - 1);
                    }
                    if (select.EndsWith(","))
                    {
                        select = select.Substring(0, select.Length - 1);
                    }
                    comand += ") " + select;
                    SendQuery(comand);
                }

            }
            return FillTable(table);
        }

        public DataTable FillTable(DataTable table)
        {
            string tblfill = " select ";
            if (table.Columns.Count > 1)
            {
                foreach (DataColumn dc in table.Columns)
                {
                    tblfill += dc.ColumnName + " ,";
                }
                if (tblfill.EndsWith(","))
                {
                    tblfill = tblfill.Substring(0, tblfill.Length - 1);
                }
            }
            else
            {

                tblfill += " * ";
            }
            tblfill += " from  `" + GetDatabase() + "`.`" + table.TableName + "` ";
            return SendQuery(tblfill);
        }

        public DataSet UpsertDataSet(DataSet DS)
        {
            DataSet ret = new DataSet();
            foreach (System.Data.DataTable t in DS.Tables)
            {
                DataTable nt = UpsertTable(t);
                if (t.ChildRelations.Count > 0)
                {
                    DataTable ut = DS.Tables[t.TableName];
                    for (int r = 0; r < ut.Rows.Count; r++)
                    {
                        ut.Rows[r]["id"] = nt.Rows[r]["id"];

                    }
                }
                ret.Tables.Add(nt);
            }
            return ret;
        }

        public DataSet FillDataSet(DataSet DS)
        {
            foreach (System.Data.DataTable t in DS.Tables)
            {
                System.Data.DataTable nt = FillTable(t);
                DS.Tables.Remove(t);
                DS.Tables.Add(nt);
            }

            return DS;
        }


        private string ReplaceInvalid(string dirty)
        {
            string ret = dirty.Replace("'", "\\\'");
            ret = ret.Replace("\"", "\\\"");
            ret = ret.Replace("`", "\\`");
            return ret;
        }
        public DataSet GetSchema()
        {
            return dbDataSet;
        }

        public void SetConfigurationLocation(String location)
        {
            configLocation = location;
        }
        public String GetConfigurationLocation()
        {
            return configLocation;
        }

        public DataTable CallStoredProc(string proc)
        {
            string tblfill = "call `" + GetDatabase() + "`.`" + proc + "`;";
            DataTable ret = SendQuery(tblfill);
            ret.TableName = proc;
            return ret;
        }


        public Boolean CheckForStoredProc(string procname)
        {
            string command = "select SPECIFIC_NAME from INFORMATION_SCHEMA.ROUTINES where SPECIFIC_NAME LIKE '" + procname + "' and ROUTINE_TYPE = 'PROCEDURE' and ROUTINE_SCHEMA like '" + GetDatabase() +  "';";
            return SendQuery(command).Rows.Count > 0;
        }

        public Boolean CheckAndFixStoredProc(string procname, string procStatment)
        {

            if (CheckForStoredProc(procname))
            {
                string cmd = "ALTER PROCEDURE " + procStatment;
                SendQuery(cmd);
            }
            else
            {
                string cmd = "CREATE PROCEDURE " + procStatment;
                SendQuery(cmd);
            }
            return CheckForStoredProc(procname);
        }
        public DataTable RemoveRow(DataRow row)
        {
            string comand = "Delete t from `" + GetDatabase() + "`.`" + row.Table.TableName + "` as t where t.id  = " + row["id"] + ";";
            SendNonQuery(comand);
            DataTable dataTable = new DataTable(row.Table.TableName);
            return FillTable(dataTable);
        }
    }
}
