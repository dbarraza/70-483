using System;
using System.IO;

namespace _07_ObjectLifeCycle
{
    class DisposablePatterns : IDisposable
    {   
        bool disposed = false;  //determina si el método dispose ha sido invocado
        StreamReader reader;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); //indica que el finalizador no debe ser llamado
        }

        public void Dispose(bool disposing)
        {
            if(disposing)
            return;

            if(disposing)
            {
                if(reader != null)
                    reader.Dispose();
            }

            disposed = true;
        }

        ~DisposablePatterns(){
            Dispose(false);
        }

    }
}