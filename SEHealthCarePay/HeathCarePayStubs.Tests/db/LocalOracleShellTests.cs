using System;
using HeathCarePayStubs.Tests.db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBConnections;

namespace HeathCarePayStubs.Tests
{
    [TestClass]
    public class LocalOracleShellTests
    {
        [TestMethod]
        public void CheckOracleShellServerSet()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalOracle();
            mdb.SetServer(DBMockConstants.mockLocalOracleSQlSever);
            if (Equals(mdb.GetServer(), DBMockConstants.mockLocalOracleSQlSever))
            {
                Assert.AreEqual(1, 1);
            }
            else
            {
                Assert.Fail(mdb.GetServer() + " does not match " + DBMockConstants.mockLocalOracleSQlSever);
            }
        }

        [TestMethod]
        public void CheckOracleShellUserSet()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalOracle();
            mdb.SetUser(DBMockConstants.mockUSER);
            if (Equals(mdb.GetUser(), DBMockConstants.mockUSER))
            {
                Assert.AreEqual(1, 1);
            }
            else
            {
                Assert.Fail(mdb.GetUser() + " does not match " + DBMockConstants.mockUSER);
            }

        }
        [TestMethod]
        public void CheckOracleShellPassSet()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalOracle();
            mdb.SetPassword(DBMockConstants.mockPASS);
            if (Equals(mdb.GetPassword(), DBMockConstants.mockPASS))
            {
                Assert.AreEqual(1, 1);
            }
            else
            {
                Assert.Fail(mdb.GetPassword() + " does not match " + DBMockConstants.mockPASS);
            }
        }




        [TestMethod]
        public void CheckOracleShellDBSet()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalOracle();
            mdb.SetDatabase(DBMockConstants.mockDBNAME);
            if (Equals(mdb.GetDatabase(), DBMockConstants.mockDBNAME))
            {
                Assert.AreEqual(1, 1);
            }
            else
            {
                Assert.Fail(mdb.GetDatabase() + " does not match " + DBMockConstants.mockDBNAME);
            }

        }


        [TestMethod]
        public void CheckOracleShellConnection()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalOracle();
            try
            {
                if (mdb.Connect(false))
                {
                    Assert.Fail(DBConstants.noInfoError);
                }
            }
            catch (Exception e)
            {
                if (!e.Message.ToString().Equals(DBConstants.noInfoError, StringComparison.InvariantCultureIgnoreCase))
                {
                    Assert.Fail(e.Message);
                }
            }
            mdb.SetServer(DBMockConstants.mockLocalOracleSQlSever);

            try
            {
                if (mdb.Connect(false))
                {
                    Assert.Fail(DBConstants.noUserError);
                }
            }
            catch (Exception e)
            {
                if (!e.Message.ToString().Equals(DBConstants.noUserError, StringComparison.InvariantCultureIgnoreCase))
                {
                    Assert.Fail(e.Message);
                }
            }
            mdb.SetUser(DBMockConstants.mockUSER);

            try
            {
                if (mdb.Connect(false))
                {
                    Assert.Fail(DBConstants.noInfoError + "\n " + DBConstants.noPasswordError);
                }
            }
            catch (Exception e)
            {
                if (!e.Message.ToString().Equals(DBConstants.noPasswordError, StringComparison.InvariantCultureIgnoreCase))
                {
                    Assert.Fail(e.Message);
                }
            }
            mdb.SetPassword(DBMockConstants.mockPASS);

            try
            {
                if (mdb.Connect(false))
                {
                    Assert.Fail(DBConstants.noDBerror);
                }
            }
            catch (Exception e)
            {
                if (!e.Message.ToString().Equals(DBConstants.noDBerror, StringComparison.InvariantCultureIgnoreCase))
                {
                    Assert.Fail(e.Message);
                }
            }
            mdb.SetDatabase(DBMockConstants.mockDBNAME);

            try
            {
                mdb.Connect(false);
                mdb.DisConnect();
                // if (mdb.getFirstConnect())
                //   {
                Assert.AreEqual(1, 1);
                /* }
                 else
                 {
                     Assert.Fail("My Sql Database of " + DBMockConstants.mockDBNAME + "Still existed");
                 }*/
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);

            }
        }
        [TestMethod]
        public void CheckOracleShellDBcreatDelete()
        {


            CheckOracleShellDeleteDB();
            CheckOracleShellCreateDB();
            CheckOracleShellDeleteDB();
            CheckOracleShellDBExists();
            CheckOracleShellDeleteDB();
            CheckOracleShellCheckFixSchema();
            //Dont run delete as it runs an index
        }


        public Boolean CheckOracleShellDeleteDB()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalOracle();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalOracleSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
                , DBMockConstants.mockDBNAME, DBMockConstants.mockDataSet);
            try
            {
                if (mdb.CheckForDataBase())
                {
                    mdb.DeleteDataBase();
                }
                Assert.AreEqual(1, 1);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            return true;
        }

        public Boolean CheckOracleShellCreateDB()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalOracle();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalOracleSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
                , DBMockConstants.mockDBNAME, DBMockConstants.mockDataSet);
            try
            {
                if (!mdb.CheckForDataBase())
                {
                    mdb.CreateDataBase();
                }
                Assert.AreEqual(1, 1);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            return true;
        }


        public Boolean CheckOracleShellDBExists()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalOracle();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalOracleSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
                , DBMockConstants.mockDBNAME, DBMockConstants.mockDataSet);
            try
            {
                if (!mdb.CheckForDataBase())
                {
                    mdb.CreateDataBase();
                }
                Assert.AreEqual(1, 1);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            return true;
        }


        public Boolean CheckOracleShellCheckFixSchema()
        {
            DBMockConstants.FillmockData();
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalOracle();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalOracleSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
                , DBMockConstants.mockDBNAME, DBMockConstants.mockDataSet);
            try
            {
                if (!mdb.CheckForDataBase())
                {
                    mdb.CreateDataBase();
                }
                mdb.CheckAndCorrectSchema();
                Assert.AreEqual(1, 1);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            return true;
        }
    }
}