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

    public class DataPort
    {
        //Authenication could be worried about at DataConnection, but at this point we are going to assume that is handled elsewhere.
        //String operation and String file in the DataConnection parameters possibly could be in an object depending on Riley's approach in Control Port
        //Notes for later editing. The switch statements in the DataConnection will need to be edited depeading on how we name the operations (send, recv, Pause ect....)

        static Byte[] DataConnection(string server, string operation, string file)
        {
            try
            {
                //Port will always be 3462 for Data sending operations.
                int port = 3462;

                TcpClient client = new TcpClient(server, port);
                BufferedStream stream = new BufferedStream(client.GetStream());
                
                BinaryReader reader = new BinaryReader(stream);
                BinaryWriter writer = new BinaryWriter(stream);

                String defaultReturn = "DefaultOperationReturn";
                byte[] defaultReturnBytes = Encoding.UTF8.GetBytes(defaultReturn);



                switch (operation)
                {
                    case "SEND":
                        byte[] fileBytes = Encoding.UTF8.GetBytes(file);
                        writer.Write(IPAddress.HostToNetworkOrder(fileBytes.Length));
                        writer.Write(fileBytes);
                        break;
                        

                    case "RECV":
                        String recvFile;
                        int recvFileLength = IPAddress.NetworkToHostOrder(reader.ReadInt32());
                        byte[] recvFileBytes = reader.ReadBytes(recvFileLength);
                        recvFile = Encoding.UTF8.GetString(recvFileBytes);
                        return recvFileBytes;

                }

                return defaultReturnBytes;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                throw e;
            }

        }

    }
}
