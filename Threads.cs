using System;
using System.Threading;

namespace FlaskeAutomat
{
    public class Threads
    {
        static object _lock = new object();
        static Bottle[] buffer = new Bottle[3];
        static Bottle[] beers = new Bottle[3];
        static Bottle[] sodas = new Bottle[3];
        public void ThreadWork()
        {
            ProduceManager pm = new ProduceManager();
            SplitterManager sm = new SplitterManager();
            ConsumeManager cm = new ConsumeManager();

            // method parameters signify the delay between each thread action (edit for testing)
            Thread p1 = new Thread(() => pm.Producer(1));
            Thread s1 = new Thread(() => sm.ConsumerSplitter(1));
            Thread c1 = new Thread(() => cm.Consumer(1));
            Thread c2 = new Thread(() => cm.Consumer(1));

            p1.Name = "Producer";
            s1.Name = "Splitter";
            c1.Name = "Consumer";
            c2.Name = "Consumer";

            p1.Start();
            s1.Start();
            c1.Start();
            c2.Start();

            p1.Join();
            s1.Join();
            c1.Join();
            c2.Join();
        }
    }
}