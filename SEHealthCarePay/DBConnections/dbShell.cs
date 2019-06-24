using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using DBConnections.DBControl;
using DBConnections.DBSchema;
using System.Data;
using System.IO;

namespace DBConnections
{
    public class DBShell : IDBShell
    {
        /// <summary>
        ///    DB Connection Interface the allows us to connec to multiple different types with one "View"
        /// </summary>
        DBControl.IDbConnection conn = null;
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
        ///     Runs Update/Insert/Delete on the Table rows and then Returns a newly filled table
        /// </summary>
        /// <param name="table">table we need to Update/Insert From/ Delete on Server / Fill</param>
        /// <returns>Filled datable</returns>
        virtual public System.Data.DataTable UpsertTable(System.Data.DataTable table)
        {
            return conn.UpsertTable(CleanTable(table));
        }
        private DataTable CleanTable(DataTable suspect)
        {
            if(suspect.Columns.Count < 2) //If the table only has 1 or 0 colummns it will be a fill anyway
            {
                return suspect;
            }
            if(suspect.Rows.Count < 1)// If the table has zero rows, it will be a fill only
            {
                return suspect;
            }
            DataTable baseT = conn.GetSchema().Tables[suspect.TableName];
            DataTable cleant = new DataTable
            {
                TableName = suspect.TableName,
                DisplayExpression = suspect.DisplayExpression

            };
            for (int c = 0; c < suspect.Columns.Count; c++)
            {
                string cName = suspect.Columns[c].ColumnName;
                if (baseT.Columns.Contains(cName) )
                {

                    DataColumn bCol = baseT.Columns[cName];
                    DataColumn nCol = new DataColumn
                    {
                        ColumnName = bCol.ColumnName,
                        AutoIncrement = bCol.AutoIncrement,
                        AllowDBNull = bCol.AllowDBNull,
                        DataType = bCol.DataType,
                        Unique = bCol.Unique,
                        AutoIncrementStep = bCol.AutoIncrementStep,
                        AutoIncrementSeed = bCol.AutoIncrementSeed,
                        MaxLength = bCol.MaxLength

                    };
                    cleant.Columns.Add(nCol);
                }
            }
            foreach (DataRow row in suspect.Rows)
            {
                DataRow cNrow = cleant.NewRow();
                int nRowID = 0;
                long nLRowID = 0;
                for (int c = 0; c < suspect.Columns.Count; c++)
                {
                    if ((cleant.Columns.Count - 1) >= c)
                    {
                        string cName = cleant.Columns[c].ColumnName;
                        if (cName.Equals("id", StringComparison.InvariantCultureIgnoreCase))
                        {
                            object o = row[suspect.Columns[cName]];
                            if (o.GetType().Equals(typeof(int)))
                            {
                                nRowID = ((int)o);
                            }
                            if (o.GetType().Equals(typeof(long)))
                            {
                                nLRowID = ((long)o);
                            }
                            if (nRowID > 0)
                            {
                                cNrow[cleant.Columns[cName]] = nRowID;
                            }
                            else if (nLRowID > 0)
                            {
                                cNrow[cleant.Columns[cName]] = nLRowID;
                            }
                        }
                        else
                        {
                            cNrow[cleant.Columns[cName]] = row[suspect.Columns[cName]];
                        }
                    }
                }
                if((nRowID > 0) || (nLRowID > 0))
                {
                    cleant.Columns["id"].AutoIncrement = false;
                }


                    cleant.Rows.Add(cNrow);
                    if (row.RowState.Equals(DataRowState.Modified))
                    {
                        cNrow.AcceptChanges();
                        cNrow.SetModified();
                    }
                if (row.RowState.Equals(DataRowState.Deleted))
                {
                    cNrow.AcceptChanges();
                    cNrow.Delete();
                }
                if ((nRowID > 0) || (nLRowID > 0))
                {
                    cleant.Columns["id"].AutoIncrement = true;
                }
            }
            if (suspect.PrimaryKey.Count() > 0)
            {
                DataColumn[] tPrimay = new DataColumn[suspect.PrimaryKey.Count()];
                for(int p = 0; p < suspect.PrimaryKey.Count(); p++)
                {
                    tPrimay[p] = cleant.Columns[ suspect.PrimaryKey[p].ColumnName];
                }
                cleant.PrimaryKey = tPrimay;
            }
            return cleant;

        }


        virtual public System.Data.DataTable FillTable(System.Data.DataTable table)
        {
            return conn.FillTable(table);
        }

        virtual public System.Data.DataSet UpsertDataSet(System.Data.DataSet DS)
        {
            DataSet cleanDS = new DataSet();
            foreach(DataTable t in DS.Tables)
            {
                cleanDS.Tables.Add(CleanTable(t));
            } 
            if(DS.Relations.Count > 0)
            {
                foreach (DataRelation rel in DS.Relations)
                {
                    for (int c = 0; c < rel.ChildColumns.Length; c++)
                    {
                        DataColumn parentColumn = cleanDS.Tables[rel.ParentTable.TableName].Columns[rel.ParentColumns[c].ColumnName];
                        DataColumn childColumn = cleanDS.Tables[rel.ChildTable.TableName].Columns[rel.ChildColumns[c].ColumnName];
                        if (!parentColumn.Equals(null) && !childColumn.Equals(null))
                        {
                            cleanDS.Relations.Add(parentColumn, childColumn);
                        }
                    }
                }
            }
            return conn.UpsertDataSet(cleanDS);
        }

        virtual public System.Data.DataSet FillDataSet(System.Data.DataSet DS)
        {
            return conn.FillDataSet(DS);
        }

        public void SetConfigurationLocation(String location)
        {
     
            if ((location.Length > 0) && File.Exists(location) )
            {
                XmlDocument _doc = new XmlDocument();
                _doc.Load(location);
                String _sqlType = _doc.GetElementsByTagName("SQLTYPE").Item(0).InnerText;
                if (_sqlType.Equals("LOCALMICROSOFT"))
                {
                    SetConnectionTypeLocalMicro();
                }
                else if (_sqlType.Equals("LOCALORACLE"))
                {
                    SetConnectionTypeLocalOracle();
                }
                if (!conn.Equals(null))
                {
                    conn.SetConfigurationLocation(location);
                    conn.SetUser(_doc.GetElementsByTagName("USER").Item(0).InnerText);
                    conn.SetPassword(_doc.GetElementsByTagName("PASSWORD").Item(0).InnerText);
                    conn.SetServer(_doc.GetElementsByTagName("SERVER").Item(0).InnerText);
                    conn.SetDatabase(_doc.GetElementsByTagName("DATABASE").Item(0).InnerText);
                    if(_doc.GetElementsByTagName("TEST").Count > 0)
                    {
                        if (conn.CheckForDataBase())
                        {
                            conn.DeleteDataBase();
                        }
                    }
                    DBSchema.IDBSchema _schema = new DBSchema.DefaultSchema();
                    conn.SetSchema(_schema.GetSchema());
                    Boolean _dbExists = conn.CheckForDataBase();
                    if (!_dbExists)
                    {

                        conn.CreateDataBase();
                    }
                    conn.CheckAndCorrectSchema();
                    if (!_dbExists)
                    {
                        UpsertDataSet(_schema.GetSampleData());
                    }
                    foreach(string p in _schema.getStoreProcs())
                    {
                        conn.CheckAndFixStoredProc(p, _schema.getStoredProcCmd(p));
                    }
                }
            }
        }
        public String GetConfigurationLocation()
        {
            return conn.GetConfigurationLocation();
        }

        public DataTable CallStoredProc(string proc)
        {
            return conn.CallStoredProc(proc);
        }

        public DataTable RemoveRow(DataRow row)
        {
            return conn.RemoveRow(row);
        }
    }
}
