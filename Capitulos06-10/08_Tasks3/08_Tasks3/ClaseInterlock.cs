using System;
using System.Threading;

namespace _08_Tasks3
{
    public class ClaseInterlock
    {
        private object locker = new object();
        private int saldo = 0;

        public ClaseInterlock(int saldo)
        {
            this.saldo = saldo;
        }

        public void Sumar(int x)
        {
            Console.WriteLine($"                    Antes de sumar {x} a saldo {saldo}");
            Interlocked.Add(ref saldo,x);
            Console.WriteLine($"                    Después de sumar {x} a saldo {saldo}");
        }

        public void Restar(int x)
        {
            Console.WriteLine($"Antes de restar {x} a saldo {saldo}");
            Interlocked.Add(ref saldo, x * -1);
            Console.WriteLine($"Después de restar {x} a saldo {saldo}");
            
        }
    }
}
