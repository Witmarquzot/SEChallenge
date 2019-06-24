using System;
using HeathCarePayStubs.Tests.db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBConnections;

namespace HeathCarePayStubs.Tests
{
    [TestClass]
    public class LocalMicroSFTShellTests
    {
        [TestMethod]
        public void CheckMicoSFTShellServerSet()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalMicro();
            mdb.SetServer(DBMockConstants.mockLocalMicroSQlSever);
            if (Equals(mdb.GetServer(), DBMockConstants.mockLocalMicroSQlSever))
            {
                Assert.AreEqual(1, 1);
            }
            else
            {
                Assert.Fail(mdb.GetServer() + " does not match " + DBMockConstants.mockLocalMicroSQlSever);
            }
        }
        [TestMethod]
        public void CheckMicoSFTShellUserSet()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalMicro();
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
        public void CheckMicoSFTShellPassSet()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalMicro();
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
        public void CheckMicoSFTShellDBSet()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalMicro();
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
        public void CheckMicoSFTShellConnection()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalMicro();
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
            mdb.SetServer(DBMockConstants.mockLocalMicroSQlSever);

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
                Assert.AreEqual(1, 1);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);

            }
        }
        [TestMethod]
        public void CheckMicoSFTShellDBcreatDelete()
        {

            CheckMicoSFTShellDeleteDB();
            CheckMicoSFTShellCreateDB();
            CheckMicoSFTShellDeleteDB();
            CheckMicoSFTShellDBExists();
            CheckMicoSFTShellDeleteDB();
            CheckMicoSFTShellCheckFixSchema();
            //Dont run delete as it runs an index
        }
        public Boolean CheckMicoSFTShellDeleteDB()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalMicro();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalMicroSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
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

        public Boolean CheckMicoSFTShellCreateDB()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalMicro();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalMicroSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
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

        public Boolean CheckMicoSFTShellDBExists()
        {
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalMicro();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalMicroSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
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

        public Boolean CheckMicoSFTShellCheckFixSchema()
        {
            DBMockConstants.FillmockData();
            DBShell mdb = new DBShell("");
            mdb.SetConnectionTypeLocalMicro();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalMicroSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
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