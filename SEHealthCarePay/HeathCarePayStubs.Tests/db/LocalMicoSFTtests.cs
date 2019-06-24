using System;
using HeathCarePayStubs.Tests.db;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DBConnections;
using DBConnections.DBControl;
namespace HeathCarePayStubs.Tests
{
    [TestClass]
    public class LocalMicroSFTtests
    {
        [TestMethod]
        public void CheckMicoSFTServerSet()
        {
            IDbConnection mdb = new MicoSFTSql();
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
        public void CheckMicoSFTUserSet()
        {
            IDbConnection mdb = new MicoSFTSql();
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
        public void CheckMicoSFTPassSet()
        {
            IDbConnection mdb = new MicoSFTSql();
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
        public void CheckMicoSFTDBSet()
        {
            IDbConnection mdb = new MicoSFTSql();
            mdb.SetDatabase(DBMockConstants.mockDBNAMELocalMicro);
            if (Equals(mdb.GetDatabase(), DBMockConstants.mockDBNAMELocalMicro))
            {
                Assert.AreEqual(1, 1);
            }
            else
            {
                Assert.Fail(mdb.GetDatabase() + " does not match " + DBMockConstants.mockDBNAMELocalMicro);
            }

        }


        [TestMethod]
        public void CheckMicoSFTConnection()
        {
            IDbConnection mdb = new MicoSFTSql();
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
            mdb.SetDatabase(DBMockConstants.mockDBNAMELocalMicro);

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
        public void CheckMicoSFTDBcreatDelete()
        {

            CheckMicoSFTDeleteDB();
            CheckMicoSFTCreateDB();
            CheckMicoSFTDeleteDB();
            CheckMicoSFTDBExists();
            CheckMicoSFTDeleteDB();
            CheckMicoSFTCheckFixSchema();
            //Dont run delete as it runs an index
        }
        public Boolean CheckMicoSFTDeleteDB()
        {
            IDbConnection mdb = new MicoSFTSql();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalMicroSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
                , DBMockConstants.mockDBNAMELocalMicro, DBMockConstants.mockDataSet);
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
 
        public Boolean CheckMicoSFTCreateDB()
        {
            IDbConnection mdb = new MicoSFTSql();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalMicroSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
                , DBMockConstants.mockDBNAMELocalMicro, DBMockConstants.mockDataSet);
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

        public Boolean CheckMicoSFTDBExists()
        {
            IDbConnection mdb = new MicoSFTSql();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalMicroSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
                , DBMockConstants.mockDBNAMELocalMicro, DBMockConstants.mockDataSet);
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

        public Boolean CheckMicoSFTCheckFixSchema()
        {
            DBMockConstants.FillmockData();
            IDbConnection mdb = new MicoSFTSql();
            mdb.SetConnectionInfo(DBMockConstants.mockLocalMicroSQlSever, DBMockConstants.mockUSER, DBMockConstants.mockPASS
                , DBMockConstants.mockDBNAMELocalMicro, DBMockConstants.mockDataSet);
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
