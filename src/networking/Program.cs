using System;

namespace microcontroller_opengl_test.src.networking {
    class Program
    {
        static Server server;
        static void Main(string[] args)
        {
            server = new Server();
            server.Start();
        }
    }
}
