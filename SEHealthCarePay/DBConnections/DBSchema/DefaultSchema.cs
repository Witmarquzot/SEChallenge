using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnections.DBSchema
{
    public class DefaultSchema : IDBSchema
    {
        public DefaultSchema()
        {

        }
        public DataSet GetSampleData()
        {
            DataSet sample = GetSchema();
            DataTable options = sample.Tables["Options"];
            DataRow nRow = options.NewRow();
            nRow["OptionsNum"] = "StandardPayCheck";
            nRow["OptionsValue"] = "2000.00";
            nRow["OptionsValType"] = "Money";
            options.Rows.Add(nRow);
            nRow = options.NewRow();
            nRow["OptionsNum"] = "EmployeeYearlyHealth";
            nRow["OptionsValue"] = "1000.00";
            nRow["OptionsValType"] = "Money";
            options.Rows.Add(nRow);
            nRow = options.NewRow();
            nRow["OptionsNum"] = "EFamMemYearlyHealth";
            nRow["OptionsValue"] = "500.00";
            nRow["OptionsValType"] = "Money";
            options.Rows.Add(nRow);
            nRow = options.NewRow();
            nRow["OptionsNum"] = "PayChecksPerYear";
            nRow["OptionsValue"] = "26";
            nRow["OptionsValType"] = "Numerical";
            options.Rows.Add(nRow);

 

            DataTable employee = sample.Tables["Employee"];
            DataRow nEmmp = employee.NewRow();
            nEmmp["LastName"] = "Durrant";
            nEmmp["FirstName"] = "Tyler";
            DataTable famMeM = sample.Tables["FamilyMember"];
            DataRow nFamMem01 = famMeM.NewRow();
            nFamMem01["FirstName"] = "Jackson";
            nFamMem01["LastName"] = "Young";
            nFamMem01["employeeID"] = nEmmp["id"];
            nFamMem01.SetParentRow(nEmmp);
            DataRow nFamMem02 = famMeM.NewRow();
            nFamMem02["FirstName"] = "Crystal";
            nFamMem02["LastName"] = "Young";
            nFamMem02["employeeID"] = nEmmp["id"];
            nFamMem02.SetParentRow(nEmmp);
            employee.Rows.Add(nEmmp);
            famMeM.Rows.Add(nFamMem01);
            famMeM.Rows.Add(nFamMem02);

            nEmmp = employee.NewRow();
            nEmmp["LastName"] = "Aardvark";
            nEmmp["FirstName"] = "Bob";
            nFamMem01 = famMeM.NewRow();
            nFamMem01["FirstName"] = "Fred";
            nFamMem01["employeeID"] = nEmmp["id"];
            nFamMem01.SetParentRow(nEmmp);
            nFamMem02 = famMeM.NewRow();
            nFamMem02["FirstName"] = "Georgia";
            nFamMem02["employeeID"] = nEmmp["id"];
            nFamMem02.SetParentRow(nEmmp);
            DataRow nFamMem03 = famMeM.NewRow();
            nFamMem03["FirstName"] = "Aiden";
            nFamMem03["employeeID"] = nEmmp["id"];
            nFamMem03.SetParentRow(nEmmp);
            employee.Rows.Add(nEmmp);
            famMeM.Rows.Add(nFamMem01);
            famMeM.Rows.Add(nFamMem02);
            famMeM.Rows.Add(nFamMem03);

            nEmmp = employee.NewRow();
            nEmmp["LastName"] = "Smith";
            nEmmp["FirstName"] = "Aaron";
            nFamMem01 = famMeM.NewRow();
            nFamMem01["FirstName"] = "Abraham";

            nFamMem01["employeeID"] = nEmmp["id"];
            nFamMem01.SetParentRow(nEmmp);
            nFamMem02 = famMeM.NewRow();
            nFamMem02["FirstName"] = "Arthur";
            nFamMem02["employeeID"] = nEmmp["id"];
            nFamMem02.SetParentRow(nEmmp);
            nFamMem03 = famMeM.NewRow();
            nFamMem03["FirstName"] = "Alexander";
            nFamMem03["employeeID"] = nEmmp["id"];
            nFamMem03.SetParentRow(nEmmp);

            employee.Rows.Add(nEmmp);
            famMeM.Rows.Add(nFamMem01);
            famMeM.Rows.Add(nFamMem02);
            famMeM.Rows.Add(nFamMem03);

            nEmmp = employee.NewRow();
            nEmmp["LastName"] = "Smithy";
            nEmmp["FirstName"] = "J'san";
            nFamMem01 = famMeM.NewRow();
            nFamMem01["FirstName"] = "Julia";
            nFamMem01["employeeID"] = nEmmp["id"];
            nFamMem01.SetParentRow(nEmmp);
            nFamMem02 = famMeM.NewRow();
            nFamMem02["FirstName"] = "Jasmine";
            nFamMem02["employeeID"] = nEmmp["id"];
            nFamMem02.SetParentRow(nEmmp);
            nFamMem03 = famMeM.NewRow();
            nFamMem03["FirstName"] = "Jace";
            nFamMem03["LastName"] = "Andrews";
            nFamMem03["employeeID"] = nEmmp["id"];
            nFamMem03.SetParentRow(nEmmp);

            employee.Rows.Add(nEmmp);
            famMeM.Rows.Add(nFamMem01);
            famMeM.Rows.Add(nFamMem02);
            famMeM.Rows.Add(nFamMem03);
            options.AcceptChanges();

            DataTable discount = sample.Tables["Discount"];
            nRow = discount.NewRow();
            nRow["Name"] = "A First names are Awesome";
            nRow["Conidition01"] = "Starts with";
            nRow["Conidition02"] = "A";
            nRow["PercntValue"] = "10";
            nRow["ApplyFields"] = "FirstName";
            nRow["Type"] = "Exclusive";
            discount.Rows.Add(nRow);
            nRow = discount.NewRow();
            nRow["Name"] = "A Last names are Awesome";
            nRow["Conidition01"] = "Starts with";
            nRow["Conidition02"] = "A";
            nRow["PercntValue"] = "10";
            nRow["ApplyFields"] = "LastName";
            nRow["Type"] = "Exclusive";
            discount.Rows.Add(nRow);

            discount.AcceptChanges();
            employee.AcceptChanges();
            famMeM.AcceptChanges();
            return sample;
        }

        public DataSet GetSchema()
        {
            DataSet ds = new DataSet();
            DataTable dataTable = new DataTable("Options")
            {
                DisplayExpression = "0001"
            };
            DataColumn column = new DataColumn("id")
            {
                AutoIncrement = true,
                AllowDBNull = false,
                DataType = System.Type.GetType("System.Int32"),
                Unique = true,
                AutoIncrementStep = -1,
                AutoIncrementSeed = 0

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
            ds.Tables.Add(dataTable);


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
                Unique = true,
                AutoIncrementStep = -1,
                AutoIncrementSeed = 0

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

            column = new DataColumn("FirstName")
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
            ds.Tables.Add(dataTable);

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
                Unique = true,
                AutoIncrementStep = -1,
                AutoIncrementSeed = 0

            };
            dataTable.Columns.Add(column);

            column = new DataColumn("employeeID")
            {
                AllowDBNull = false,
                DataType = System.Type.GetType("System.Int32"),
                Unique = false,
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("FirstName")
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
            ds.Tables.Add(dataTable);
            DataColumn parentColumn = ds.Tables["Employee"].Columns["id"];
            DataColumn childColumn = ds.Tables["FamilyMember"].Columns["employeeID"];
           DataRelation rel = ds.Relations.Add(parentColumn, childColumn);
            rel.ChildKeyConstraint.UpdateRule = Rule.Cascade;
            rel.ChildKeyConstraint.DeleteRule = Rule.Cascade;
           /* DataRelation relation = new DataRelation("Employee2FamilyMember", parentColumn, childColumn);
           
            ds.Tables["FamilyMember"].ParentRelations.Add(relation);*/
            

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
                Unique = true,
                AutoIncrementStep = -1,
                AutoIncrementSeed = 0

            };
            dataTable.Columns.Add(column);

            column = new DataColumn("Name")
            {
                AllowDBNull = false,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);
            column = new DataColumn("Conidition01")
            {
                AllowDBNull = false,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);
            column = new DataColumn("Conidition02")
            {
                AllowDBNull = false,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);
            column = new DataColumn("PercntValue")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);

            column = new DataColumn("ApplyFields")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);
            column = new DataColumn("Type")
            {
                AllowDBNull = true,
                DataType = System.Type.GetType("System.String"),
                MaxLength = 500
            };
            dataTable.Columns.Add(column);
            tPrimay = new DataColumn[1];
            tPrimay[0] = dataTable.Columns["id"];
            dataTable.PrimaryKey = tPrimay;
            ds.Tables.Add(dataTable);
            return ds;
        }

        public string[] getStoreProcs()
        {
            return new string[] { "getPaySum", "getEmpInf" };
        }
        public string getStoredProcCmd(string procName)
        {
            if(procName.Equals("getPaySum", StringComparison.InvariantCultureIgnoreCase))
            {
                string ret = "";
                ret += "[getPaySum] ";
                ret += "as ";
               // ret += "SET NOCOUNT ON; ";
               // ret += "SET ANSI_NULLS ON; ";
                ret += "Create table #DiscountStatement(stmt nvarchar(2000) ) ";
                ret += "Create table #Discountavail (employeeId int null, fammid int null, percnt int null) ";
                ret += "Create table #ret(empID int, famid int, LastName nvarchar(500), FirstName nvarchar(500) , BaseCost Money, DscntPrcnt int, yearlycost money, perpay money) ";
                ret += "Declare @Table01 as nvarchar(30) ";
                ret += "Declare @table02 as nvarchar(30) ";
                ret += "DECLARE @PayCheckPerYear as int ";
                ret += "DECLARE @StandardPay as Money ";
                ret += "DECLARE @EmpHealthCost as Money ";
                ret += "Declare @FamMemHealthCost as Money ";

                ret += "Set @PayCheckPerYear = (Select OptionsValue from Options where OptionsNum like 'PayChecksPerYear') ";
                ret += "Set @StandardPay = (Select OptionsValue from Options where OptionsNum like 'StandardPayCheck') ";
                ret += "Set @EmpHealthCost = (Select OptionsValue from Options where OptionsNum like 'EmployeeYearlyHealth') ";
                ret += "Set @FamMemHealthCost = (Select OptionsValue from Options where OptionsNum like 'EFamMemYearlyHealth') ";
                ret += "Set @Table01 = 'Employee' ";
                ret += "Set @Table02 = 'FamilyMember' ";
                ret += "Declare @id as int ";

                ret += "DECLARE db_cursor CURSOR FOR ";
                ret += "Select id ";
                ret += "From Discount order by Type DESC, isnull(PercntValue, 0) desc, Name asc ";
                ret += "OPEN db_cursor  ";
                ret += "FETCH NEXT FROM db_cursor INTO @id ";
                ret += "WHILE @@FETCH_STATUS = 0  ";
                ret += "BEGIN  ";
                ret += "Declare @sqlStmt as nvarchar(2000) ";

                ret += "SET @sqlStmt = 'insert into #Discountavail (employeeId, fammid,percnt) Select id,null, ' + (select PercntValue from Discount where id = @id) +  '  from ' + @Table01 + ' as t where ' + (select ApplyFields from Discount where id = @id) + ' '  + ";
                ret += "CaSE WHEN (select Conidition01 from Discount where id = @id)like 'Starts with' then ' like ' + CHAR(39) + (select Conidition02 from Discount where id = @id) + '%' + CHAR(39)  else ";
                ret += "CaSE WHEN(select Conidition01 from Discount where id = @id) like 'Contains' then ' like '+ CHAR(39) + '%' + (select Conidition02 from Discount where id = @id) + '%'+ CHAR(39)  else ";
                ret += "CaSE WHEN(select Conidition01 from Discount where id = @id) like 'Ends with' then ' like '+ CHAR(39) + '%' + (select Conidition02 from Discount where id = @id) + CHAR(39) else '= '+ CHAR(39)+ CHAR(39) end end end ";
                ret += "+ CaSE WHEN (select type from Discount where id = @id) like 'Exclusive'  then ' and t.id not in (select employeeId from #Discountavail where fammid is null )' else '' end ";
                ret += "insert into #DiscountStatement(stmt) ";
                ret += "select @sqlStmt ";

                ret += "SET @sqlStmt = 'insert into #Discountavail (employeeId, fammid,percnt) Select null ,t.id, ' + (select PercntValue from Discount where id = @id) +  '  from ' + @Table02 + ' as t inner join employee e on t.employeeId = e.id where isnull(t.' + (select ApplyFields from Discount where id = @id)  ";
                ret += "+ ',e. ' + (select ApplyFields from Discount where id = @id) + ') ' + ";
                ret += "CaSE WHEN (select Conidition01 from Discount where id = @id) like 'Starts with' then ' like ' + CHAR(39) + (select Conidition02 from Discount where id = @id) + '%' + CHAR(39)  else ";
                ret += "CaSE WHEN(select Conidition01 from Discount where id = @id) like 'Contains' then ' like '+ CHAR(39) + '%' + (select Conidition02 from Discount where id = @id) + '%'+ CHAR(39)  else ";
                ret += "CaSE WHEN(select Conidition01 from Discount where id = @id) like 'Ends with' then ' like '+ CHAR(39) + '%' + (select Conidition02 from Discount where id = @id) + CHAR(39) else '= '+ CHAR(39)+ CHAR(39) end end end ";
                ret += "+ CaSE WHEN (select type from Discount where id = @id) like 'Exclusive' then ' and t.id not in (select fammid from #Discountavail where fammid is not null )' else '' end ";
                ret += "insert into #DiscountStatement(stmt) ";
                ret += "select @sqlStmt ";

                ret += "FETCH NEXT FROM db_cursor INTO @id ";
                ret += "END ";
                ret += "CLOSE db_cursor ";
                ret += "DEALLOCATE db_cursor ";

                ret += "Declare @sqlStmtn as nvarchar(2000) ";
                ret += "DECLARE db_cursor CURSOR FOR ";
                ret += "Select stmt ";
                ret += "From #DiscountStatement ";
                ret += "OPEN db_cursor ";
                ret += "FETCH NEXT FROM db_cursor INTO @sqlStmtn ";
                ret += "WHILE @@FETCH_STATUS = 0  ";
                ret += "BEGIN ";
                ret += "eXEC sp_executesql @sqlStmtn ";
                ret += "FETCH NEXT FROM db_cursor INTO @sqlStmtn  ";
                ret += "END ";
                ret += "CLOSE db_cursor ";
                ret += "DEALLOCATE db_cursor ";

                ret += "insert #ret(empID, famid , LastName, FirstName  , BaseCost , DscntPrcnt, yearlycost , perpay ) ";
                ret += "Select emp.id , 0  , emp.LastName, emp.FirstName  , @EmpHealthCost  ,isnull(ed.percnt,0), Round(((@EmpHealthCost * (100 - isnull(ed.percnt,0)) )/100) ,4,0), ";
                ret += "Round((((@EmpHealthCost * (100 - isnull(ed.percnt,0)) )/100)/@PayCheckPerYear) ,4,0) ";
                ret += "from Employee emp ";
                ret += "left join  ( ";
                ret += "Select employeeId, fammid, percnt ";
                ret += "From #Discountavail ) as ed on ";
                ret += "emp.id = ed.employeeId and ed.fammid is null ";


                ret += "insert #ret(empID, famid , LastName, FirstName  , BaseCost , DscntPrcnt, yearlycost , perpay ) ";
                ret += "Select fm.employeeID, fm.id, isnull(fm.LastName, e.lastName) , fm.FirstName , @FamMemHealthCost ,  isnull(fmd.percnt,0) , Round(((@FamMemHealthCost * (100 - isnull(fmd.percnt,0)) )/100) ,4,0), ";
                ret += "Round((((@FamMemHealthCost * (100 - isnull(fmd.percnt,0)) )/100)/@PayCheckPerYear),4,0) ";
                ret += "from FamilyMember fm ";
                ret += "left join  ( ";
                ret += "Select employeeId, fammid, percnt ";
                ret += "From #Discountavail ) as fmd on ";
                ret += "fm.id = fmd.fammid and fmd.fammid is not null ";
                ret += "inner join Employee e on ";
                ret += "fm.employeeID = e.id ";


                ret += "Create table  #empInf(empid int,  LastName nvarchar(500), FirstName nvarchar(500) , AddtlFamily int, AnnualPay money, annualBaseCost money, annualAdjCost money, annualBeforeTax  money, normalPay  money, perPayPeriodCost money, perPayBeforeTax money) ";
                ret += "Insert #empInf(empid, LastName,FirstName , AddtlFamily , AnnualPay , annualBaseCost, annualAdjCost , annualBeforeTax  , normalPay  , perPayPeriodCost , perPayBeforeTax ) ";
                ret += "Select emp.id, emp.LastName, emp.FirstName, count(famid) -1   ,@PayCheckPerYear * @StandardPay,   sum(baseCost), sum(yearlyCost) , ";
                ret += "(@PayCheckPerYear * @StandardPay) - sum(yearlyCost) , ";
                ret += "@StandardPay  , Round(sum(perPay),2,0) , ";
                ret += "@StandardPay - Round(sum(perPay),2,0)  ";
                ret += "From #ret ";
                ret += "Inner join Employee emp on ";
                ret += "#ret.empID = emp.id ";
                ret += "Group By emp.id, emp.LastName, emp.FirstName ";
                ret += "Select * from #empInf ";
                
                return ret;
            }
            else if (procName.Equals("getEmpInf", StringComparison.InvariantCultureIgnoreCase))
            {
                string ret = "";
                ret += "getEmpInf(@empID as int) ";
                ret += "as ";
               // ret += "SET NOCOUNT ON; ";
              //  ret += "SET ANSI_NULLS ON; ";
                ret += "Create table #DiscountStatement(stmt nvarchar(2000) ) ";
                ret += "Create table #Discountavail (employeeId int null, fammid int null, percnt int null, DiscountName nvarchar(500), Type nvarchar(500)) ";
                ret += "Create table #ret(empID int, famid int, LastName nvarchar(500), FirstName nvarchar(500) , BaseCost Money, DscntPrcnt int, yearlycost money, perpay money, DiscountName nvarchar(500), Type nvarchar(500)) ";
                ret += "Declare @Table01 as nvarchar(30) ";
                ret += "Declare @table02 as nvarchar(30) ";
                ret += "DECLARE @PayCheckPerYear as int ";
                ret += "DECLARE @StandardPay as Money ";
                ret += "DECLARE @EmpHealthCost as Money ";
                ret += "Declare @FamMemHealthCost as Money ";

                ret += "Set @PayCheckPerYear = (Select OptionsValue from Options where OptionsNum like 'PayChecksPerYear') ";
                ret += "Set @StandardPay = (Select OptionsValue from Options where OptionsNum like 'StandardPayCheck') ";
                ret += "Set @EmpHealthCost = (Select OptionsValue from Options where OptionsNum like 'EmployeeYearlyHealth') ";
                ret += "Set @FamMemHealthCost = (Select OptionsValue from Options where OptionsNum like 'EFamMemYearlyHealth') ";
                ret += "Set @Table01 = 'Employee' ";
                ret += "Set @Table02 = 'FamilyMember' ";
                ret += "Declare @id as int ";

                ret += "DECLARE db_cursor CURSOR FOR ";
                ret += "Select id ";
                ret += "From Discount order by Type DESC, isnull(PercntValue, 0) desc, Name asc ";
                ret += "OPEN db_cursor  ";
                ret += "FETCH NEXT FROM db_cursor INTO @id ";
                ret += "WHILE @@FETCH_STATUS = 0  ";
                ret += "BEGIN  ";
                ret += "Declare @sqlStmt as nvarchar(3000) ";

                ret += "SET @sqlStmt = 'insert into #Discountavail (employeeId, fammid,percnt, DiscountName, Type ) Select id,null, ' + (select PercntValue from Discount where id = @id) + ' , '+ CHAR(39)  + (select Name from Discount where id = @id) + CHAR(39) + ' , '+ CHAR(39)  + (select Type from Discount where id = @id) + CHAR(39) +  '   from ' + @Table01 + ' as t where ' + (select ApplyFields from Discount where id = @id) + ' '  + ";
                ret += "CaSE WHEN (select Conidition01 from Discount where id = @id)like 'Starts with' then ' like ' + CHAR(39) + (select Conidition02 from Discount where id = @id) + '%' + CHAR(39)  else ";
                ret += "CaSE WHEN(select Conidition01 from Discount where id = @id) like 'Contains' then ' like '+ CHAR(39) + '%' + (select Conidition02 from Discount where id = @id) + '%'+ CHAR(39)  else ";
                ret += "CaSE WHEN(select Conidition01 from Discount where id = @id) like 'Ends with' then ' like '+ CHAR(39) + '%' + (select Conidition02 from Discount where id = @id) + CHAR(39) else '= '+ CHAR(39)+ CHAR(39) end end end ";
                ret += "+ CaSE WHEN (select type from Discount where id = @id) like 'Exclusive'  then ' and t.id not in (select employeeId from #Discountavail where fammid is null )' else '' end ";
                ret += "insert into #DiscountStatement(stmt) ";
                ret += "select @sqlStmt ";

                ret += "SET @sqlStmt = 'insert into #Discountavail (employeeId, fammid,percnt,DiscountName,Type) Select null ,t.id, ' + (select PercntValue from Discount where id = @id)  + ' , ' + CHAR(39)  + (select Name from Discount where id = @id)+ CHAR(39)  + ' , '+ CHAR(39)  + (select Type from Discount where id = @id) + CHAR(39)  +  '  from ' + @Table02 + ' as t inner join employee e on t.employeeId = e.id where isnull(t.' + (select ApplyFields from Discount where id = @id)  ";
                ret += "+ ',e. ' + (select ApplyFields from Discount where id = @id) + ') ' + ";
                ret += "CaSE WHEN (select Conidition01 from Discount where id = @id) like 'Starts with' then ' like ' + CHAR(39) + (select Conidition02 from Discount where id = @id) + '%' + CHAR(39)  else ";
                ret += "CaSE WHEN(select Conidition01 from Discount where id = @id) like 'Contains' then ' like '+ CHAR(39) + '%' + (select Conidition02 from Discount where id = @id) + '%'+ CHAR(39)  else ";
                ret += " CaSE WHEN(select Conidition01 from Discount where id = @id) like 'Ends with' then ' like '+ CHAR(39) + '%' + (select Conidition02 from Discount where id = @id) + CHAR(39) else '= '+ CHAR(39)+ CHAR(39) end end end ";
                ret += "+ CaSE WHEN (select type from Discount where id = @id) like 'Exclusive' then ' and t.id not in (select fammid from #Discountavail where fammid is not null )' else '' end ";
                ret += "insert into #DiscountStatement(stmt) ";
                ret += "select @sqlStmt ";

                ret += "FETCH NEXT FROM db_cursor INTO @id ";
                ret += "END ";
                ret += "CLOSE db_cursor ";
                ret += "DEALLOCATE db_cursor ";

                ret += "Declare @sqlStmtn as nvarchar(2000) ";
                ret += "DECLARE db_cursor CURSOR FOR ";
                ret += "Select stmt ";
                ret += "From #DiscountStatement ";
                ret += "OPEN db_cursor ";
                ret += "FETCH NEXT FROM db_cursor INTO @sqlStmtn ";
                ret += "WHILE @@FETCH_STATUS = 0  ";
                ret += "BEGIN ";
               // ret += " Declare @sqlStmt2 as nvarchar(4000) ";
               // ret += " set @sqlStmt2 = @sqlStmtn ";
                ret += "eXEC sp_executesql @sqlStmtn ";
                ret += "FETCH NEXT FROM db_cursor INTO @sqlStmtn  ";
                ret += "END ";
                ret += "CLOSE db_cursor ";
                ret += "DEALLOCATE db_cursor ";

                ret += "insert #ret(empID, famid , LastName, FirstName  , BaseCost , DscntPrcnt, yearlycost , perpay, DiscountName, Type ) ";
                ret += "Select emp.id , 0  , emp.LastName, emp.FirstName  , @EmpHealthCost  ,isnull(ed.percnt,0), Round(((@EmpHealthCost * (100 - isnull(ed.percnt,0)) )/100) ,4,0), ";
                ret += "Round((((@EmpHealthCost * (100 - isnull(ed.percnt,0)) )/100)/@PayCheckPerYear) ,4,0), isnull(ed.DiscountName,'') , isnull(ed.Type,'')  ";
                ret += "from Employee emp ";
                ret += "left join  ( ";
                ret += "Select employeeId, fammid, percnt, DiscountName, Type  ";
                ret += "From #Discountavail ) as ed on ";
                ret += "emp.id = ed.employeeId and ed.fammid is null ";
                ret += "where emp.id = @empID ";

                ret += "insert #ret(empID, famid , LastName, FirstName  , BaseCost , DscntPrcnt, yearlycost , perpay, DiscountName, Type ) ";
                ret += "Select fm.employeeID, fm.id, isnull(fm.LastName, e.lastName) , fm.FirstName , @FamMemHealthCost ,  isnull(fmd.percnt,0) , Round(((@FamMemHealthCost * (100 - isnull(fmd.percnt,0)) )/100) ,4,0), ";
                ret += "Round((((@FamMemHealthCost * (100 - isnull(fmd.percnt,0)) )/100)/@PayCheckPerYear),4,0), isnull(fmd.DiscountName,''),  isnull(fmd.Type,'')  ";
                ret += "from FamilyMember fm ";
                ret += "left join  ( ";
                ret += "Select employeeId, fammid, percnt, DiscountName, Type  ";
                ret += "From #Discountavail ) as fmd on ";
                ret += "fm.id = fmd.fammid and fmd.fammid is not null ";
                ret += "inner join Employee e on ";
                ret += "fm.employeeID = e.id ";
                ret += "where fm.employeeID = @empID ";
                ret += "and e.id = @empID ";

                ret += "Select * from #ret ";
                ret += "Order By famid ";

                return ret;
            }



            else
            {
                return "";
            }
        }

    }
}
