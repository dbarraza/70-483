using System;

namespace _09_RethrowingEx
{
    class Program
    {
        static void Main(string[] args)
        {
            //Rethrow with throw
            Console.WriteLine("=======================    Rethrow with throw    ==================");
            try
            {
                RethrowWithThrow();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException?.Message);
            }


            //Throw with InnerException
            Console.WriteLine("=======================    Throw with InnerException    ==================");
            try
            {
                RethrowWithInnerException();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException?.Message);
            }

            //Throw with InnerException
            Console.WriteLine("=======================    Throw with New without InnerException    ==================");
            try
            {
                NewException();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                // Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException?.Message);
            }

            //Custom Exception
            Console.WriteLine("======================         Custom Exception      ===================================");
            try
            {
                CustomException();
            }
            catch(MyCustomException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex?.InnerException?.Message);
            }
        }

        static void RethrowWithThrow()
        {
            try
            {
                ThrowException();
            }
            catch(NullReferenceException ex)
            {
                throw;
            }
        }

        static void RethrowWithInnerException()
        {
            try
            {
                ThrowException();
            }
            catch(NullReferenceException ex)
            {
                throw new Exception("Ha ocurrido un error en RethrowWithInnerException",ex);
            }
        }

        static void NewException()
        {
            try
            {
                ThrowException();
            }
            catch(NullReferenceException ex)
            {
                throw new Exception();
            }
        }

        static void CustomException()
        {
            try
            {
                ThrowException();
            }
            catch(NullReferenceException ex)
            {
                throw new MyCustomException("Error en CustomException", ex);
            }
        }

        static void ThrowException()
        {
            throw new NullReferenceException("This will be an inner exception");
        }
    }
}
