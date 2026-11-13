using System;
using System.Diagnostics;

namespace RegularPolygons
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Complete the input arguments check
            if ((args.Length != 1))
            {
                Console.WriteLine("\nPlease enter a file name.");
                Console.WriteLine("\nUsage: RegularPolygons <filename>");
                Console.WriteLine();
                return;
            }

            try
            {
                // Setup Memory Usage metrics
                Process process = Process.GetCurrentProcess();
                process.Refresh();
                long startPeakWorkingSet64 = process.PeakWorkingSet64;
                long startPeakPagedMemorySize64 = process.PeakPagedMemorySize64;
                long startPeakVirtualMemorySize64 = process.PeakVirtualMemorySize64;

                // Setup Time metrics
                Stopwatch stopWatch = new Stopwatch(); // DateTime startTime = DateTime.Now;
                stopWatch.Start();

                // Execute the program logic
                Console.WriteLine("Processing " + args[0]);
                (new Broker()).Process(args[0]);

                // Report Time metrics
                stopWatch.Stop();
                Console.WriteLine();
                Console.WriteLine(stopWatch.Elapsed); //Console.WriteLine(new TimeSpan(DateTime.Now.Ticks - startTime.Ticks));

                // Report Memory Usage metrics
                process.Refresh();
                Console.WriteLine();
                Console.WriteLine("Delta peak physical memory usage: {0}", process.PeakWorkingSet64 - startPeakWorkingSet64);
                Console.WriteLine("Delta peak paged memory usage: {0}", process.PeakPagedMemorySize64 - startPeakPagedMemorySize64);
                Console.WriteLine("Delta peak virtual memory usage: {0}", process.PeakVirtualMemorySize64 - startPeakVirtualMemorySize64);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }
    }
}

/*
// TEST CASES: 
 * 
triangle,3.5
circle,2

triangle,-1
circle,2

triangle,4.8
triangle,2
square,4.8
penthagon,4.8
circle,2
 * 
very long file
empty file
etc.
*/
