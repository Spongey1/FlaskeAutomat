namespace FlaskeAutomat
{
    public class Bottle
    {
        public string Name { get; set; }
        public int SerialNumber { get; set; }
        private static int Count = 1;
        public Bottle(string name)
        {
            Name = name;
            SerialNumber = Count;

            Count++;
        }
    }
}