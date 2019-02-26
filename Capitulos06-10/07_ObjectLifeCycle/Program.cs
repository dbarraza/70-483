using System;
using System.IO;

namespace _07_ObjectLifeCycle
{
    class Program
    {
        static void Main(string[] args)
        {
            //El tiempo de vida de un objeto transcurre desde su creación hasta su destrucción 
            var miClase = new MiClase();
            try
            {
                Console.WriteLine("Intendando leer el archivo");
                var fileContent = miClase.Read("PlainText.txt");
                Console.WriteLine(fileContent);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                //Llamada al método dispose de manera explicita
                miClase.Dispose();
            }
        }
    }

    class MiClase : IDisposable
    {
        public StreamReader streamReader;

        public void Dispose()
        {
            if(streamReader != null)
                streamReader.Dispose();

            Console.WriteLine("Calling Dispose Method and SuppressFinalize");
            GC.SuppressFinalize(this);

        }

        public string Read(string path)
        {
            var str = string.Empty;
            using(var fileStream = new FileStream(path,FileMode.Open))
            {
                streamReader = new StreamReader(fileStream);
                str = streamReader.ReadToEnd();
            }//Llamada al método dispose de manera implicita, cuando termina el bloque using
            return str;
        }
    }
}
