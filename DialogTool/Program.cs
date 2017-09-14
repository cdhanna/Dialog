using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DialogTool
{
    class Program
    {
        static void Main(string[] args)
        {
            // parse options
            var options = new ProgramOptions();
            Console.WriteLine("Starting Service...");

            // begin websocket
            var ws = new WebSocketSharp.Server.WebSocketServer(options.WebsocketPort);
            ws.AddWebSocketService<StateService>("/state");
            ws.Start();
            if (ws.IsListening)
            {
                Console.WriteLine("Websocket available at " + ws.Address + ":" + ws.Port);
            }

            // begin http
            var serverFolder = Path.GetFullPath(options.ServerPath);
            var server = new SimpleHTTPServer(serverFolder, options.HttpPort);
            Console.WriteLine("Http available at " + server.Port);

            // idle
            while (true)
            {

            }

            Console.WriteLine("Exit.");
        }
    }
}
