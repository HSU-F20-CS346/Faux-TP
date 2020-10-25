using System;
using FauxTP.Library;
namespace FauxTP.Terminal
{
    class Program
    {
        static void Main(string[] args)
        {
            string filename = "testfile2.csv";
            bool shouldOverwrite = false;
            
            Console.WriteLine("Hello World!");
            string[] filecontents = Functions.loadCSV();
            Functions.overwriteTest(filecontents, filename, shouldOverwrite);
        }
    }
}
