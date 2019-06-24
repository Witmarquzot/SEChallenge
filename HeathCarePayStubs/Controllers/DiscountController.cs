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
    public class DiscountController : ApiController
    {

        DBConnections.IDBShell shell = DBSetup.Shell;

        // GET: api/Discount
        public string Get()
        {
            DataTable Discount = new DataTable("Discount");
            return JsonConvert.SerializeObject(shell.FillTable(Discount));
        }
        // GET: api/Discount/5
        public string Get(int id)
        {
            DataTable Discount = new DataTable("Discount");
            if (id <= 0)
            {
                return JsonConvert.SerializeObject(shell.FillTable(Discount));
            }
            else
            {

                DataColumn column = new DataColumn("id")
                {
                    DataType = System.Type.GetType("System.Int32"),
                };
                Discount.Columns.Add(column);
                DataRow nRow = Discount.NewRow();
                nRow["id"] = id;
                Discount.Rows.Add(nRow);
                Discount = shell.FillTable(Discount);
            }
            return JsonConvert.SerializeObject(Discount);
        }

        // POST: api/Discount
        public string Post()
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            DataTable Discount = new DataTable("Discount");

                Discount = extraction.BuildTable(Discount, value);
                DataRow nRow = extraction.BuildRow(Discount, value);
                Discount.Rows.Add(nRow);
                if (Discount.Columns.Contains("id"))
                {
                    if (((int)nRow["id"]) > 0)
                    {
                        nRow.AcceptChanges();
                        nRow.SetModified();
                    }
                }
                DataTable ret = shell.UpsertTable(Discount);
                return JsonConvert.SerializeObject(ret);
            

        }

        // PUT: api/Discount/5
        public string Put(int id)
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            DataTable Discount = new DataTable("Discount");
            if (id <= 0)
            {
                return JsonConvert.SerializeObject(shell.FillTable(Discount));
            }
            else
            {
                Discount = extraction.BuildTable(Discount, value);
                DataRow nRow =   extraction.BuildRow(Discount, value);
                Discount.Rows.Add(nRow);
                if(id > 0)
                {
                    nRow.AcceptChanges();
                    nRow.SetModified();
                }
                Discount = shell.UpsertTable(Discount);
        


            }
            return JsonConvert.SerializeObject(Discount);
        }

        // DELETE: api/Discount/5
        public string Delete(int id)
        {
            DataTable Discount = new DataTable("Discount");
            if (id <= 0)
            {
                return JsonConvert.SerializeObject(shell.FillTable(Discount));
            }
            else
            {

                DataColumn column = new DataColumn("id")
                {
                    DataType = System.Type.GetType("System.Int32"),
                };
                Discount.Columns.Add(column);
                DataRow nRow = Discount.NewRow();
                nRow["id"] = id;
                Discount = shell.RemoveRow(nRow);
            }
            return JsonConvert.SerializeObject(Discount);
        }

    }
  
}
