using System;
using System.Runtime.InteropServices;
using FauxTP.Library;

namespace FauxTP
{
    class Program
    {
        static void Main(string[] args)
        {
            RequestHeader testHead = new TestHead();
            testHead.Flag = 20;
            Console.WriteLine(testHead.Flag);
        }
    }
}
