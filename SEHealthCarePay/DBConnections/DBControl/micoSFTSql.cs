using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DBConnections.DBControl
{

    public class MicoSFTSql : IDbConnection
    {
        string serverName;
        string userName;
        string encryptedPass;
        string dbName;
        string configLocation;
        System.Data.DataSet dbDataSet;

        Boolean UsingDB = false;

        SqlConnection conn;

        private string mSConnectionString;
        public bool Connect(Boolean useDB)
        {
            if (string.IsNullOrWhiteSpace(GetServer()) && string.IsNullOrWhiteSpace(GetUser()) &&
             string.IsNullOrWhiteSpace(GetPassword()) && string.IsNullOrWhiteSpace(GetDatabase()))
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
                mSConnectionString = "Data Source=" + GetServer() + ";"
                + "Initial Catalog=" + GetDatabase() + ";"
                + "User ID=" + GetUser() + ";"
                + "Password=" + GetPassword() + "";
                SetUsingDB(true);
            }
            else
            {
                mSConnectionString = "Data Source=" + GetServer() + ";"
                + "User ID=" + GetUser() + ";"
                + "Password=" + GetPassword() + "";
                SetUsingDB(false);
            }

            try
            {
                conn = new SqlConnection(mSConnectionString);
                conn.Open();
                return true;
            }
            catch (Exception ex)
            {
                SetUsingDB(false);
                DisConnect();
                throw new Exception(ex.Message);
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

        public DataTable SendQuery(string Query)
        {
            DataTable results = new DataTable();
            if (Connect(true))
            {
          
                SqlDataAdapter dataAdapter;
                try
                {

                    dataAdapter = new SqlDataAdapter(Query, conn);
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
        public Boolean SendNonQuery(string NoNQuery)
        {
            if (Connect(true))
            {
                SqlTransaction transaction;
                transaction = conn.BeginTransaction();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(NoNQuery, conn)
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

        public object SendScalar(string ScalarQuery)
        {
            object rowsUpdated = 0;
            if (Connect(true))
            {
                SqlTransaction transaction;
                transaction = conn.BeginTransaction();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(ScalarQuery, conn)
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
        public Boolean CreateDataBase()
        {
            if (Connect(false))
            {
                string command = "CREATE DATABASE " + GetDatabase() + ";";
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(command, conn);
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
                string command = "DROP DATABASE " + GetDatabase() + ";";
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(command, conn);
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
                string command = " select name from master.dbo.sysdatabases where name like '" + GetDatabase() + "';";
                DataTable results = new DataTable();
                SqlDataAdapter dataAdapter;
                try
                {

                    dataAdapter = new SqlDataAdapter(command, conn);
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
                if(CheckForTable(t.TableName))
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

        public Boolean CheckForTable(string tableName)
        {
            string command = "select TABLE_NAME from INFORMATION_SCHEMA.tables where TABLE_NAME LIKE '" + tableName + "';";
            return SendQuery(command).Rows.Count > 0;
        }

        public Boolean DeleteTable(string tableName)
        {
            string command = "DROP TABLE " + tableName + ";";
            return SendNonQuery(command);
        }

        public Boolean CheckAndFixTable(System.Data.DataTable table)
        {
            string command = "select top 1 * from " + table.TableName + ";";
            DataTable res = SendQuery(command);
            List<DataColumn> nColumns = new List<DataColumn>();
            foreach(DataColumn dc in table.Columns)
            {
                if(!res.Columns.Contains(dc.ColumnName))
                {
                    nColumns.Add(dc);
                }
            }
            if (nColumns.Count > 0)
            {
                foreach (DataColumn d in nColumns)
                {
                    command = "ALTER TABLE [dbo].[" + table.TableName + "] ADD ";
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
                string command = "CREATE TABLE [dbo].[" + table.TableName + "](";
                foreach(DataColumn dc in table.Columns)
                {
                    command += GetColumnCommand(dc);
                    if(table.Columns.IndexOf(dc).Equals(table.Columns.Count -1) )
                    {
                        command += " );";
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
            string command;
            if(table.PrimaryKey.Length > 0)
            {
                for (int PK = 0; PK < table.PrimaryKey.Length; PK++)
                {
                    command = "Alter Table " + table.TableName + " ADD PRIMARY KEY ( " + table.PrimaryKey[PK].ColumnName + ");";
                    SendNonQuery(command);
                }
            }
            if(table.ParentRelations.Count > 0)
            {
                foreach(DataRelation relation in table.ParentRelations)
                {
                    for (int c = 0; c < relation.ChildColumns.Length; c++)
                    {
                        command = "Alter Table " + relation.ChildTable.TableName + " ADD FOREIGN KEY ( " + relation.ChildColumns[c].ColumnName + ")";
                        command += " REFERENCES " + relation.ParentTable.TableName + "(" + relation.ParentColumns[c].ColumnName + ") ON DELETE CASCADE ;";
                        SendNonQuery(command);
                    }
                }

            }
            return true;
            
        }

        private string GetColumnCommand(DataColumn dc)
        {
            string command = "";
            command += "[" + dc.ColumnName + "][";
            if (dc.DataType.Equals(System.Type.GetType("System.String")))
            {
                command += "nvarchar](" + dc.MaxLength + ")";
            }
            else if (dc.DataType.Equals(System.Type.GetType("System.Int32")))
            {
                command += "int]";
            }
            if (command.EndsWith("int]") && dc.AutoIncrement && dc.Unique)
            {
                command += " IDENTITY(1,1) ";
            }
            else if (dc.AllowDBNull)
            {
                command += " NULL";
            }
            else
            {
                command += " NOT NULL ";
            }

            return command;
        }

        public DataTable UpsertTable(DataTable table)
        {
            foreach(DataRow dataRow in table.Rows)
            {
                if(dataRow.RowState.Equals(DataRowState.Deleted))
                {
                    continue;
                }
                else if( (dataRow.RowState.Equals(DataRowState.Modified)  && ((int)dataRow["id"]) > 0) )
                {
                    string comand = "Update t set " ;
                    foreach(DataColumn dc in table.Columns)
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
                    if(comand.EndsWith(","))
                    {
                        comand = comand.Substring(0, comand.Length - 1);
                    }
                    comand += "from " + table.TableName + " as t where t.id  = " + dataRow["id"] + ";";
                    SendQuery(comand);

                }
                else if( (dataRow.RowState.Equals(DataRowState.Added)) || ((int)dataRow["id"]) < 1)
                {
                    string comand = "insert "+ table.TableName + " (";
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
                                select += (dc.DataType.Equals(System.Type.GetType("System.String")) ? ("'" + ReplaceInvalid((string)dataRow[dc]) + "'")  : dataRow[dc]) +" ,";
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
                    comand +=  ") " + select;
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
            tblfill += " from " + table.TableName;
            DataTable whereTable = table.Clone();
            whereTable.Clear();
            foreach(DataRow row in  table.Rows)
            {
                if (!row.RowState.Equals(DataRowState.Deleted))
                {
                    if (table.Columns.Contains("id") && ((int)row[table.Columns["id"]]) > 0)
                    {
                        if (row.Table.TableName.Length > 0)
                        {
                            whereTable.ImportRow(row);
                        }
                        else
                        {
                            whereTable.Rows.Add(row);
                        }
                    }
                    else if (!table.Columns.Contains("id"))
                    {
                        if (row.Table.TableName.Length > 0)
                        {
                            whereTable.ImportRow(row);
                        }
                        else
                        {
                            whereTable.Rows.Add(row);
                        }
                    }
                }
            }
            if (whereTable.Rows.Count > 0)
            {
                String where = " where ";
                where += whereTable.Columns[0].ColumnName + " in ( ";
                foreach (DataRow row in whereTable.Rows)
                {
                    if (!row.RowState.Equals(DataRowState.Deleted))
                    {
                        if (!row[whereTable.Columns[0]].Equals(DBNull.Value))
                        {
                            if (whereTable.Columns[0].DataType.Equals(System.Type.GetType("System.String")))
                            {
                                where += " '" + ReplaceInvalid((string)row[whereTable.Columns[0]]) + "' ";
                            }
                            else
                            {
                                where += " " + row[whereTable.Columns[0]] + " ";
                            }
                        }
                        if (!whereTable.Rows.IndexOf(row).Equals(whereTable.Rows.Count - 1))
                        {
                            where += " , ";
                        }
                    }
                }

                where += " );";
                if ( ! where.Equals(" where " + whereTable.Columns[0].ColumnName + " in ( " + " );" ))
                {
                    tblfill += where;
                }
            }


            DataTable ret = SendQuery(tblfill);
            ret.TableName = table.TableName;
            return ret;
        }

        public DataSet UpsertDataSet(DataSet DS)
        {
            DataSet ret = new DataSet();
            foreach (System.Data.DataTable t in DS.Tables)
            {
                DataTable nt = UpsertTable(t);
                if(t.ChildRelations.Count > 0)
                {
                    DataTable ut = DS.Tables[t.TableName];
                    for(int r = 0; r < ut.Rows.Count; r++)
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
            string ret = dirty.Replace("'", "' + CHAR(39) + '");
            ret = ret.Replace("\\", "' + CHAR(92) + '");
            ret = ret.Replace("\"", "' + CHAR(34) + '");
            ret = ret.Replace("/", "' + CHAR(47) + '");
            ret = ret.Replace("`", "' + CHAR(96) + '");
            return ret;
        }

        public DataSet GetSchema()
        {
            return dbDataSet;
        }
        public void SetConfigurationLocation(string location)
        {
            configLocation = location;
        }
        public string GetConfigurationLocation()
        {
            return configLocation;
        }

        public DataTable CallStoredProc(string proc)
        {
            string tblfill = " exec " + proc;
            DataTable ret = SendQuery(tblfill);
            ret.TableName = proc;
            return ret;

        }

        public Boolean CheckForStoredProc(string procname)
        {
            string command = "select SPECIFIC_NAME from INFORMATION_SCHEMA.ROUTINES where SPECIFIC_NAME LIKE '" + procname + "' and ROUTINE_TYPE = 'PROCEDURE';";
            return SendQuery(command).Rows.Count > 0;
        }

        public Boolean CheckAndFixStoredProc(string procname, string procStatment)
        {
            
            if(CheckForStoredProc(procname))
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
            string comand = "Delete t from " + row.Table.TableName + " as t where t.id  = " + row["id"] + ";";
            SendNonQuery(comand);
            DataTable dataTable = new DataTable(row.Table.TableName);
            return FillTable(dataTable);
        }
    }
}
