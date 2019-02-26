using System;
using System.Threading;
using System.Threading.Tasks;

namespace _08_Tasks2
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Chaining Multiple Task with Continuations

            Console.WriteLine("================   Chaining Multiple Task with Continuations    ==================");

            //Task void
            var task1 = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Tarea 1");
            });
            
            var task2 = task1.ContinueWith((t)=> 
            {
                Thread.Sleep(2000);
                Console.WriteLine("Tarea 2");
            });

            task2.Wait();

            //Task with return value
            var task3 = Task<string>.Run(() =>
            {
                Thread.Sleep(2000);
                var str = "Tarea 3 que retorna un  string, terminada";
                return str;
            }).ContinueWith((t)=>
            {
                Thread.Sleep(2000);
                var str = t.Result;
                str += "\nTarea 3 (encadenada) que retorna un string, terminada";
                return str;
            });

            Console.WriteLine(task3.Result);

            #endregion

            #region Task ContinuationOptions

            Console.WriteLine("\n================   Task ContinuationOptions    ==================");

            //TasContinuationOptions OnRanCompleted
            var task4 = Task.Run(delegate()
            {
                Console.WriteLine("Tarea 4 se está Ejecutando");
            });

            var task4Ok = task4.ContinueWith((t4) => 
            {
               Console.WriteLine("Tarea 4 terminada con exito");
            }, TaskContinuationOptions.NotOnFaulted);

            //TasContinuationOptions OnFaulted
            var task5 = Task.Run(() =>
            {
                Console.WriteLine("Tarea 5 se está Ejecutando");
                throw new Exception("Tarea 5 error");
            });

            var task5Exception = task5.ContinueWith(delegate (Task t4)
            {
                Console.WriteLine("Tarea 5 ha lanzado una excepción no controlada");
            }, TaskContinuationOptions.OnlyOnFaulted);

            task4Ok.Wait();
            task5Exception.Wait();
            #endregion

            #region Nested Task

            Console.WriteLine("\n================   Nested Task   ==================");

            //Detached child Task
            var taskPadre1 = Task.Run(() =>
            {
                Console.WriteLine("Ejecutando Tarea Padre 1");
                var taskHijoNoAnexada = Task.Run(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Ejecutando Tarea hija1 (no anexada)");
                });
                Console.WriteLine("Terminado Tarea Padre 1");
            });

            //Atached child task
            var taskPadre2 = new Task(() =>
            {
                Console.WriteLine("Ejecutando Tarea Padre 2");
                var taskHijoNoAnexada = new Task(() =>
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Ejecutando Tarea hija2 (anexada)");
                }, TaskCreationOptions.AttachedToParent);

                taskHijoNoAnexada.Start();

                Console.WriteLine("Terminando Tarea Padre 2");
            });

            taskPadre2.Start();
            var taskArgs = new Task[] { taskPadre1, taskPadre2 };
            Task.WaitAll(taskArgs);
                        
            #endregion

            Thread.Sleep(2000);
            Console.WriteLine("Terminando hilo principal");
        }
    }
}
