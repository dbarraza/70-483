using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace _11_DataConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            //SqlConnection
            string connectionString = "Data Source=CH-STG-NB031;Initial Catalog=SiGeCap;Persist Security Info=True;User ID=sa;Password=4rk4n0!@.";
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();

            #region Connected Layer

            Console.WriteLine("=============  Working in Connected Layer ==============\n");

            //SqlCommand
            var random = new Random();
            var randomId = random.Next(3, 100);
            string insertQuery = $"insert into sigecap.dim_adherente values ({randomId},1,1,1,1,1)";
            SqlCommand command = new SqlCommand(insertQuery, con);

            //ExecuteNonQuery
            //int resInsert = command.ExecuteNonQuery(); //retorna la cantidad de filas afectadas
            //Console.WriteLine(resInsert);


            //ExcecuteScalar
            string countQuery = "select count(*) from sigecap.dim_adherente";
            command = new SqlCommand(countQuery, con);
            var resCount = command.ExecuteScalar();
            Console.WriteLine(resCount);


            //ExecuteReader (SqlDataReader)
            string selectQuery = "select * from sigecap.dim_adherente";
            command = new SqlCommand(selectQuery, con);
            SqlDataReader reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var adherenteId = int.Parse(reader[0].ToString());
                    Console.WriteLine(adherenteId);
                }
            }
            reader.Close();

            //ExecuteXMLReader (XmlReader)
            string selectXmlQuery = "select * from sigecap.dim_adherente for xml auto";
            command.CommandText = selectXmlQuery;

            var xmlReader = command.ExecuteXmlReader();
            while (xmlReader.Read())
            {
                while (xmlReader.MoveToNextAttribute())
                {
                    Console.Write($"{xmlReader.Name}: - {xmlReader.Value} ");
                }
                Console.WriteLine();
            }

            #endregion


            #region Disconnected Layer

            Console.WriteLine("\n=============  Working in Disconnected Layer ==============\n");

            string selectQry = "select * from sigecap.dim_adherente";
            var sqlAdapter = new SqlDataAdapter(selectQry, con);

            //Select in disconnected enviroment
            DataTable tbl = new DataTable();
            sqlAdapter.Fill(tbl);
            //con.Close(); //yo podría cerrar la conexión en este punto, ya que es un ambiente desconectado
            foreach (DataRow row in tbl.Rows)
            {
                Console.WriteLine(row[0] + " - " + row[1]);
            }

            Console.WriteLine("\nAdd new Rows");
            //Insert into disconnected enviroment
            DataRow newRow = tbl.NewRow();
            newRow["IdAdherente"] = 101;
            newRow["RutAdherente"] = 1;
            newRow["NombreAdherente"] = 1;
            newRow["FonoAdherente"] = 1;
            newRow["SegmentoAdherente"] = 1;
            newRow["EmailAdherente"] = 1;

            tbl.Rows.Add(newRow);

            foreach (DataRow row in tbl.Rows)
            {
                Console.WriteLine(row[0] + " - " + row[1]);
            }


            #endregion

            //Cerrar conexion
            con.Close();

            #region Consume XML

            //XML
            string xml = @"<?xml version='1.0' encoding='utf - 16'?>
                            <Student Id = '1'>   
                                <Name> Hasan Asam Yamil Amil</Name>
                            </Student>
                            ";

            //Read a xml as a string
            var stringReader = new StringReader(xml);

            //ReaderXML
            var xReader = XmlReader.Create(stringReader);

            while (xReader.Read())//Read the entire xml
            {
                Console.WriteLine(xReader.Value);
            }

            //USING XmlDocument
            Console.WriteLine("\nUsing XmlDocument");
            var xmlDocument = new XmlDocument();

            //foreach (XmlNode nodo in xmlDocument.DocumentElement)
            //{
            //    Console.WriteLine(nodo.InnerText);
            //}


            //Using XmlWriter
            Console.WriteLine("\nUsing XmlWrite");
            var stringWriter = new StringWriter();

            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { Indent = true }))
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteStartElement("Student");
                xmlWriter.WriteAttributeString("Id","1");
                xmlWriter.WriteElementString("Name", "Daniel Barraza");
                xmlWriter.WriteEndElement();
            }

            Console.WriteLine(stringWriter.ToString());

            #endregion

            #region Consume JSON

            #endregion

        }
    }
}
