using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Threading;

namespace _06_Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            var strList = new List<string> 
            {
                "Uno",
                "Dos",
                "Tres",
                "Cuatro",
                "Cinco"
            };

            /*
            Una Query LINQ se componen de 3 partes
            1.- Obtener el DataSource
            2.- Crear la Query
            3.- Ejecutar la Query
            */

            #region Deferred vs Inmediate

            var inmediateQuery = (from e in strList
                                 where e.Contains("o")
                                 select e).ToList();
            ImprimirListado(inmediateQuery,"Inmediate");

            var deferredQuery = strList.Where(e => e.Contains("o"));
            strList.Add("Ocho");
            ImprimirListado(deferredQuery,"Deferred");

            #endregion

            #region LINQ To SQL

            //Crear un XML
            var xmlDocument = new XElement("Persona");
            xmlDocument.Add(new XElement("Name","NombrePersona"));
            xmlDocument.Add(new XElement("Age","31"));
            xmlDocument.Save("Persona.xml");
            Console.WriteLine("XML Creado");
            Thread.Sleep(2000);            

            //Leer y actualizar un XML
            var fileReader = new FileStream("Persona.xml", FileMode.Open);
            var strReaderXML = new StreamReader(fileReader);
            var xDocument = XDocument.Parse(strReaderXML.ReadToEnd());


            var elementAge = (from e in xDocument.Elements()    //Elements obtiene todos los Nodos hijos de xDocument (XElement);
                            where e.Element("Name").Value == "NombrePersona"
                            select e.Element("Age")).FirstOrDefault();
            elementAge.SetValue("35");
            xDocument.Save("PersonaUpdated.xml");
            Console.WriteLine("XML Actualizado");
            fileReader.Dispose();
            
            
            //Leer y eliminar un elemento del XML
            using (var fileReader2 = new FileStream("Persona.xml", FileMode.Open))
            {
                var strReaderXML2 = new StreamReader(fileReader2);
                var xDocument2 = XDocument.Parse(strReaderXML2.ReadToEnd());
                xDocument2.Elements().Where(e => e.Element("Name").Value == "NombrePersona").Select(e => e.Element("Name")).Remove();
                xDocument2.Save("PersonaDeleted.xml");
                Console.WriteLine("XML Elemento Eliminado");
            }//call automatically filreReader2.Dispose()

            #endregion

            #region Pregunta de Examen 70-483
            /*
            You are developing an application. The application calls a method that returns an array of integers named employeeIds. 
            You define an integer variable named employeeIdToRemove and assign a value to it. You declare an array named filteredEmployeeIds.

            You have the following rerquirements:
            - Remove duplicate integers from the employeeIds array
            - Sort the array in order from the highest value to the lowest value
            - Remove the integer value stored in the employeeIdToRemove variable from the employeeIds array.

            You need to create a LINQ query to meet the requirements.
             */

            var employeeIds = new int [] { 1, 2 ,3, 3 , 1, 4 ,5 ,6 ,7, 8, 1};
            var employeeIdToReove = 3;

            int[] filteredEmployeeIds = employeeIds.Where(value => value != employeeIdToReove).Distinct().OrderByDescending(value => value).ToArray();

            ImprimirListado(employeeIds,"De Enteros original");
            ImprimirListado(filteredEmployeeIds,"De Enteros no duplicados");

            #endregion

            #region Pregunta de Examen 70-483
            #if (DEBUG)                //Esta es la opción correcta para el Examen
                Console.WriteLine("Modo DEGUB");
            #else
                Console.WriteLine("Modo RELEASE");
            #endif

            #if (DEBUG)               //Esta opción igual es valida, pero no es aceptada como correcta para el Examen
                Console.WriteLine("Modo DEBUG");
#elif (RELEASE)
                Console.WriteLine("Modo RELEASE");
#endif
            #endregion

            #region Pregunta de Examen 70-483
            /*
             You are developing an application that will populate an extensive XML tree from a Microsoft SQL Server 2008 R2.

            You are creating a XML tree. The solution must meet the following requirements:
            - Minimize memory requirements
            - Maximize data processing speed

             */

            var contactDataSource = new List<Contact>
            {
                new Contact(1,"Nom1","Appe1"),
                new Contact(2,"Nom2","Appe2"),
                new Contact(3,"Nom3","Appe3"),
                new Contact(4,"Nom4","Appe4"),
                new Contact(5,"Nom5","Appe5"),
                new Contact(6,"Nom6","Appe6"),
                new Contact(7,"Nom7","Appe7"),
                new Contact(8,"Nom8","Appe8"),
                new Contact(9,"Nom9","Appe9"),
            };

            XNamespace ew = "ContactList";
            XElement root = new XElement(ew + "Root");
            Console.WriteLine(root);

            XElement contacts = new XElement("Contacts",  //<=== Esta línea es EXAM dice XAttribute, lo que es un error y no genera un XML valido

                from c in contactDataSource
                orderby c.ContactId
                select new XElement("contact",
                    new XAttribute("contactId",c.ContactId),
                    new XElement("firstName", c.FirstName),
                    new XElement("lastName", c.LastName))
                );

            root.Add(contacts);
            root.Save("Contacts.xml");
            #endregion
        }

        public static void ImprimirListado(IEnumerable<string> listado, string tipo)
        {
            Console.WriteLine($"===========  Imprimiendo Listado {tipo} ===============");
            foreach (var item in listado)
            {
                Console.WriteLine(item);
            }
        }

        public static void ImprimirListado(IEnumerable<int> listado, string tipo)
        {
            Console.WriteLine($"===========  Imprimiendo Listado {tipo} ===============");
            foreach (var item in listado)
            {
                Console.WriteLine(item);
            }
        }
    }
}
