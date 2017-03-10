using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace StringBuilderFormatPlus
{
    class Program
    {
        static string oreo = "oreo";
        static Stopwatch sw;

        static void Main(string[] args)
        {
            sw = new Stopwatch();

            //Test1();
            //Test2();

            Test3();
            Test4();
        }

        private static void Test3()
        {
            sw.Reset();
            sw.Start();

            int totalForLoop = 20000;

            for (int i = 0; i < totalForLoop; i++)
            {
                ProcessStringFormat();
            }

            sw.Stop();

            DisplayStopWatchResults("Test3()-ProcessStringFormat | For Loop", totalForLoop, sw);
        }

        private static void Test4()
        {
            sw.Reset();
            sw.Start();

            int totalForLoop = 20000;

            for (int i = 0; i < totalForLoop; i++)
            {
                ProcessStringBuilderAppend();
            }

            sw.Stop();

            DisplayStopWatchResults("Test4()-ProcessStringBuilderAppend | For Loop", totalForLoop, sw);
        }

        private static void Test1()
        {
            sw.Reset();
            sw.Start();
            ProcessStringFormat();
            sw.Stop();

            DisplayStopWatchResults("Test1()-ProcessStringFormat", null, sw);
        }

        private static void Test2()
        {
            sw.Start();
            ProcessStringBuilderAppend();
            sw.Stop();

            DisplayStopWatchResults("Test2()-ProcessStringBuilderAppend", null, sw);
        }

        private static void ProcessStringFormat()
        {
            Debug.WriteLine(string.Format("We will win the {0} one day soon hopefully!", oreo));
        }

        private static void ProcessStringBuilderAppend()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("We will win the");
            sb.Append(" ");
            sb.Append(oreo);
            sb.Append(" ");
            sb.Append("one day soon hopefully!");

            Debug.WriteLine(sb.ToString());
        }

        private static void DisplayStopWatchResults(string task, int? count, Stopwatch sw)
        {

            string output = string.Format("Time: {0} | Task: {1} | Number: {2} | StopWatch Time: {3}",
                                            DateTime.Now.ToString(),
                                            task,
                                            count != null ? count.ToString() : "no count",
                                            sw.Elapsed.ToString());



            Debug.WriteLine(output);

            // put to a text file...
            using (StreamWriter writer = new StreamWriter("Results_StringBuilderFormat.txt", true))
            {
                writer.WriteLine(output);
                writer.WriteLine("------------------------------------------------------------------------------------------------------------------");
            }
        }
    }
}
