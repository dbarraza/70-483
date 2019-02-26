using System;
using System.Threading;

namespace _08_Threads
{
    class Program
    {
        static bool stop = false;

        [ThreadStatic]
        static int counter = 0;

        static void Main(string[] args)
        {
            #region Métodos void y con parámetros
            /*
             * Por defecto un programa tiene un hilo principal
             * Un hilo solo puede manejar un método
             * Se puede instanciar un hilo mediante 4 formas:
             *      - Usando métodos anónimos
             *      - Lambda (que es un tipo de función anónima)
             *      - Usando la clase Thread
             *      - Usando la clase ThreadStart
            */

            /*
            //Métodos anónimos
            var t1 = new Thread(delegate() {Console.WriteLine("Hilo usando  método anónimo"); });
            
            //Métodos lambda
            var t2 = new Thread(() => Console.WriteLine("Hilo usando lambda"));

            //Usando la clase Thread
            var t3 = new Thread(ImprimirMensaje2); //Recibe un delegado como parámetro

            //Usando la clase ThreadStart
            var t4 = new Thread(new ThreadStart(ImprimirMensaje)); //Recibe un delegado como parámetro

            //Usando método parametrizado
            var t5 = new Thread(new ParameterizedThreadStart(ImprimirWithParameter));

            //Usando método parametrizado y con lambda
            var t6 = new Thread(new ParameterizedThreadStart((object str) => Console.WriteLine(str.ToString())));

            t1.Start();
            
            t2.Start();

            t3.IsBackground = true; //establece la ejecución de este hilo para que se ejecute por background. (el hilo principal no esperará que este hilo termine)
            t3.Start();

            t4.Start();
            t4.Join(); //bloquea el hilo principal hasta que este hilo termine

            t5.Start("Mensaje de prueba");

            t6.Start("Método parametrizado con lambda expression");

            t5.Join();
            t6.Join();
            */
            #endregion

            #region ThreadPriority
            /*
            Console.WriteLine("\n====== Thread Priority  =======\n");

            var tNormal = new Thread(PriorityMethod)
            {
                Name = "Thread Normal",
                Priority = ThreadPriority.Normal
            };

            var tLow = new Thread(PriorityMethod)
            {
                Name = "Thread Low",
                Priority = ThreadPriority.Lowest
            };

            var tHigh = new Thread(PriorityMethod)
            {
                Name = "Thread High",
                Priority = ThreadPriority.Highest
            };

            tNormal.Start();
            tLow.Start();
            tHigh.Start();

            Thread.Sleep(5000);
            stop = true;
            */
            #endregion

            #region ThreadPriority
            /*
            var thread1 = new Thread(() =>
            {
                for(int i = 0; i < 10; i++)
                {
                    counter++;
                    Console.WriteLine($"Thread 1: {counter}");
                }
            });

            thread1.Start();
            thread1.Join();

            var thread2 = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    counter++;
                    Console.WriteLine($"Thread 2: {counter}");
                }
            });
            
            thread2.Start();            
            thread2.Join();
            */
            #endregion

            #region ThreadPool
            ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc),"Texto stateInfo");

            ThreadPool.QueueUserWorkItem((object s) =>
            {
                Console.WriteLine($"Queue for object s: {s}");
            },"object s is a string");

            Console.WriteLine("Presione una tecla para terminar");
            Console.Read();

            #endregion
        }

        static void ImprimirMensaje()
        {
            Thread.Sleep(5000); //Se detiene el hilo principal duante 5 segundos
            Console.WriteLine("Metodo ImprimirMensaje");
        }

        static void ImprimirMensaje2()
        {
            Thread.Sleep(60000); //Se detiene el hilo principal duante 60 segundos
            Console.WriteLine("Metodo ImprimirMensaje for Background Thread");
        }

        static void ImprimirWithParameter(object str)
        {
            Console.WriteLine(str);
        }

        static void PriorityMethod()
        {
            //Get the name of current thread
            string threadName = Thread.CurrentThread.Name;

            //Get the priority 
            string threadPriority = Thread.CurrentThread.Priority.ToString();

            uint count = 0;

            while(!stop)
            {
                count++;
            }

            Console.WriteLine($"Hijo {threadName} con prioridad {threadPriority}, llegó a {count}");
        }

        static void ThreadProc(Object stateInfo)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"Hello from the thread pool. stateInfo {stateInfo.ToString()}");
        }
    }
}
