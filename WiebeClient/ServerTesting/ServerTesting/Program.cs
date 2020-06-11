using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerTesting
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            string ip = "127.0.0.1";
            int port = 69;
            bool verbose = args.Contains("--verbose");
            if (args.Contains("--ip"))
                ip = args[Array.FindIndex(args, arg => arg == "--ip") + 1];
            if (args.Contains("--port"))
                port = Convert.ToInt32(args[Array.FindIndex(args, arg => arg == "--port") + 1]);

            Server server = new Server(ip, port, verbose);
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "stop")
                    break;
                if (input == "clear")
                    Console.Clear();
                if (input == "help" || input == "h")
                    Console.WriteLine("Commands:\n'stop' stops the server\n'clear' clears the console\n'verbose' toggles verbose logging mode\n'help' shows all commands");
                if (input == "verbose")
                    server.ToggleVerbose();
            }
        }
    }
}
