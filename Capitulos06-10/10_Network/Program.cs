using System;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace _10_Network
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region NetWork  
            WebRequest request = WebRequest.Create("http://www.apress.com");   
            Console.WriteLine(request.Headers);
            WebResponse response = request.GetResponse();
            Console.WriteLine(response.Headers);

            StreamReader strReader = new StreamReader(response.GetResponseStream());
            string result = strReader.ReadToEnd();
            // Console.WriteLine(result);
            #endregion


            #region Async and Await with File I/O

            //Write to a File
            WriteToFileAsync();

            //Read From File
            Console.WriteLine("Read From File");
            ReadFromFileAsync();

            var strAsync = GetStringAsync().Result;
            Console.WriteLine(strAsync);
                
            #endregion
        }

        public async static void WriteToFileAsync()
        {
            FileStream file = File.Create("Sample.txt");
            StreamWriter writer = new StreamWriter(file);
            await writer.WriteAsync("Asynchronously Written Data");
            writer.Close();
        }

        public async static void ReadFromFileAsync()
        {
            FileStream readFile = File.Open("Sample.txt", FileMode.Open);
            StreamReader reader = new StreamReader(readFile);
            string res = await reader.ReadToEndAsync();
            Console.WriteLine(res);
        }

        public static Task<string> GetStringAsync()
        {
            return Task.Run(()=>{
                Thread.Sleep(2000);
                return ("String returned from asynchronous task");
            });
        }
    }
}
