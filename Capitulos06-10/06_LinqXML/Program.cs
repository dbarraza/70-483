using System;
using System.Threading;
using System.Xml.Linq;
using System.Linq;
using System.IO;

namespace LinqXML
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //Hilo para crear XML
            var hiloCrearXml = new Thread(CrearXML);
            
            //Hilo para actualizar XML
            var hiloUpdateXml = new Thread(UpdateXML);

            //Hilo para leer XML
            var hiloReadXml = new Thread(new ParameterizedThreadStart(ReadXMLFile));


            hiloCrearXml.Start();
            hiloUpdateXml.Start();
            hiloCrearXml.Join();

            //Contar elementos
            hiloReadXml.Start("Persona.XML");
            hiloReadXml.Join();

            Console.WriteLine("Terminado");

        }

        public static void CrearXML()
        {
            var element = new XElement("Personas");
            for(int i = 0; i < 100; i++)
            {
                var elementPersona = new XElement("Persona");
                elementPersona.Add(new XElement("Nombre","Daniel" + i));
                elementPersona.Add(new XElement("Edad",i));
                elementPersona.Add(new XElement("Direccion","Mi dirección" + i));
                element.Add(elementPersona);
                Thread.Sleep(100);
                Console.WriteLine(i);
            }
            
            element.Save("Persona.xml");
            Console.WriteLine("XML creado");
        }

        public static void UpdateXML()
        {
            var strXml = @"<Persona>
                            <Nombre>Daniel</Nombre>
                            <Edad>21</Edad>
                            <Direccion>Mi dirección</Direccion>
                          </Persona>";

            var document = new XDocument(XDocument.Parse(strXml));
            var nodoEdad = (from p in document.Descendants()
                            where p.Element("Nombre").Value == "Daniel"
                            select p.Element("Edad")).FirstOrDefault();
            nodoEdad.Value = "31";
            nodoEdad.Name = "Age";
            document.Save("PersonaUpdated.xml");

            Console.WriteLine("XML actualizado");

            
        }

        public static void ReadXMLFile(object xml)
        {   
            var file = (string)xml;
            var str = new StreamReader(file);
            var strXml = str.ReadToEnd();
            var document = new XDocument(XDocument.Parse(strXml));

            Console.WriteLine("Descendants()");
            Console.WriteLine(document.Descendants().Count());

            Console.WriteLine("Elements()");
            var rootElement = document.Elements().Where(p => p.Name == "Personas").FirstOrDefault();
            Console.WriteLine(rootElement.Elements().Count());

            str.Dispose();
        }
    }
}
