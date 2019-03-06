using System;
using static System.Console;

namespace _09_RethrowingEx
{
    public class MyCustomException : Exception
    {
        public MyCustomException(string message, Exception ex) : base (message,ex)
        {
            WriteLine($"Loggin MyCustomException");
        }
    }
}