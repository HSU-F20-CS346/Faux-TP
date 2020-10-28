using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace FauxTP.Library
{
    //log in and then if requesting file enter in IP address

    public class Authenticator
    {
        public static int ListenPort { get; set; }
        public bool IsRunning { get; private set; }

        private Thread _receivingThread = null;

        public static Dictionary<string, User> Whitelist { get; set; }

        public static string SynchronousAuthentication(string address, String Uname)
        {
            //string to return
            string authString = null;
            try
            {
                TcpClient client = new TcpClient(address, ListenPort);
                using (BufferedStream stream = new BufferedStream(client.GetStream()))
                {
                    BinaryReader reader = new BinaryReader(stream);
                    BinaryWriter writer = new BinaryWriter(stream);

                    //TODO: do work!

                    //send username
                    byte[] username = Encoding.UTF8.GetBytes(Uname);
                    writer.Write(IPAddress.HostToNetworkOrder(username.Length));
                    writer.Write(username);

                    //recieve auth key from server
                    int authKeyLength = IPAddress.NetworkToHostOrder(reader.ReadInt32());
                    byte[] authKeyBytes = reader.ReadBytes(authKeyLength);
                    authString = Encoding.UTF8.GetString(authKeyBytes);

                    string ipAddress = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                    User user = new User()
                    {
                        userAddress = ipAddress,
                        authKey = authString
                    };
                    Whitelist[user.authKey] = user;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: {0}", ex.Message);
                throw ex;
            }
            return authString;
        }

        public void Run()
        {
            IsRunning = true;
            TcpListener listener = new TcpListener(IPAddress.Any, ListenPort);
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
                    using (BufferedStream stream = new BufferedStream(client.GetStream())) {
                        reader = new BinaryReader(stream);
                        writer = new BinaryWriter(stream);

                        //grab auth string
                        int userNameLength = IPAddress.NetworkToHostOrder(reader.ReadInt32());
                        byte[] userNameBytes = reader.ReadBytes(userNameLength);
                        string userName = Encoding.UTF8.GetString(userNameBytes);


                        //TODO validate

                        //assign access key
                        string address = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
                        User user = new User()
                        {
                            userAddress = address,
                            authKey = GenerateAccessKey()
                        };
                        Whitelist[user.authKey] = user;

                        //return access key to client
                        byte[] authBytes = Encoding.UTF8.GetBytes(user.authKey);
                        writer.Write(IPAddress.HostToNetworkOrder(authBytes.Length));
                        writer.Write(authBytes);
                            
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("error: {0}", ex.Message);
                    throw ex;
                }
               
            }
        }

        public void AcceptConnections()
        {
            ThreadStart authStart = Run;
            _receivingThread = new Thread(authStart);
            _receivingThread.Start();
        }

        public static string GenerateAccessKey()
        {
            var key = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(key);
            }
            return Convert.ToBase64String(key);
        }
    }

}