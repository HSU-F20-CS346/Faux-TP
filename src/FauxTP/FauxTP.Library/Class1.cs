using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;


namespace FauxTP.Library
{
    public class Class1
    {

    }
    public class CurrentFile
    {
        public byte[] FileContents { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public string Flag { get; set; }
    }


    public class DataPort
    {
        //Authenication could be worried about at DataConnection, but at this point we are going to assume that is handled elsewhere.
        //String operation and String file in the DataConnection parameters possibly could be in an object depending on Riley's approach in Control Port
        //Notes for later editing. The switch statements in the DataConnection will need to be edited depeading on how we name the operations (send, recv, Pause ect....)
        //Notes 10/27/2020 After linking up with Riley we are sure now about what we are getting passed from the control port. From the control port we will be getting...
        //... An object with different values inside.

        static void DataConnection(string server, string operation, string filePath, string rileysFlag)
        {
            try
            {
                CurrentFile file = new CurrentFile();
                file.FileName = filePath;
                file.Flag = rileysFlag;





                //Port will always be 3462 for Data sending operations.
                int port = 3462;

                TcpClient client = new TcpClient(server, port);
                BufferedStream stream = new BufferedStream(client.GetStream());
                
                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream);

                String defaultReturn = "DefaultOperationReturn";
                byte[] defaultReturnBytes = Encoding.UTF8.GetBytes(defaultReturn);



                switch (file.Flag)
                {
                    case "SEND":
                        //In the send function the File.ReadAllText is reading the file within the filePath (file.FileName)
                        //Than the file is converted into bytes and sent through the binary writer. 
                        String fileString = File.ReadAllText(file.FileName); 
                        file.FileContents = Encoding.UTF8.GetBytes(fileString);
                        writer.Write(IPAddress.HostToNetworkOrder(file.FileContents.Length));
                        writer.Write(file.FileContents);
                        break;
                        

                    case "RECV":
                        //For right now we are downloading the files to the System.AppDomain.CurrentDomain.BaseDirectory and not accounting for any subfolders. Which may come in the future
                        String recvFile;
                        int recvFileLength = IPAddress.NetworkToHostOrder(reader.ReadInt32());
                        file.FileContents = reader.ReadBytes(recvFileLength);
                        recvFile = Encoding.UTF8.GetString(file.FileContents);
                        //TODO: download the file to the default System.AppDomain.CurrentDomain.BaseDirectory
                        //Note this will include rebuilding the file from bytes into strings than into file type
                        break;

                }

            }
            catch(Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                throw e;
            }

        }
    }
}