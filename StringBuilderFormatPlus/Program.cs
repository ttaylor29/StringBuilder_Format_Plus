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
        static Stopwatch swFirst;
        static string fullPath;

        static int countForTest3andTest4 = 200000;

        static void Main(string[] args)
        {
            // set path
            fullPath = string.Format("{0}{1}",
                                     AppDomain.CurrentDomain.BaseDirectory.ToString().Replace(@"bin\Debug\", ""),
                                     "Results_UsingStrings_Final_ByTitus.txt");


            sw = new Stopwatch();

            swFirst = new Stopwatch();

            // Test 4 is string builder append
            // Test 3 is string format
            // test 35 is using a +

            //Test1();
            //Test2();

            //swFirst = Test3();
            //Test4(swFirst, "Test3()-ProcessString_Using_StringFormat");

            //swFirst = Test35();
            //Test3_1(swFirst, "Test35()-ProcessString_Using_APlus");


            swFirst = Test35();
            Test4(swFirst, "Test35()-ProcessString_Using_APlus");

            //string test = AppDomain.CurrentDomain.BaseDirectory.ToString().Replace(@"bin\Debug\","");
        }

        private static Stopwatch Test3()
        {
            sw = new Stopwatch();
            sw.Start();

           
            for (int i = 0; i < countForTest3andTest4; i++)
            {
                ProcessString_Using_StringFormat(i);
            }

            sw.Stop();

            DisplayStopWatchResults("Test3()-ProcessString_Using_StringFormat | For Loop", countForTest3andTest4, sw, null, null);

            return sw;
        }

        private static Stopwatch Test3_1(Stopwatch test35Sw, string taskFirst)
        {
            sw = new Stopwatch();
            sw.Start();


            for (int i = 0; i < countForTest3andTest4; i++)
            {
                ProcessString_Using_StringFormat(i);
            }

            sw.Stop();

            DisplayStopWatchResults("Test3_1()-ProcessString_Using_StringFormat | For Loop", countForTest3andTest4, sw, test35Sw, taskFirst);

            return sw;
        }

        private static Stopwatch Test35()
        {
            sw = new Stopwatch();
            sw.Start();


            for (int i = 0; i < countForTest3andTest4; i++)
            {
                ProcessString_Using_APlus(i);
            }

            sw.Stop();

            DisplayStopWatchResults("Test35()-ProcessString_Using_APlus | For Loop", countForTest3andTest4, sw, null, null);

            return sw;
        }

        private static Stopwatch Test4(Stopwatch test3Sw, string taskFirst)
        {
            sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < countForTest3andTest4; i++)
            {
                ProcessString_Using_StringBuilderAppend(i);
            }

            sw.Stop();

            DisplayStopWatchResults("Test4()-ProcessString_Using_StringBuilderAppend | For Loop", countForTest3andTest4, sw, test3Sw, taskFirst);

            return sw;
        }

        private static void Test1()
        {
            sw.Reset();
            sw.Start();
            ProcessString_Using_StringFormat(null);
            sw.Stop();

            DisplayStopWatchResults("Test1()-ProcessString_Using_StringFormat", null, sw, null, null);
        }

        private static Stopwatch Test2(Stopwatch swFirst, string taskFirst)
        {
            sw.Start();
            ProcessString_Using_StringBuilderAppend(null);
            sw.Stop();

            DisplayStopWatchResults("Test2()-ProcessString_Using_StringBuilderAppend", null, sw, swFirst, taskFirst);

            return sw;
        }

        private static void ProcessString_Using_StringFormat(int? count)
        {
            Debug.WriteLine(string.Format("ProcessString_Using_StringFormat | Count: {0} | We will win the {1} one day soon hopefully!",
                                            count != null ? string.Format("{0:n0}", count) : "no count",
                                            oreo));
        }

        private static void ProcessString_Using_APlus(int? count)
        {

            string value = "ProcessString_Using_APlus | Count: " +
                            (count != null ? string.Format("{0:n0}", count) : "no count") +
                            " | We will win the " + oreo + " one day soon hopefully!";


            Debug.WriteLine(value);
                                          
                                            
        }

        private static void ProcessString_Using_StringBuilderAppend(int? count)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("ProcessString_Using_StringBuilderAppend | Count: ");
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
                                            count != null ? string.Format("{0:n0}", count) : "no count",
                                            sw.Elapsed.ToString());

            Debug.WriteLine(output);

            string outputDiff = string.Empty;
            TimeSpan spanFirst;
            TimeSpan spanSecond;
            TimeSpan spanDiff;

            if (swFirst != null && taskFirst != null)
            {

                if (swFirst.Elapsed.TotalMilliseconds > sw.Elapsed.TotalMilliseconds)
                {
                    spanFirst = new TimeSpan(swFirst.Elapsed.Days, swFirst.Elapsed.Hours,
                                             swFirst.Elapsed.Minutes, swFirst.Elapsed.Seconds, swFirst.Elapsed.Milliseconds);

                    spanSecond = new TimeSpan(sw.Elapsed.Days, sw.Elapsed.Hours,
                                              sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.Elapsed.Milliseconds);

                    spanDiff = spanFirst.Subtract(spanSecond);

                    outputDiff = string.Format("Time: {0} | Task {1} was slower by: {2} | Total Milliseconds: {3}",
                                                DateTime.Now.ToString(),
                                                taskFirst, spanDiff.Duration().ToString(), spanDiff.Duration().TotalMilliseconds.ToString());
                }
                else
                {
                    spanFirst = new TimeSpan(sw.Elapsed.Days, sw.Elapsed.Hours,
                                             sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.Elapsed.Milliseconds);

                    spanSecond = new TimeSpan(swFirst.Elapsed.Days, swFirst.Elapsed.Hours,
                                              swFirst.Elapsed.Minutes, swFirst.Elapsed.Seconds, swFirst.Elapsed.Milliseconds);

                    spanDiff = spanSecond.Subtract(spanFirst);

                    outputDiff = string.Format("Time: {0} | Task {1} was slower by: {2} | Total Milliseconds: {3}",
                                               DateTime.Now.ToString(),
                                               task, spanDiff.Duration().ToString(), spanDiff.Duration().TotalMilliseconds.ToString());
                }

                Debug.WriteLine(outputDiff);
            }



           
           

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
                    writer.WriteLine("-------------------------------------------------------------------------------------------------------------------------------");
                }
                
            }
        }
    }
}
