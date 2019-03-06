using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;

namespace _08_ConcurrentCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Dictionary thread-safe

            Console.WriteLine("============   Concurrent Collection   =================");

            //Si se ocupa Dictionary se genera una excepción: key duplicate
            // var dictionaryNotSafe = new Dictionary<int, int>(); 
            var dictionaryNotSafe = new ConcurrentDictionary<int,int>();

            var tsk1 = Task.Run(()=>{
                for(int i = 0; i < 10; i++)
                {
                    dictionaryNotSafe.TryAdd(i,i+1);
                }
            });

            var tsk2 = Task.Run(()=>{
                for(int i = 0; i < 10; i++)
                {
                    dictionaryNotSafe.TryAdd(i+1,i);
                }
            });

            var taskArg1 = new Task[] { tsk1, tsk2 };
            Task.WaitAll(taskArg1);

            foreach (var item in dictionaryNotSafe)
            {
                Console.WriteLine(item);
            }


            #endregion

            #region Parallel.For

            Console.WriteLine("============   Parallel.For   =================");

            Parallel.For(1,60,(i)=>{
                Console.WriteLine(i);
            });

            #endregion

            #region Parallel.Foreach

            Console.WriteLine("============   Parallel.Foreach     =================");

            int[] data = {1,2,3,4,5};
            Parallel.ForEach(data,(item)=>{
                Console.WriteLine(item);
            });

            #endregion

            #region Parallel.For vs For

            Console.WriteLine("=============  Parallel.For vs For  ===========");

            //Normal for
            var inicioFor = DateTime.Now;
            for (long i = 0; i < int.MaxValue; i++)
            {
                // var x = i;
            }
            var terminoFor = DateTime.Now;

            //Parallel for 
            var inicioParallelFor = DateTime.Now;
            Parallel.For(1,long.MaxValue,(i)=>{
                // var x = i;
            });
            var terminoParallelFor = DateTime.Now;

            var tiempoFor = terminoFor - inicioFor;
            var tiempoForParallel = terminoParallelFor - inicioParallelFor;

            Console.WriteLine($"Tiempo para for normal incio {inicioFor.ToLongTimeString()}. termino {terminoFor.ToLongTimeString()}. Total : {tiempoFor.Milliseconds}");
            Console.WriteLine($"Tiempo para for paralelo incio {inicioParallelFor.ToLongTimeString()}. termino {terminoParallelFor.ToLongTimeString()}. Total: {tiempoForParallel.Milliseconds}");

            #endregion

            
            #region PLINQ
            Console.WriteLine("========== Parallel LINQ  ==================");

            var dataLinq = Enumerable.Range(1,50);
            //dividir la fuente de datos en sementos usando el método AsParallel()
            var plinq = from d in  dataLinq.AsParallel()
                        where d % 10 == 0
                        select d;

            foreach (var item in plinq)
            {
                Console.WriteLine(item);
            }

            #endregion


        }

        protected async void StartTask()
        {
            string result = await GetData();
        }
        public async Task<string> GetData()
        {
            return string.Empty;
        }
    }
}
