using System;
using System.Data;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Streaming.Tests
{
    [TestClass]
    public class DatabaseStreamFixture
    {
        [TestMethod]
        public void CanSerialize()
        {
            var table = new DataTable("User");
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Salary", typeof(decimal));

            var row = table.NewRow();
            row["Id"] = 1;
            row["Name"] = "John Doe";
            row["Salary"] = 15.5m;

            table.Rows.Add(row);



            var dbStream = new DatabaseStream(table.CreateDataReader());

            String EmpData;

            dbStream.dtReader.Read();

            EmpData = dbStream.dtReader[0].ToString() + "," + dbStream.dtReader[1].ToString() + "," + dbStream.dtReader[2].ToString();

            byte[] bytes = Encoding.ASCII.GetBytes(EmpData.ToString());
            int numBytesToRead = (int)EmpData.Length;
            int numBytesRead = 0;

            dbStream.Read(bytes, numBytesRead, numBytesRead);

            Console.Write("Success");


        }
    }
}
