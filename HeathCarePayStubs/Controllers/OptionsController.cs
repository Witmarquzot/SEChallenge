using System.Collections.Generic;
using System.Web.Http;
using System.Data;

using HeathCarePayStubs.Providers;
using Newtonsoft.Json;
using System.Web;
using System.Web.Http.Cors;
namespace HeathCarePayStubs.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OptionsController : ApiController
    {
        DBConnections.IDBShell shell = DBSetup.Shell;

        // GET: api/Options
        public string Get()
        {
            DataTable options = new DataTable("Options");
            return  JsonConvert.SerializeObject(shell.FillTable(options)) ;
        }

        // GET: api/Options/5
        public string Get(int id)
        {
            DataTable options = new DataTable("Options");
            if (id <= 0)
            {
                return JsonConvert.SerializeObject(shell.FillTable(options));
            }
            else
            {

                DataColumn column = new DataColumn("id")
                {
                    DataType = System.Type.GetType("System.Int32"),
                };
                options.Columns.Add(column);
                DataRow nRow = options.NewRow();
                nRow["id"] = id;
                options.Rows.Add(nRow);
                options = shell.FillTable(options);
            }
            return JsonConvert.SerializeObject(options);

        }

        // POST: api/Options
        public string Post()
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            if (string.IsNullOrWhiteSpace(value))
            {
                DataTable options = new DataTable("Options");
                return JsonConvert.SerializeObject(shell.FillTable(options));
            }
            object obj = JsonConvert.DeserializeObject(value, typeof(DataTable));
            if (obj.GetType().Equals(typeof(DataSet)))
            {
                DataSet ret = shell.UpsertDataSet((DataSet)obj);
                return JsonConvert.SerializeObject(ret);
            }
            if (obj.GetType().Equals(typeof(DataTable)))
            {
                ((DataTable)obj).TableName = "options";
                foreach(DataRow r in ((DataTable)obj).Rows )
                {
                    if (((string) r["rowMode"]).Equals("modified", System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        r.AcceptChanges();
                        r.SetModified();
                    }
                    else if (((string)r["rowMode"]).Equals("IGNORE", System.StringComparison.InvariantCultureIgnoreCase))
                    {
                        r.AcceptChanges();
                        r.Delete();
                    }
                }
                DataTable ret = shell.UpsertTable((DataTable)obj);
                return JsonConvert.SerializeObject(ret);
            }
            return value;
        }

        // PUT: api/Options/5
        public string Put()
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            if (string.IsNullOrWhiteSpace(value))
            {
                DataTable options = new DataTable("Options");
                return JsonConvert.SerializeObject(shell.FillTable(options));
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
            return value;
        }

        // DELETE: api/Options/5
        public string Delete(int id)
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            if (string.IsNullOrWhiteSpace(value))
            {
                DataTable options = new DataTable("Options");
                return JsonConvert.SerializeObject(shell.FillTable(options));
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
            return value;
        }
    }
}
