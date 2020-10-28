using FauxTP.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace FauxTP.Terminal
{
    public class ControlConnection
    {
        public static int controlPort { get; set; }

        public static bool IsRunning = false;

        public void controlSend(string Header, string address, string authKey)
        {
            try
            {
                TcpClient client = new TcpClient(address, controlPort);
                using (BufferedStream stream = new BufferedStream(client.GetStream()))
                {
                    BinaryReader reader = new BinaryReader(stream);
                    BinaryWriter writer = new BinaryWriter(stream);

                    byte[] authBytes = Encoding.UTF8.GetBytes(authKey);
                    writer.Write(IPAddress.HostToNetworkOrder(authBytes.Length));
                    writer.Write(authBytes);

                    byte[] headerBytes = Encoding.UTF8.GetBytes(Header);
                    writer.Write(IPAddress.HostToNetworkOrder(headerBytes.Length));
                    writer.Write(headerBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: {0}", ex.Message);
                throw ex;
            }
        }

        public static string controlRecieve()
        {
            IsRunning = true;
            string header = null;
            TcpListener listener = new TcpListener(IPAddress.Any, controlPort);
            listener.Start();
            while (IsRunning)
            {

                TcpClient client = null;
                try
                {
                    client = listener.AcceptTcpClient();

#if DEBUG == false
                    //timeouts affect debugging when stepping through code
                    client.ReceiveTimeout = 5000;
#endif

                }
                catch (Exception ex)
                {
                    Console.WriteLine("error: {0}", ex.Message);
                    continue;
                }


                BinaryReader reader = null;
                BinaryWriter writer = null;
                try
                {


                    using (BufferedStream stream = new BufferedStream(client.GetStream()))
                    {
                        reader = new BinaryReader(stream);
                        writer = new BinaryWriter(stream);

                        int authLength = IPAddress.NetworkToHostOrder(reader.ReadInt32());
                        byte[] authBytes = reader.ReadBytes(authLength);
                        string authKey = Encoding.UTF8.GetString(authBytes);

                        if (Authenticator.Whitelist.ContainsKey(authKey) == true) {
                            IsRunning = false;
                            int headerLength = IPAddress.NetworkToHostOrder(reader.ReadInt32());
                            byte[] headerBytes = reader.ReadBytes(headerLength);
                            header = Encoding.UTF8.GetString(headerBytes);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error: {0}", ex.Message);
                    throw ex;
                }
            }
            return header;
        }
    }
}
