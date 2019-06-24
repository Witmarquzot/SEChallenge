using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace HeathCarePayStubs.Tests.db
{
    class DBMockConstants
    {
        public const String mockLocalOracleSQlSever = "localhost";
        public const String mockLocalMicroSQlSever = "LAPTOP-WITMARQU\\SQLEXPRESS";

        public const String mockUSER = "MOCKUSER";
        public const String mockPASS = "MOCKPASS";

        public const String mockDBNAME = "MOCKTESTING";
        public const String mockDBNAMELocalMicro = "MOCKTESTINGMICRO";
        public const String mockDBNAMELocalOracke = "MOCKTESTINGORACLE";
        public static DataSet mockDataSet = new DataSet("MOCKDATASET");
  
        public static Boolean FillmockData()
        {
            mockDataSet = new DataSet("MOCKDATASET");
            DataTable dataTable = new DataTable("Options")
            {
                DisplayExpression = "0001"
            };
            DataColumn column = new DataColumn("id")
            {
                AutoIncrement = true,
                AllowDBNull = false,
                DataType = System.Type.GetType("System.Int32"),
                Unique = true
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("OptionsNum")
            {
                AllowDBNull = false,
                DataType = System.Type.GetType("System.String"),
                Unique = true,
                MaxLength = 50
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("OptionsValue")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);
            column = new DataColumn("OptionsValType")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 25
            };
            dataTable.Columns.Add(column);
            DataColumn[] tPrimay = new DataColumn[1];
            tPrimay[0] = dataTable.Columns["id"];
            dataTable.PrimaryKey = tPrimay;
            mockDataSet.Tables.Add(dataTable);


            //Employee
            dataTable = new DataTable("Employee")
            {
                DisplayExpression = "0002"
            };
            column = new DataColumn("id")
            {
                AutoIncrement = true,
                AllowDBNull = false,
                DataType = System.Type.GetType("System.Int32"),
                Unique = true
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("LastName")
            {
                AllowDBNull = false,
                DataType = System.Type.GetType("System.String"),
                Unique = false,
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("FistName")
            {
                AllowDBNull = false,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("Address1")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("Address2")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("City")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("Province")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("PostalCode")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("Country")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            tPrimay = new DataColumn[1];
            tPrimay[0] = dataTable.Columns["id"];
            dataTable.PrimaryKey = tPrimay;
            mockDataSet.Tables.Add(dataTable);

            //Family Members
            dataTable = new DataTable("FamilyMember")
            {
                DisplayExpression = "0003"
            };
            column = new DataColumn("id")
            {
                AutoIncrement = true,
                AllowDBNull = false,
                DataType = System.Type.GetType("System.Int32"),
                Unique = true
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("employeeID")
            {
                AllowDBNull = false,
                DataType = System.Type.GetType("System.Int32"),
                Unique = false,
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("FistName")
            {
                AllowDBNull = false,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("LastName")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            tPrimay = new DataColumn[1];
            tPrimay[0] = dataTable.Columns["id"];
            dataTable.PrimaryKey = tPrimay;
            mockDataSet.Tables.Add(dataTable);
            DataColumn parentColumn = mockDataSet.Tables["Employee"].Columns["id"];
            DataColumn childColumn = mockDataSet.Tables["FamilyMember"].Columns["employeeID"];
            DataRelation relation = new DataRelation("Employee2FamilyMember", parentColumn, childColumn);
            mockDataSet.Tables["FamilyMember"].ParentRelations.Add(relation);

            //Discount
            dataTable = new DataTable("Discount")
            {
                DisplayExpression = "0004"
            };
            column = new DataColumn("id")
            {
                AutoIncrement = true,
                AllowDBNull = false,
                DataType = System.Type.GetType("System.Int32"),
                Unique = true
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("Name")
            {
                AllowDBNull = false,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("Percent")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("Fields")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            tPrimay = new DataColumn[1];
            tPrimay[0] = dataTable.Columns["id"];
            dataTable.PrimaryKey = tPrimay;
            mockDataSet.Tables.Add(dataTable);

            return true;
        }
 
    }
}
