using ConsumingWebService.MyService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ConsumingWebService
{
    class Program
    {
        static void Main(string[] args)
        {
            SampleServiceSoapClient proxy = new SampleServiceSoapClient();
            int addResult = proxy.Add(5, 10);
            int subtractResult = proxy.Subtract(100, 40);
            Console.WriteLine("Addition Result is: " + addResult);
            Console.WriteLine("Subtraction Result is: " + subtractResult);
            Console.WriteLine(proxy.HelloWorld());


            var insertQuery = string.Empty;
            SqlConnection con = new SqlConnection("");
            using (SqlCommand cmd = new SqlCommand(insertQuery, con))
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
