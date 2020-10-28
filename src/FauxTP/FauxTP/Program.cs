using System;
using System.Runtime.InteropServices;
using FauxTP.Library;

namespace FauxTP
{
    class RequestHeader
    {
        private static enum requestFlag
        {
            NULL = 00, //Null flag
            SEND = 10, //Send file
            RECV = 20, //Receive file
            DELE = 30, //Delete file. Will not overwrite.
            OVER = 40, //Overwrite file
            PAUS = 50, //Pause transfer
            RESU = 60, //Resume transfer
            CANC = 70, //Cancel transfer
            CHEK = 80, //Check if a file exists
            DIRE = 90  //Return a list of all files in the active directory
        }

        requestFlag Flag { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
    

        RequestHeader(string input = "0,00,/test/test/,test,test")
        {
            //Input string format (UTF8 encoding): length, flags(comma separated), filepath, filename, fileextension
            //Digest the CSV into a string array
            string[] csvSplit = input.Split(',');
            //Assign values
            int length = csvSplit[0]; //Purpose of length unknown, likely vestigial, possible use in case of multiple flags. No such event should occur. 
            this.Flag = requestFlag[(int)csvSplit[1]]; //Assumes one flag
            this.Path = csvSplit[2]; //Needs set protection against '..' exploits
            this.FileName = csvSplit[3]; //Needs set protections
            this.FileExtension = csvSplit[4]; //Needs set protections
        }
    }

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
