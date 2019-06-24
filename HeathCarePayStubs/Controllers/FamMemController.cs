using System.Collections.Generic;
using System.Web.Http;
using System.Data;

using HeathCarePayStubs.Providers;
using Newtonsoft.Json;
using System.Web.Http.Cors;
using System.Web;

namespace HeathCarePayStubs.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FamMemController : ApiController
    {
        DBConnections.IDBShell shell = DBSetup.Shell;
        // GET: api/FamMem
        public string Get()
        {
            DataTable famMem = new DataTable("FamilyMember");
            return JsonConvert.SerializeObject(shell.FillTable(famMem));
        }
        // GET: api/FamMem/5
        public string Get(int id)
        {

            DataTable FamilyMember = new DataTable("FamilyMember");
            if (id <= 0)
            {
                return JsonConvert.SerializeObject(shell.FillTable(FamilyMember));
            }
            else
            {

                DataColumn column = new DataColumn("id")
                {
                    DataType = System.Type.GetType("System.Int32"),
                };
                FamilyMember.Columns.Add(column);
                DataRow nRow = FamilyMember.NewRow();
                nRow["id"] = id;
                FamilyMember.Rows.Add(nRow);
                FamilyMember = shell.FillTable(FamilyMember);
            }
            return JsonConvert.SerializeObject(FamilyMember);
        }

        // POST: api/FamMem
        public string Post()
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            DataTable FamilyMember = new DataTable("FamilyMember");

            FamilyMember = extraction.BuildTable(FamilyMember, value);
            DataRow nRow = extraction.BuildRow(FamilyMember, value);
            FamilyMember.Rows.Add(nRow);
            if (FamilyMember.Columns.Contains("id"))
            {
                if (((int)nRow["id"]) > 0)
                {
                    nRow.AcceptChanges();
                    nRow.SetModified();
                }
            }
            DataTable ret = shell.UpsertTable(FamilyMember);
            return JsonConvert.SerializeObject(ret);


        }

        // PUT: api/FamMem/5
        public string Put(int id)
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            DataTable FamilyMember = new DataTable("Discount");
            if (id <= 0)
            {
                return JsonConvert.SerializeObject(shell.FillTable(FamilyMember));
            }
            else
            {
                FamilyMember = extraction.BuildTable(FamilyMember, value);
                DataRow nRow = extraction.BuildRow(FamilyMember, value);
                FamilyMember.Rows.Add(nRow);
                if (id > 0)
                {
                    nRow.AcceptChanges();
                    nRow.SetModified();
                }
                FamilyMember = shell.UpsertTable(FamilyMember);



            }
            return JsonConvert.SerializeObject(FamilyMember);
        }
        // DELETE: api/FamMem/5
        public string Delete(int id)
        {
            DataTable FamilyMember = new DataTable("FamilyMember");
            if (id <= 0)
            {
                return JsonConvert.SerializeObject(shell.FillTable(FamilyMember));
            }
            else
            {

                DataColumn column = new DataColumn("id")
                {
                    DataType = System.Type.GetType("System.Int32"),
                };
                FamilyMember.Columns.Add(column);
                DataRow nRow = FamilyMember.NewRow();
                nRow["id"] = id;
                FamilyMember = shell.RemoveRow(nRow);
            }
            return JsonConvert.SerializeObject(FamilyMember);
        }
    }
}
