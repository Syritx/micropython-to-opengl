using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Text;
using System.Threading;

using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.Common;

using microcontroller_opengl_test.src.game;

namespace microcontroller_opengl_test.src.networking {

    class Server {

        Socket serverSocket;
        static Task waitForClients;

        public Server() {
            
            IPAddress HOST = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
            const int PORT = 6060;
            IPEndPoint endPoint = new IPEndPoint(HOST, PORT);

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(endPoint);
            serverSocket.Listen(5);
        }

        public void Start() {
            GameWindowSettings gameWindowSettings = new GameWindowSettings();
            NativeWindowSettings nativeWindowSettings = new NativeWindowSettings() {
                Size = new Vector2i(1000, 720),
                Title = "Multiplayer",
                APIVersion = new Version(3, 2),
                Flags = ContextFlags.ForwardCompatible,
                Profile = ContextProfile.Core,
                StartVisible = true,
                StartFocused = true,
            };

            waitForClients = Task.Factory.StartNew(WaitForClients);
            new Scene(gameWindowSettings, nativeWindowSettings);
            Task.WaitAll(waitForClients);
        }


        void ClientThread(Socket client) {
            bool isConnected = true;

            while (isConnected) {
                try {
                    byte[] bytes = new byte[2048];
                    client.Receive(bytes);

                    string command = Encoding.UTF8.GetString(bytes);
                    if (command.StartsWith("inc")) {
                        Scene.InputPressed(.1f);
                    }
                }
                catch(Exception e) {
                    isConnected = false;
                }
            }
            client.Close();
        }

        void WaitForClients() {
            while (true) {
                Socket client = serverSocket.Accept();
                Console.WriteLine("connection");
                Task.Run(() => ClientThread(client));
            }
        }
    }
}