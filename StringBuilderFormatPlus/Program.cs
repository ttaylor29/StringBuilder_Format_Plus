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

        static int countForTest3andTest4 = 10;

        static void Main(string[] args)
        {
            sw = new Stopwatch();

            Stopwatch swFirst = new Stopwatch();

            //Test1();
            //Test2();

            swFirst = Test3();
            Test4(swFirst);

            //string test = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace(@"bin\Debug\","");
        }

        private static Stopwatch Test3()
        {
            sw.Reset();
            sw.Start();

           
            for (int i = 0; i < countForTest3andTest4; i++)
            {
                ProcessStringFormat(i);
            }

            sw.Stop();

            DisplayStopWatchResults("Test3()-ProcessStringFormat | For Loop", countForTest3andTest4, sw, null, null);

            return sw;
        }

        private static Stopwatch Test4(Stopwatch test3Sw)
        {
            sw.Reset();
            sw.Start();

            for (int i = 0; i < countForTest3andTest4; i++)
            {
                ProcessStringBuilderAppend(i);
            }

            sw.Stop();

            DisplayStopWatchResults("Test4()-ProcessStringBuilderAppend | For Loop", countForTest3andTest4, sw, test3Sw, "Test3()-ProcessStringFormat");

            return sw;
        }

        private static void Test1()
        {
            sw.Reset();
            sw.Start();
            ProcessStringFormat(null);
            sw.Stop();

            DisplayStopWatchResults("Test1()-ProcessStringFormat", null, sw, null, null);
        }

        private static Stopwatch Test2(Stopwatch swFirst, string taskFirst)
        {
            sw.Start();
            ProcessStringBuilderAppend(null);
            sw.Stop();

            DisplayStopWatchResults("Test2()-ProcessStringBuilderAppend", null, sw, swFirst, taskFirst);

            return sw;
        }

        private static void ProcessStringFormat(int? count)
        {
            Debug.WriteLine(string.Format("ProcessStringFormat | Count: {0} | We will win the {1} one day soon hopefully!",
                                            count != null ? string.Format("{0:n0}", count) : "no count",
                                            oreo));
        }

        private static void ProcessStringBuilderAppend(int? count)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("ProcessStringBuilderAppend | Count: ");
            sb.Append(count != null ? string.Format("{0:n0}", count) : "no count");
            sb.Append(" | ");
            sb.Append("We will win the");
            sb.Append(" ");
            sb.Append(oreo);
            sb.Append(" ");
            sb.Append("one day soon hopefully!");

            Debug.WriteLine(sb.ToString());
        }

        private static void DisplayStopWatchResults(string task, int? count, Stopwatch sw, Stopwatch swFirst, string taskFirst)
        {

            string output = string.Format("Time: {0} | Task: {1} | Number: {2} | StopWatch Time: {3}",
                                            DateTime.Now.ToString(),
                                            task,
                                            count != null ? string.Format("{0:n0}", count.ToString()) : "no count",
                                            sw.Elapsed.ToString());

            Debug.WriteLine(output);

            string outputDiff = string.Empty;
            TimeSpan spanFirst;
            TimeSpan spanSecond;
            TimeSpan spanDiff;

            if (swFirst != null && sw != null)
            {

                if (swFirst.Elapsed.TotalMilliseconds > sw.Elapsed.TotalMilliseconds)
                {
                    spanFirst = new TimeSpan(swFirst.Elapsed.Days, swFirst.Elapsed.Hours,
                                             swFirst.Elapsed.Minutes, swFirst.Elapsed.Seconds, swFirst.Elapsed.Milliseconds);

                    spanSecond = new TimeSpan(sw.Elapsed.Days, sw.Elapsed.Hours,
                                              sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.Elapsed.Milliseconds);

                    spanDiff = spanFirst.Subtract(spanSecond);

                    outputDiff = string.Format("Time: {0} | Task {1} was faster by: {2} | Total Milliseconds: {3}",
                                                DateTime.Now.ToString(),
                                                taskFirst, spanDiff.ToString(), spanDiff.TotalMilliseconds.ToString());
                }
                else
                {
                    spanFirst = new TimeSpan(sw.Elapsed.Days, sw.Elapsed.Hours,
                                             sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.Elapsed.Milliseconds);

                    spanSecond = new TimeSpan(swFirst.Elapsed.Days, swFirst.Elapsed.Hours,
                                              swFirst.Elapsed.Minutes, swFirst.Elapsed.Seconds, swFirst.Elapsed.Milliseconds);

                    spanDiff = spanSecond.Subtract(spanFirst);

                    outputDiff = string.Format("Time: {0} | Task {1} was faster by: {2} | Total Milliseconds: {3}",
                                               DateTime.Now.ToString(),
                                               task, spanDiff.ToString(), spanDiff.TotalMilliseconds.ToString());
                }

                Debug.WriteLine(outputDiff);
            }



            string fullPath = string.Format("{0}{1}",
                                        AppDomain.CurrentDomain.BaseDirectory.ToString().Replace(@"bin\Debug\", ""),
                                        "Results_StringBuilderFormat.txt");
           

            // put to a text file...
            using (StreamWriter writer = new StreamWriter(fullPath, true))
            {
                if(swFirst == null && taskFirst == null)
                {
                    writer.WriteLine(output);
                    writer.WriteLine("-------------------------");
                }
                else
                {
                    writer.WriteLine(output);
                    writer.WriteLine("=========================>>>");
                    writer.WriteLine(outputDiff);
                    writer.WriteLine("------------------------------------------------------------------------------------------------------------------");
                }
                
            }
        }
    }
}
