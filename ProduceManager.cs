using System;
using System.Threading;

namespace FlaskeAutomat
{
    public class ProduceManager
    {
        public void Producer(int delay)
        {
            Random rnd = new Random();

            while (true)
            {
                Thread.Sleep(delay);
                if (Monitor.TryEnter(Storage.buffer))
                {
                    // checks if array contains a single null value, returns true / false
                    if (Array.Exists(Storage.buffer, x => x == null))
                    {
                        try
                        {
                            SetBottleValue(CreateBottle());

                            Monitor.PulseAll(Storage.buffer);
                        }
                        finally
                        {
                            Monitor.Exit(Storage.buffer);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Producer is Sleeping");
                        Monitor.Wait(Storage.buffer);
                    }
                }
            }
        }

        public Bottle CreateBottle()
        {
            Random rnd = new Random();
            Bottle value = null;

            switch (rnd.Next(0, 2)) // Creates bottle depending on random number from 0-1
            {
                case 0:
                    value = new Bottle("Beer");
                    break;
                case 1:
                    value = new Bottle("Soda");
                    break;
            }

            return value;
        }

        public void SetBottleValue(Bottle value)
        {
            for (int i = 0; i < Storage.buffer.Length; i++)
            {
                // checks if bottle is null, if so sets bottle value and then breaks out of for-loop
                if (Storage.buffer[i] == null)
                {
                    Storage.buffer[i] = value;
                    Console.WriteLine("Producer | {0} | Buffer", value.Name + value.SerialNumber);
                    break;
                }
            }
        }
    }
}