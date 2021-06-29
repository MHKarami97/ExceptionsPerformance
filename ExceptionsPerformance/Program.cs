using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ExceptionsPerformance
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var result = await Task.Run(WithException);
            var result1 = await Task.Run(WithOutException);

            Console.WriteLine($"WithException: {result} , TotalMilliSecond: {result.Milliseconds}");
            Console.WriteLine($"WithOutException: {result1} , TotalMilliSecond: {result1.Milliseconds}");
        }

        static TimeSpan WithException()
        {
            var watch = new Stopwatch();
            var list = new List<string>();

            for (var i = 0; i < Math.Pow(10, 6); i++)
            {
                if (i == 1 || i % 2 != 0)
                {
                    list.Add(i.ToString());
                }
                else
                {
                    list.Add(null);
                }
            }

            watch.Start();

            foreach (var s in list)
            {
                try
                {
                    var i = s.Length;
                }
                catch (Exception ex)
                {
                    var e = ex.Message;
                }
            }

            watch.Stop();

            return watch.Elapsed;
        }

        static TimeSpan WithOutException()
        {
            var watch = new Stopwatch();
            var list = new List<string>();

            for (var i = 0; i < Math.Pow(10, 6); i++)
            {
                if (i == 1 || i % 2 != 0)
                {
                    list.Add(i.ToString());
                }
                else
                {
                    list.Add(null);
                }
            }

            watch.Start();

            foreach (var s in list)
            {
                if (s == null)
                    continue;

                var i = s.Length;
            }

            watch.Stop();

            return watch.Elapsed;
        }
    }
}