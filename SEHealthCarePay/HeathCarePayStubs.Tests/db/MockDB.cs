using DBConnections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeathCarePayStubs.Tests.db
{
    class MockDB
    {

        String serverName;
        String userName;
        String encryptedPass;
        String dbName;
        Boolean firstConnect = false;
        public bool Connect(bool allowSet)
        {
            firstConnect = false;
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
            if(allowSet)
            {
                firstConnect = true;
            }
            return true;
        }

        public void setFirstConnect(Boolean value)
        {
            firstConnect = value;
        }
        public Boolean getFirstConnect()
        {
            return firstConnect;
        }

        public void DisConnect()
        {
            throw new NotImplementedException();
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


        public void SetConnectionInfo(string server, string user, string password, string database, String[] schema)
        {
            serverName = server;
            userName = user;
            encryptedPass = password;
            dbName = database;

        }

        public void SetDatabase(string database, String[] schema)
        {
            dbName = database;

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

    }
}