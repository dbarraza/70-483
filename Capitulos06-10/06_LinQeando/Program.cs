using System;
using System.Collections.Generic;
using System.Linq;

namespace LinQeando
{
    class Program
    {
        static void Main(string[] args)
        {
            var personas = new Persona[]
            {
                new Persona
                {
                    Edad = 21,
                    Nombre = "Daniel Barraza"
                },
                new Persona
                {
                    Edad = 22,
                    Nombre = "Javier Barraza"
                }
            };

            var gente = new Gente(personas);

            var daniel = gente.Where(p => p.Nombre == "Daniel Barraza").First();
            daniel.Edad = 32;

            foreach (var p in gente)
            {
                Console.WriteLine(p);
            }

        }
    }
}
