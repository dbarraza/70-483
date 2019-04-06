#define DEBUG
#undef TRACE //By default the trace directive is configured, so i have to "undefine" it

// #define FLAG
// #define NEPE
// #define BANDERA

using System;

namespace _15_CompilerDirectives
{
    class Program
    {
        static void Main(string[] args)
        {
            #if (DEBUG)
            Console.WriteLine("Debugging is enabled");
            #endif

            #if (TRACE)
            Console.WriteLine("Tracing is enabled");
            #endif

            Console.WriteLine();

            #if (FLAG || NEPE)
            Console.WriteLine("There is a Flag");
            #elif (BANDERA)
            Console.WriteLine("There is a Bandera");
            #else
            Console.WriteLine("There is not Flag");
            #endif


            Console.WriteLine("Default/Normal Line No");
            #line 100
            Console.WriteLine("Override Line No");
            #line hidden
            Console.WriteLine("Hidden Line No");
            #line default
            Console.WriteLine("Default/Noraml Line No");
        }
    }
}
