using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace HeathCarePayStubs
{
    public class extraction
    {

        public static string RequestBody(Stream inputSteam)
        {
            var bodyStream = new StreamReader(inputSteam);
            bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
            var bodyText = bodyStream.ReadToEnd();
            return bodyText;
        }

        public static DataRow BuildRow( DataTable dt, String json)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            dynamic results = JsonConvert.DeserializeObject<dynamic>(json, settings);
            DataRow nRow = dt.NewRow();
            foreach (DataColumn dc in dt.Columns)
            {
                nRow[dc.ColumnName] = results[dc.ColumnName];
            }
            return nRow;
        }
        public static DataTable BuildTable(DataTable dt, String json)
        {
            var settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            dynamic results = JsonConvert.DeserializeObject<dynamic>(json, settings);
            foreach (dynamic d in results)
            {
                DataColumn column = new DataColumn(d.Path);
                if (results[d.Path].Type.ToString().Equals("Integer", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    column.DataType = System.Type.GetType("System.Int32");
                }
                else if (results[d.Path].Type.ToString().Equals("String", System.StringComparison.InvariantCultureIgnoreCase))
                {
                    column.DataType = System.Type.GetType("System.String");
                }
                dt.Columns.Add(column);
            }
            return dt;
        }


    }
}