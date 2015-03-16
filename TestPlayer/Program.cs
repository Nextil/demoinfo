using System;
using System.Diagnostics;
using System.IO;
using EHVAG.DemoInfo;

namespace TestPlayer
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var stream = File.OpenRead(args[0]);

            DemoParser parser = new DemoParser(stream);

            parser.ParseToEnd();
            watch.Stop();

            Console.WriteLine(watch.Elapsed.TotalMilliseconds);

        }
    }
}
