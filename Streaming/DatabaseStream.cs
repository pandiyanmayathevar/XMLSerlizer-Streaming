using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Streaming
{
    [Serializable()]
    [XmlRoot("ArrayRecord")]
    public class RecordClass
    {

        [XmlElement("Header")]
        public Header Head { get; set; }

        [XmlElement("Record")]
        public Employee Record { get; set; }
    }

    [Serializable()]
    public class Header
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Salary { get; set; }
    }

    //[Serializable]
    //[XmlRoot("ArrayRecord")]
    //[XmlType("Record")]
    [Serializable()]
    public class Employee
    {

        [XmlElement("Id")]
        //public int Id { get; set; 
        public string Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Salary")]
        //public decimal Salary { get; set; }
        public string Salary { get; set; }
    }


    public class DatabaseStream : System.IO.Stream
    {
        public IDataReader dtReader;
        Employee Emp;
        RecordClass Recd;
        Header Head;

        //Access the XML Serlize into Bytes
        public byte[] xmlBytes;


        public DatabaseStream(IDataReader dataReader)
        {
            dtReader = dataReader;
        }

    public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void Flush()
        {
        }

        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        public override long Position
        {
            get
            {
                //TODO: Implement Position getter
                throw new NotImplementedException();
            }
            set
            {
                //TODO: Implement Position setter
                throw new NotImplementedException();
            }
        }


        public override int Read(byte[] buffer, int offset, int count)
        {
            //TODO: implement serialization of IDataReader as XML into the buffer

            Emp = new Employee();
            Recd = new RecordClass();
            Head = new Header();
            String XMLString;
            string EmpData = System.Text.Encoding.UTF8.GetString(buffer);

            if (EmpData != "")
            {


                Head.Id = "";
                Head.Name = "";
                Head.Salary = "";

                Recd.Head = Head;


                Emp.Id = EmpData.Split(',')[0].ToString();
                Emp.Name = EmpData.Split(',')[1].ToString();//dataReader.GetValue(1);
                Emp.Salary = EmpData.Split(',')[2].ToString(); //dataReader.GetValue(2);

                Recd.Record = Emp;


                XMLString = SerializeToString(Recd);
                xmlBytes = Encoding.ASCII.GetBytes(XMLString.ToString());


                //XmlRootAttribute xRoot = new XmlRootAttribute();
                //xRoot.ElementName = "CustomRoot";
                //XmlSerializer serializer = new XmlSerializer(typeof(Employee), xRoot);
                //using (TextWriter writer = new StreamWriter(@"C:\Sample.xml"))
                //{
                //    serializer.Serialize(writer, Emp);
                //}

                return 1;
            }
            else
            {
                return 0;
            }

            //throw new NotImplementedException();

        }

        public static string SerializeToString(object obj)
        {
            XmlSerializer serializer = new XmlSerializer(obj.GetType());

            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, obj);

                return writer.ToString();
            }
        }



        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}