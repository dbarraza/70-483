using System;
using System.Threading.Tasks;
using System.Threading;


namespace _08_Tasks3
{
    class Program
    {
        [ThreadStatic]
        static bool debeSumar;

        static void Main(string[] args)
        {
            #region Synchronization Variables in task

            Console.WriteLine("===================  Synchronization Variables in task  ===============");

            var random = new Random();          

            //Lock
            Console.WriteLine("         LOCK");

            var claseLock = new ClaseLock(1000);
            var taskLock = new Task[17];
            for(int i = 0; i < 17; i++)
            {   
                taskLock[i] = Task.Run(() =>
                {
                    debeSumar = random.NextDouble() >= 0.5 ? true : false;
                    var monto = random.Next(1, 100);

                    if (debeSumar)
                        claseLock.Sumar(monto);
                    else
                        claseLock.Restar(monto);
                });
            }
            Task.WaitAll(taskLock);
            Console.Read();

            //Monitor
            Console.WriteLine("\n         MONITOR");
            var claseMonitor = new ClaseMonitor(1000);
            var taskMonitor = new Task[17];
            for (int i = 0; i < 17; i++)
            {
                debeSumar = random.NextDouble() >= 0.5 ? true : false;
                var monto = random.Next(1, 10);
                taskMonitor[i] = Task.Run(() =>
                {
                    if (debeSumar)
                        claseMonitor.Sumar(monto);
                    else
                        claseMonitor.Restar(monto);
                });
            }
            Task.WaitAll(taskMonitor);
            Console.Read();

            //Interlock
            Console.WriteLine("\n         INTERELOCK");
            var claseInterlock = new ClaseInterlock(1000);
            var taskInterlock = new Task[17];
            for (int i = 0; i < 17; i++)
            {
                debeSumar = random.NextDouble() >= 0.5 ? true : false;
                var monto = random.Next(1, 100);
                taskInterlock[i] = Task.Run(() =>
                {
                    if (debeSumar)
                        claseInterlock.Sumar(monto);
                    else
                        claseInterlock.Restar(monto);
                });
            }
            Task.WaitAll(taskInterlock);
            Console.Read();

            #endregion

            #region Cancelattion Token

            Console.WriteLine("============  Cancelattion Token   ==================");

            var source = new CancellationTokenSource();
            var token = source.Token;

            Task.Run(() =>
            {
                Console.WriteLine("Executing task");
                while (true)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("*");

                    if (token.IsCancellationRequested)
                        return;
                }
            });

            Thread.Sleep(5000);
            source.Cancel();

            #endregion

            Console.WriteLine("Terminado hilo principal");
        }
    }
}
