using System;
using System.Threading;

namespace FlaskeAutomat
{
    public class SplitterManager
    {
        public void ConsumerSplitter(int delay)
        {
            while (true)
            {
                Thread.Sleep(delay);
                if (Monitor.TryEnter(Storage.buffer))
                {
                    // checks if array contains a single value other than null, returns true / false
                    if (Array.Exists(Storage.buffer, x => x != null))
                    {
                        try
                        {
                            Bottle value = new Bottle("empty");
                            SplitBottleValue(value);

                            Monitor.PulseAll(Storage.buffer);
                        }
                        finally
                        {
                            Monitor.Exit(Storage.buffer);
                        }
                    }
                    else
                    {
                        Console.WriteLine("ConsumerSplitter is Sleeping");
                        Monitor.Wait(Storage.buffer);
                    }
                }
            }
        }

        public void SplitBottleValue(Bottle value)
        {
            for (int i = 0; i < Storage.buffer.Length; i++)
            {
                if (Storage.buffer[i] != null)
                {
                    if (Storage.buffer[i].Name == "Beer")
                    {
                        value = Storage.buffer[i];
                        for (int b = 0; b < Storage.beers.Length; b++)
                        {
                            if (Storage.beers[b] == null)
                            {
                                Storage.beers[b] = value;
                                Console.WriteLine("ConsumerSplitter | {0} | Beers", value.Name + value.SerialNumber);
                                break;
                            }
                        }
                    }
                    else if (Storage.buffer[i].Name == "Soda")
                    {
                        value = Storage.buffer[i];
                        for (int s = 0; s < Storage.sodas.Length; s++)
                        {
                            if (Storage.sodas[s] == null)
                            {
                                Storage.sodas[s] = value;
                                Console.WriteLine("ConsumerSplitter | {0} | Sodas", value.Name + value.SerialNumber);
                                break;
                            }
                        }

                    }
                }
                Storage.buffer[i] = null;
            }
        }
    }
}