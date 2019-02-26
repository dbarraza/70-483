using System;
using System.Threading;

namespace _08_Tasks3
{
    public class ClaseMonitor
    {
        private int saldo;
        private static object monitor = new object();

        public ClaseMonitor(int saldo)
        {
            this.saldo = saldo;
        }

        public void Sumar(int x)
        {
            Monitor.Enter(monitor);
            Console.WriteLine($"                    Antes de sumar {x} a saldo {saldo}");
            saldo += x;
            Console.WriteLine($"                    Después de sumar {x} a saldo {saldo}");
            Monitor.Exit(monitor);
        }

        public void Restar(int x)
        {
            Monitor.Enter(monitor);
            Console.WriteLine($"Antes de restar {x} a saldo {saldo}");
            saldo -= x;
            Console.WriteLine($"Después de restar {x} a saldo {saldo}");
            Monitor.Exit(monitor);
        }
    }
}
