using System;
using HeathCarePayStubs.Tests.db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBConnections;
using DBConnections.DBControl;

namespace HeathCarePayStubs.Tests
{
    [TestClass]
    public class LocalOracleTests
    {
        [TestMethod]
        public void CheckOracleServerSet()
        {
            IDbConnection mdb = new OraclSQL();
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
        public void CheckOracleUserSet()
        {
            IDbConnection mdb = new OraclSQL();
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
        public void CheckOraclePassSet()
        {
            IDbConnection mdb = new OraclSQL();
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
        public void CheckOracleDBSet()
        {
            IDbConnection mdb = new OraclSQL();
            mdb.SetDatabase(DBMockConstants.mockDBNAMELocalOracke);
            if (Equals(mdb.GetDatabase(), DBMockConstants.mockDBNAMELocalOracke))
            {
                Assert.AreEqual(1, 1);
            }
            else
            {
                Assert.Fail(mdb.GetDatabase() + " does not match " + DBMockConstants.mockDBNAMELocalOracke);
            }

        }

        [TestMethod]
        public void CheckOracleConnection()
        {
            IDbConnection mdb = new OraclSQL();
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
            mdb.SetDatabase(DBMockConstants.mockDBNAMELocalOracke);

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
                     Assert.Fail("My Sql Database of " + DBMockConstants.mockDBNAMELocalOracke + "Still existed");
                 }*/
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);

            }
        }

        [TestMethod]
        public void CheckOracleDBcreatDelete()
        {

            CheckOracleDeleteDB();
            CheckOracleCreateDB();
            CheckOracleDeleteDB();
            CheckOracleDBExists();
            CheckOracleDeleteDB();
            CheckOracleCheckFixSchema();
            //Dont run delete as it runs an index
        }


        public Boolean CheckOracleDeleteDB()
        {
            IDbConnection mdb = new OraclSQL();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalOracleSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
                , DBMockConstants.mockDBNAMELocalOracke, DBMockConstants.mockDataSet);
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
 
        public Boolean CheckOracleCreateDB()
        {
            IDbConnection mdb = new OraclSQL();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalOracleSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
                , DBMockConstants.mockDBNAMELocalOracke, DBMockConstants.mockDataSet);
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


        public Boolean CheckOracleDBExists()
        {
            IDbConnection mdb = new OraclSQL();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalOracleSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
                , DBMockConstants.mockDBNAMELocalOracke, DBMockConstants.mockDataSet);
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


        public Boolean CheckOracleCheckFixSchema()
        {
            DBMockConstants.FillmockData();
            IDbConnection mdb = new OraclSQL();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalOracleSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
                , DBMockConstants.mockDBNAMELocalOracke, DBMockConstants.mockDataSet);
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
