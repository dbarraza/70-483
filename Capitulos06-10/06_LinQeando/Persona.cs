using System.Collections;
using System.Collections.Generic;

namespace LinQeando
{
    public class  Persona
    {
        public string Nombre { get; set; }
        public int Edad { get; set; }

        public override string ToString() => $"Persona {this.Nombre}, Edad {Edad}";       

        public override bool Equals(object c)
        {
            return false;
        }

        public override int GetHashCode()
        {
            return Edad.GetHashCode();
        }

    }



    public class Gente : IEnumerable<Persona>
    {
        private Persona[] args;

        public Gente(Persona[] _agrs) => args = _agrs;

        public Persona this[int i]
        {
            get { return args[i]; }
            set { args[i] = value; }
        }

        public IEnumerator<Persona> GetEnumerator()
        {
            foreach (var p in args)
            {
                yield return p;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        
    }
}