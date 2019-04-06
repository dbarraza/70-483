#undef TRACE
#undef DEBUG

using System;
using System.Threading.Tasks;
using System.Diagnostics;


namespace _15_TraceAndLog
{
    class Program
    {

        // static TraceSource ts = new TraceSource("Contoso", SourceLevels.ActivityTracing);
        static TraceSource ts = new TraceSource("Contoso", SourceLevels.ActivityTracing);

        static void Main(string[] args)
        {
            try
            {
                int age = 10;
                Debug.WriteLineIf(age.GetType() == typeof(int),"Debug Mode");
            }
            catch (System.Exception)
            {
                Debug.Assert(false);  
                throw;
            }

            Console.WriteLine();

            Trace.WriteLine("Numbers must be int");

            int n1 = 10;
            int n2 = 0;

            Trace.WriteLineIf(n1.GetType()== typeof(int) && n2.GetType() == typeof(int),"boths are numbers");

            if(n2 < 1)
            {
                n2 = n1;
                Trace.TraceInformation("n2 has been changed due to zero value");
                Trace.TraceError("n2 has been changed due to zero value");
                Trace.Indent();
                Trace.Indent();
                Trace.WriteLine("Test");

            }
            
            // TraceSource tsrc = new TraceSource("TraceSource1", SourceLevels.ActivityTracing);
            // tsrc.TraceInformation("information");
            // tsrc.TraceData(TraceEventType.Information, 1, new string[]{"info","info2"});
            // tsrc.Flush(); // flush the buffer
            // tsrc.Close(); //
            
            
            DoWork();

            #if DEBUG
            Console.WriteLine("Debuging test");
            #endif

        }

        public static void DoWork()
        {
            Console.WriteLine("DoWork");

            #region Pregunta Examen 70-483
            ts.TraceInformation("Information");
            
            var originalId = Trace.CorrelationManager.ActivityId;
            Console.WriteLine(originalId);

            var guid = Guid.NewGuid();
            ts.TraceTransfer(1,"Changing Activity",guid);

            Trace.CorrelationManager.ActivityId = guid;
            ts.TraceEvent(TraceEventType.Start,0,"Start");


            //finally
            ts.TraceTransfer(1,"Changing Activity", originalId);
            ts.TraceEvent(TraceEventType.Stop,0,"Stop");
            ts.TraceData(TraceEventType.Information,1,"tesssss");

            var decimala = Method1(10);
            Console.WriteLine(decimala.Result);

            #endregion
        }

        public static async Task<decimal> Method1(int i)
        {
            return await Task.Run(() =>
             {
                 return Convert.ToDecimal(i);
             });
        }
    }
}
