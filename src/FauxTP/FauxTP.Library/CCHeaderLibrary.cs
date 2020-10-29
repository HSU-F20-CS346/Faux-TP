using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace FauxTP.Library
{

    /*Assumptions:
    Client and Server are able to move IControlHeader objects between them.
    Server may be serving multiple clients at once.
    Client and Server will know how to behave when C&C info is fed to them.
    The execution of C&C commands such as pause will be handled elsewhere.
    */

    public class RequestHeader
    {
        public enum requestFlag
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

        //TODO set protection
        requestFlag Flag { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
    

        RequestHeader(string input = ",,,")
        {
            //Input string format (UTF8 encoding): flags(comma separated), filepath, filename, fileextension
            //Digest the CSV into a string array
            try
            {
                string[] csvSplit = input.Split(',');
                //Assign values
                this.Flag = Int32.Parse(csvSplit[0]); //Assumes one flag
                this.FilePath = csvSplit[1]; //Needs set protection against '..' exploits
                this.FileName = csvSplit[2]; //Needs set protections
                this.FileExtension = csvSplit[3]; //Needs set protections
            }
            catch(IndexOutOfRangeException)
            {
                //Not enough arguments in input string. Server responds with FAILR flag
                //TODO
            }
            catch(Exception e)
            {

            }
        }

        public string ComposeCsv()
        {
            return ((int)Flag).ToString() + ',' + FilePath + ',' + FileName + ',' + FileExtension; 
        }

        public string FullPath()
        {
            return FilePath + FileName + "." + FileExtension;
        }
    }

    public class ResponseHeader
    {
        public static enum responseFlag
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
        //TODO set protection
        responseFlag Flag { get; set; }
        int DatagramSize { get; set; }

        ResponseHeader(string input = ",")
        {
            //Input string format (UTF8 encoding): flags(comma separated), filepath, filename, fileextension
            //Digest the CSV into a string array
            try
            {
                string[] csvSplit = input.Split(',');
                //Assign values
                this.Flag = Int32.Parse(csvSplit[0]); //Assumes one flag
                this.DatagramSize = Int32.Parse(csvSplit[1]); //Default value 0 if error
            }
            catch (IndexOutOfRangeException)
            {
                //TODO
            }
            catch(Exception e)
            {
                //TODO
            }
        }

        public string ComposeCsv()
        {
            return ((int)Flag).ToString() + ',' + (DatagramSize.ToString());
        }
    }

    public class EOFHeader
    {
        //TODO set protection
        public byte[] HashValue { get; set; }
        //TODO implement MD5 hash check and find reliable way to transfer MD5 object across wire

        EOFHeader(string input = ",,,")
        {
            //TODO
        }
    }



}