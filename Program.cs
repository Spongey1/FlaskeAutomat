using System;

namespace FlaskeAutomat
{
    class Program
    {
        static void Main(string[] args)
        {
            Threads t = new Threads();

            t.ThreadWork();
        }
    }
}
