using System;
using System.Threading;

namespace FlaskeAutomat
{
    public class ConsumeManager
    {
        public void Consumer(int delay)
        {
            while (true)
            {
                Thread.Sleep(delay);
                if (Monitor.TryEnter(Storage.beers))
                {
                    ConsumeAction(Storage.beers);
                }
                else if (Monitor.TryEnter(Storage.sodas))
                {
                    ConsumeAction(Storage.sodas);
                }
            }
        }

        public void ConsumeAction(Bottle[] arr)
        {
            try
            {
                if (Array.Exists(arr, x => x == null))
                {
                    // Checks array for a value other than null, then consumes it and turns it into null
                    for (int b = 0; b < arr.Length; b++)
                    {
                        if (arr[b] != null)
                        {
                            Console.WriteLine("Consumer is drinking {0}", arr[b].Name);
                            arr[b] = null;
                            break;
                        }
                    }
                    Monitor.PulseAll(arr);
                }
                else
                {
                    Console.WriteLine("Consumer{0} is sleeping", Thread.CurrentThread.Name);
                    Monitor.Wait(arr);
                }
            }
            finally
            {
                Monitor.Exit(arr);
            }
        }
    }
}