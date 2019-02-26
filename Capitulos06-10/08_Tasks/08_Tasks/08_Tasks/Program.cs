using System;
using System.Threading.Tasks;
using System.Threading;

namespace _08_Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Introduction
            /*
             Task are better than Threads becuase:
             - They let you know when the jobs is finalized and allows to you return a result
             - It gives a better flow control
             - It gives the ability of chain multiples tasks togheter (using ContinueWith method)
             */
            #endregion

            #region Task that doesn't return a value

            Console.WriteLine("===============   Task void  ==============");

            //.NET 4 not optimal version (using lambda)
            var task1 = new Task(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Void Task .NET 4 not optimal version");
            });
            task1.Start();

            //.NET 4 optimal version (using Action delegate)
            var actionForTask2 = new Action(Net4OptionalVersion);
            var task2 = Task.Factory.StartNew(actionForTask2); //StarNew create and initialize the task, cost less than start method

            //.NET 4.5 more optimal version (using anonimous method)
            var task3 = Task.Run(delegate() 
            {
                Thread.Sleep(2000);
                Console.WriteLine("Void Task .NET 4.5 more optimal version");
            });

            //.NET 4.5 more optimal version (using Action<string> delegate)
            var actionForTask4 = new Action<string>(PrintMessage);
            var task4 = Task.Run(() => 
            {
                actionForTask4("Void Task .NET 4.5 more optimal version (using Action<string> delegate)");
            });

            //Esperar la ejecución de todas las tareas
            task1.Wait();
            task2.Wait();
            task3.Wait();
            task4.Wait();
            #endregion

            #region Task that return value

            Console.WriteLine();
            Console.WriteLine("===============   Task with a return value   ==============");

            //string Task .NET 4 not optimal version (using lambda)
            var task5 = new Task<string>(() =>
            {
                Thread.Sleep(2000);
                return "string Task .NET 4 not optimal version (using lambda)";
            });
            task5.Start();
            //task5.Wait();   It's not necessary because the Result force to wait to the task to finish
            Console.WriteLine(task5.Result);

            //string Task .NET 4 optimal version (using anonimous method)
            var task6 = Task<string>.Factory.StartNew(delegate ()
            {
                Thread.Sleep(2000);
                Console.WriteLine("string Task .NET 4 optimal version (using anonimous method)");
                return "string Task .NET 4 optimal version (using anonimous method)";
            });

            //string Task .NET 4.5 more optimal version (using anonimous method)
            var task7 = Task.Run(delegate ()
            {
                Thread.Sleep(2000);
                Console.WriteLine("string Task .NET 4.5 more optimal version (using anonimous method)");
                return "string Task .NET 4.5 more optimal version (using anonimous method)";
            });

            //string .NET 4.5 more optimal version (using Function<string> delegate)
            var funcForTask8 = new Func<string>(FuncForTask8);
            var task8 = Task<string>.Run(new Func<string>(funcForTask8));
            Console.WriteLine(task8.Result);


            //string .NET 4.5 more optional version (using Func<int,string> delegate)
            var funcForTask9 = new Func<int, string>(FuncForTask9);
            var task9 = Task<string>.Run(new Func<string>(()=> funcForTask9(10)));
            Console.WriteLine(task9.Result);

            //WAIT FOR ALL
            var taskArg = new Task[] { task6, task7 };

            #endregion




            Console.WriteLine("\nTerminando Hilo Principal");

        }

        public static void Net4OptionalVersion()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Void Task .NET 4 optimal version");
        }

        public static void PrintMessage(string str)
        {
            Thread.Sleep(2000);
            Console.WriteLine(str);
        }

        public static string FuncForTask8()
        {
            Thread.Sleep(2000);
            return $"string Task .NET 4.5 more optimal version(using Function<string> delegate)";
        }

        public static string FuncForTask9(int a)
        {
            Thread.Sleep(2000);
            return $"string Task .NET 4.5 with parameter Task. Parameter value: {a.ToString()}";
        }
    }
}
