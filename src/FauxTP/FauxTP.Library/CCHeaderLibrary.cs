using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FauxTP.Library
{

    /*Assumptions:
    Client and Server are able to move IControlHeader objects between them.
    Server may be serving multiple clients at once.
    Client and Server will know how to behave when C&C info is fed to them.
    The execution of C&C commands such as pause will be handled elsewhere.
    */

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
            DIRE = 90 //Return a list of all files in the active directory
        }

        requestFlag Flag { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
    

        RequestHeader(string input = "")
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

    class ResponseHeader
    {
        private static enum responseFlag
        {
            NOERR = 00, //No Error detected
            FAILW = 10, //Failure to Write
            FAILR = 20, //Failure to Read
            RPATH = 21, //Failure to Read: Bad Path
            RCORR = 22, //Failure to Read: File Corrupt
            RDNEX = 23, //Failure to Read: File does not exist
            FILEE = 30, //File exists and overwrite flag not set
            PERMS = 40 //Permissions error
        }
        responseFlag Flag;

    }

    class EOFHeader : IControlHeader
    {

    }



}