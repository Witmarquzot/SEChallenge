
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
    public class EmployeeController : ApiController
    {

        DBConnections.IDBShell shell = DBSetup.Shell;
        // GET: api/Employee
        public string Get()
        {
            DataTable employee = new DataTable("Employee");
            return JsonConvert.SerializeObject(shell.FillTable(employee));
        }
        // GET: api/Employee/5
        public string Get(int id)
        {
            DataTable employee = new DataTable("Employee");
            if (id <= 0)
            {
                return JsonConvert.SerializeObject(shell.FillTable(employee));
            }
            else
            {

                DataColumn column = new DataColumn("id")
                {
                    DataType = System.Type.GetType("System.Int32"),
                };
                employee.Columns.Add(column);
                DataRow nRow = employee.NewRow();
                nRow["id"] = id;
                employee.Rows.Add(nRow);
                employee = shell.FillTable(employee);
            }
            return JsonConvert.SerializeObject(employee);
        }

        // POST: api/Employee
        public string Post()
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            DataTable Employee = new DataTable("Employee");

            Employee = extraction.BuildTable(Employee, value);
            DataRow nRow = extraction.BuildRow(Employee, value);
            Employee.Rows.Add(nRow);
            if (Employee.Columns.Contains("id"))
            {
                if (((int)nRow["id"]) > 0)
                {
                    nRow.AcceptChanges();
                    nRow.SetModified();
                }
            }
            DataTable ret = shell.UpsertTable(Employee);
            return JsonConvert.SerializeObject(ret);


        }
        // PUT: api/Employee/5
        public string Put(int id)
        {
            string value = extraction.RequestBody(HttpContext.Current.Request.InputStream);
            DataTable Employee = new DataTable("Employee");
            if (id <= 0)
            {
                return JsonConvert.SerializeObject(shell.FillTable(Employee));
            }
            else
            {
                Employee = extraction.BuildTable(Employee, value);
                DataRow nRow = extraction.BuildRow(Employee, value);
                Employee.Rows.Add(nRow);
                if (id > 0)
                {
                    nRow.AcceptChanges();
                    nRow.SetModified();
                }
                Employee = shell.UpsertTable(Employee);



            }
            return JsonConvert.SerializeObject(Employee);
        }
        // DELETE: api/Employee/5
        public string Delete(int id)
        {
            DataTable Employee = new DataTable("Employee");
            if (id <= 0)
            {
                return JsonConvert.SerializeObject(shell.FillTable(Employee));
            }
            else
            {

                DataColumn column = new DataColumn("id")
                {
                    DataType = System.Type.GetType("System.Int32"),
                };
                Employee.Columns.Add(column);
                DataRow nRow = Employee.NewRow();
                nRow["id"] = id;
                Employee = shell.RemoveRow(nRow);
            }
            return JsonConvert.SerializeObject(Employee);
        }
    }
}
