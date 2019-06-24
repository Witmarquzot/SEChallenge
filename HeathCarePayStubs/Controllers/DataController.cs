using System.Collections.Generic;
using System.Web.Http;
using System.Data;

using HeathCarePayStubs.Providers;
using Newtonsoft.Json;
using System.Web;
using System.Web.Http.Cors;
using System;

namespace HeathCarePayStubs.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DataController : ApiController
    {
        DBConnections.IDBShell shell = DBSetup.Shell;
        // GET: api/Data/5
  
        public string Get()
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            if (string.IsNullOrWhiteSpace(value))
            {
                DataTable ret = shell.CallStoredProc("getPaySum");
                return JsonConvert.SerializeObject(ret);
            }
            object obj = JsonConvert.DeserializeObject(value);
            if(obj.GetType().Equals(typeof(DataSet)))
            {
                DataSet ret = shell.UpsertDataSet((DataSet)obj);
                return JsonConvert.SerializeObject(ret);
            }
            else if (obj.GetType().Equals(typeof(DataTable)))
            {
                DataTable ret = shell.UpsertTable((DataTable)obj);
                return JsonConvert.SerializeObject(ret);
            }
            else if(obj.GetType().Equals(typeof(string)))
            {
                DataTable ret = shell.CallStoredProc((string)obj);
                return JsonConvert.SerializeObject(ret);
            }
            return "Unkown object type";
        }

        // POST: api/Data
        public string Post()
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            if (string.IsNullOrWhiteSpace(value))
            {
                DataTable ret = shell.CallStoredProc("getPaySum");
                return JsonConvert.SerializeObject(ret);
            }
            try
            {
                object obj = JsonConvert.DeserializeObject(value);
                if (obj.GetType().Equals(typeof(DataSet)))
                {
                    DataSet ret = shell.UpsertDataSet((DataSet)obj);
                    return JsonConvert.SerializeObject(ret);
                }
                if (obj.GetType().Equals(typeof(DataTable)))
                {
                    DataTable ret = shell.UpsertTable((DataTable)obj);
                    return JsonConvert.SerializeObject(ret);
                }
            }
            catch(Exception e)
            {
                DataTable ret = shell.CallStoredProc(value);
                return JsonConvert.SerializeObject(ret);
            }
            return "Unkown object type";
        }

        // PUT: api/Data/5
        public string Put()
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            if (string.IsNullOrWhiteSpace(value))
            {
                DataTable ret = shell.CallStoredProc("getPaySum");
                return JsonConvert.SerializeObject(ret);
            }
            object obj = JsonConvert.DeserializeObject(value);
            if (obj.GetType().Equals(typeof(DataSet)))
            {
                DataSet ret = shell.UpsertDataSet((DataSet)obj);
                return JsonConvert.SerializeObject(ret);
            }
            if (obj.GetType().Equals(typeof(DataTable)))
            {
                DataTable ret = shell.UpsertTable((DataTable)obj);
                return JsonConvert.SerializeObject(ret);
            }
            return "Unkown object type";
        }

        // DELETE: api/Data/5
        public string Delete()
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            if (string.IsNullOrWhiteSpace(value))
            {
                DataTable ret = shell.CallStoredProc("getPaySum");
                return JsonConvert.SerializeObject(ret);
            }
            object obj = JsonConvert.DeserializeObject(value);
            if (obj.GetType().Equals(typeof(DataSet)))
            {
                DataSet ret = shell.UpsertDataSet((DataSet)obj);
                return JsonConvert.SerializeObject(ret);
            }
            if (obj.GetType().Equals(typeof(DataTable)))
            {
                DataTable ret = shell.UpsertTable((DataTable)obj);
                return JsonConvert.SerializeObject(ret);
            }
            return "Unkown object type";
        }
        public string Options()
        {
            string ret = "";
            ret += "HTTP/1.1 204 No Content \r\n";
            ret += "Access-Control-Allow-Origin: http://127.0.0.1  \r\n";
            ret += "Access-Control-Allow-Methods: POST, GET, OPTIONS, Delete,Put, \r\n";
            ret += "Access-Control-Allow-Headers: X-PINGOTHER, Content-Type  \r\n";
            ret += "Vary: Accept-Encoding, Origin \r\n";
            ret += "Keep-Alive: timeout=2, max=100  \r\n";

            return ret;

        }
    }
}
