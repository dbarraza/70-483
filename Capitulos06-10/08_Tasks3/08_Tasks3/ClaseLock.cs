using System;

namespace _08_Tasks3
{
    public class ClaseLock
    {
        private object locker = new object();
        private int saldo = 0;

        public ClaseLock(int saldo)
        {
            this.saldo = saldo;
        }

        public void Sumar(int x)
        {
            lock (locker)
            {
                Console.WriteLine($"                                      Antes de sumar {x} a saldo {saldo}");
                saldo += x;
                Console.WriteLine($"                                      Después de sumar {x} a saldo {saldo}");
            }
        }

        public void Restar(int x)
        {
            lock (locker)
            {
                Console.WriteLine($"Antes de restar {x} a saldo {saldo}");
                saldo -= x;
                Console.WriteLine($"Después de restar {x} a saldo {saldo}");
            }
        }
    }
}
